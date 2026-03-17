using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Abstracciones.Modelos.Producto;

namespace Web.Pages.Productos
{
    public class AgregarModel : PageModel
    {

        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto {  get; set; }


        public void OnGet()
        {
        }


        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarProducto");

            Console.WriteLine(endpoint);

            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");

        }


    }
}
