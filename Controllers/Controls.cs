using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Grupo1_tarea_2_1.Controllers
{
    public class Controls
    {
        public async static Task<List<Models.Contrieinfo>> GetPosts(string region)
        {
            List<Models.Contrieinfo> posts = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(Config.Config.EndpointPost+""+region);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Models.Contrieinfo>>(result);
                    }
                    else
                    {
                        // Manejo de error si no se pudo obtener una respuesta exitosa
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de error de solicitud HTTP
            }
            catch (JsonException ex)
            {
                // Manejo de error de deserialización JSON
            }
            catch (Exception ex)
            {
                // Otros errores
            }

            return posts;
        }
    }
}
