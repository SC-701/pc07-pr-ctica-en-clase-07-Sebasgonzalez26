using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Producto;
namespace DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        public async Task<Guid> Agregar(Producto.ProductoRequest producto)
        {
            string query = @"AgregarProducto";

            Guid id = Guid.NewGuid();

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(
                query,
                new
                {
                    Id = id,
                    IdSubCategoria = producto.IdSubCategoria,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CodigoBarras = producto.CodigoBarras
                },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, Producto.ProductoRequest producto)
        {
            await VerificarProductoExistencia(Id);
            string query = @"EditarProducto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            });
            return resultadoConsulta;
        }

     

        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarProductoExistencia(Id);
            string query = @"EliminarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<Producto.ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
            
        
        }

        public async Task<Producto.ProductoResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query,
                new {Id=Id});
            return resultadoConsulta.FirstOrDefault();

        }
        private async Task VerificarProductoExistencia(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)

                throw new Exception("El producto no existe");
        }
    }
}
