using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private readonly IProductoDA _productoDA;
        private readonly IProductoReglas _productoReglas;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas)
        {
            _productoDA = productoDA;
            _productoReglas = productoReglas;
        }

        public async Task<Guid> Agregar(Producto.ProductoRequest producto)
        {
            return await  _productoDA.Agregar(producto);
        }

        public async Task<Guid> Editar(Guid Id, Producto.ProductoRequest producto)
        {
            return await _productoDA.Editar(Id, producto);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _productoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<Producto.ProductoResponse>> Obtener()
        {
            return await _productoDA.Obtener();
        }

        public async Task<Producto.ProductoResponse> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);

            // Si no existe, devolvemos null o lanzamos excepción (depende del profe)
            if (producto == null)
                return null;

            // Aquí se cumple la práctica: calcular precio USD para el detalle
            producto.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);

            return producto;
        }
    }
}
