using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SignalRChat.Services
{
    public class SwishQRService
    {
        private HttpClient client;
        public SwishQRService()
        {
            client = new HttpClient();
        }


        public async Task<string> GetQRImage(string swishNr, string amount, string message)
        {
            client.BaseAddress = new Uri("https://mpc.getswish.net/qrg-swish/api/v1/");
            client.DefaultRequestHeaders.Add("content-type", "application/json"); 


            var str = new swishString();
            str.value = "meddelandet";
            str.editable = false;

            QRRequest req = new QRRequest
            {
                format = "png",
                message = str,
                size = 450
            };

            HttpResponseMessage resp = await client.PostAsJsonAsync("prefilled", req);
            Console.WriteLine("Resp received");
            resp.EnsureSuccessStatusCode();
            Console.WriteLine("Request OK");

            byte[] arr = await resp.Content.ReadAsByteArrayAsync();
            return "data:image/png;base64," + Convert.ToBase64String(arr);

        }

    }

    class QRRequest
    {
        public string format { get; set; }
        public swishString message { get; set; }
        public int size { get; set; }
    }

    class swishString
    {
        public string value { get; set; }
        public bool editable { get; set; }
    }
}