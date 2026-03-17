using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Producto
    {
        public class ProductoBase
        {
            [Required(ErrorMessage ="El nombre es requerido")]
                    [RegularExpression(
                    @"^[A-Za-z0-9 ]{3,50}$",
                    ErrorMessage = "El nombre del producto debe tener entre 3 y 50 caracteres y solo puede contener letras, números y espacios."
                    )]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "La descripcion es requerida")]

            public string Descripcion { get; set; }

            [Required(ErrorMessage = "El Precio es requerido")]
            [RegularExpression(
            @"^\d+(\.\d{1,2})?$",
            ErrorMessage = "El precio debe ser un número válido, con hasta dos decimales."
            )]

            public decimal Precio { get; set; }

            [Required(ErrorMessage = "El Stock es requerido")]
            public int Stock { get; set; }


            [Required(ErrorMessage = "El Codigo de Barras es requerido")]
                    [RegularExpression(
            @"^\d{8}|\d{13}$",
            ErrorMessage = "El código de barras debe tener 8 o 13 dígitos numéricos."
               )]
            public string CodigoBarras { get; set; }
        }

        public class ProductoRequest : ProductoBase
        {
            public Guid IdSubCategoria { get; set; }
        }

        public class ProductoResponse : ProductoBase
        {
            public Guid Id { get; set; }
            public string SubCategoria { get; set; }
            public string Categoria { get; set; }

            public decimal PrecioUSD { get; set; }


        }
    }
}
