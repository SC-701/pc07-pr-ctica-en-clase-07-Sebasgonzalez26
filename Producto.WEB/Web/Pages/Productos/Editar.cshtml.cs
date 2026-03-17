using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using static Abstracciones.Modelos.Producto;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public ProductoRequest productoRequest { get; set; }

        public ProductoResponse? productoResponse { get; set; }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");

            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();

                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                productoResponse = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

                if (productoResponse == null)
                    return NotFound();

                Id = productoResponse.Id;

                productoRequest = new ProductoRequest
                {
                    Nombre = productoResponse.Nombre,
                    Descripcion = productoResponse.Descripcion,
                    Precio = productoResponse.Precio,
                    Stock = productoResponse.Stock,
                    CodigoBarras = productoResponse.CodigoBarras
                };
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");

            Console.WriteLine(endpoint);

            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Id), productoRequest);

            var contenido = await respuesta.Content.ReadAsStringAsync();

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"Error API: {contenido}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}