using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MetricsRazor.Interfaces;
using MetricsRazor.Models;

namespace MetricsRazor.Repositories
{
    public class MetricsRepository : Interfaces.IMetricsRepository
    {
        public async Task<Metrics> GetAll()
        {
            string Url ="http://localhost:57425/metrics";

            using (var client = new HttpClient())
            {
                using (var r = await client.GetAsync(new Uri(Url)))
                {
                    string result = await r.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Metrics>(result);
                }
            }

        }
      
    }
}
