using LeadsAPI.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace LeadsAPI.Services
{
    public class WorkshopService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly ILogger<WorkshopService> _logger;
        private const string CacheKey = "workshops";

        public WorkshopService(IHttpClientFactory httpClientFactory, IMemoryCache cache, ILogger<WorkshopService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _logger = logger;
        }

        //Metodo que obtiene una lista de los talleres activos
        public async Task<HashSet<int>> GetActiveWorkshops()
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("max@tecnom.com.ar:b0x3sApp"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                HttpResponseMessage response = await client.GetAsync("https://dev.tecnomcrm.com/api/v1/places/workshops");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                List<PlaceDTO>? places = JsonConvert.DeserializeObject<List<PlaceDTO>>(json);

                //obtengo todos los ids y uso el tipo HashSet porque es mas rapido y no permite duplicado como por ahi lo hace List<int>
                HashSet<int> ids_place = places!.Select(p => p.Id).ToHashSet();

                _cache.Set(CacheKey, ids_place);
                return ids_place;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al llamar a la API.");
                return new HashSet<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener talleres.");
                return new HashSet<int>();
            }
        }
    }
}
