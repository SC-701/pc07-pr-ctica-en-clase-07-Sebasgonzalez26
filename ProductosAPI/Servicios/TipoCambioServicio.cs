using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.BancoCentral;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TipoCambioServicio(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<decimal> ObtenerTipoCambioVentaHoy()
        {
            var urlBase = _configuration["BancoCentralCR:UrlBase"];
            var token = _configuration["BancoCentralCR:BearerToken"];

            if (string.IsNullOrWhiteSpace(urlBase))
                throw new Exception("Falta BancoCentralCR:UrlBase en appsettings.json");

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Falta BancoCentralCR:BearerToken en appsettings.json");

            var hoy = DateTime.Now.ToString("yyyy/MM/dd");
            var url = $"{urlBase}?fechaInicio={hoy}&fechaFin={hoy}&idioma=ES";

            var client = _httpClientFactory.CreateClient("BancoCentralCR");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var respuesta = await client.GetAsync(url);
            var json = await respuesta.Content.ReadAsStringAsync();

            respuesta.EnsureSuccessStatusCode();

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<TipoCambioResponse>(json, opciones);

            var tipoCambio = data?.Datos?
                .FirstOrDefault()?.Indicadores?
                .FirstOrDefault()?.Series?
                .FirstOrDefault()?.ValorDatoPorPeriodo;

            if (tipoCambio == null)
                throw new Exception("El BCCR no devolvió tipo de cambio para la fecha consultada.");

            return tipoCambio.Value;
        }
    }
}
