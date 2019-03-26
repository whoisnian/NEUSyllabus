using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Services
{
    public class GetCoursesService
    {
        public async Task<List<Course>> LoginAndGetCoursesAsync()
        {
            var handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:66.0) Gecko/20100101 Firefox/66.0");
            string url = "http://219.216.96.4/eams/loginExt.action";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            return new List<Course>();
        }
    }
}
