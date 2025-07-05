using LeadsAPI.DTOs;
using LeadsAPI.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
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
        private readonly ExternalService _settings;

        public WorkshopService(IHttpClientFactory httpClientFactory, IMemoryCache cache, ILogger<WorkshopService> logger, IOptions<ExternalService> options)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _logger = logger;
            _settings = options.Value;
        }

        //Metodo que obtiene una lista de los talleres activos
        public async Task<HashSet<int>> GetActiveWorkshops()
        {
            try
            {
                if (_cache.TryGetValue(CacheKey, out HashSet<int>? cachedWorkshops))
                {
                    _logger.LogInformation("Talleres activos: {Ids}", string.Join(", ", cachedWorkshops!));
                    return cachedWorkshops!;
                }

                HttpClient client = _httpClientFactory.CreateClient();
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.Username}:{_settings.Password}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                HttpResponseMessage response = await client.GetAsync(_settings.Url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                List<PlaceDTO>? places = JsonConvert.DeserializeObject<List<PlaceDTO>>(json);

                //obtengo todos los ids y uso el tipo HashSet porque es mas rapido y no permite duplicado como por ahi lo hace List<int>
                HashSet<int> ids_place = places!.Select(p => p.Id).ToHashSet();

                _logger.LogInformation("Talleres activos obtenidos: {Ids}", string.Join(", ", ids_place));

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
