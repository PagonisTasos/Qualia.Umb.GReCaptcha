using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qualia.Umb.GReCaptcha
{
    public class GReCatchaService
    {
        private readonly SettingsManager settingsManager;
        private readonly ILogger<GReCatchaService> logger;

        public GReCatchaService(IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache, ILogger<GReCatchaService> logger)
        {
            this.settingsManager = new SettingsManager(webHostEnvironment, memoryCache);
            this.logger = logger;
        }

        public string SiteKey => settingsManager.ReadKeys().sitekey; 
        private string _secretKey => settingsManager.ReadKeys().secretkey; 

        private const string _verification_endpoint = "https://www.google.com/recaptcha/api/siteverify";

        public HtmlString RenderJsScript()
        {
            return new HtmlString(
                    $"<script src=\"https://www.google.com/recaptcha/api.js?render={SiteKey}\"></script>" +
                    $"<script>{settingsManager.GetCaptchaHelperJsScript()}</script>"
                );
        }

        public async Task<bool> Verify(string response)
        {

            var c = new HttpClient();

            var gResp = await c.PostAsync(_verification_endpoint + $"?secret={_secretKey}&response={response}", null);

            gResp.EnsureSuccessStatusCode();
            var respBody = await gResp.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<ValidateCaptchaResponse>(respBody);

            if (!r.success)
            {
                logger.LogInformation("recaptcha failed with response: '{responseJson}'", respBody);
            }

            return r.success;
        }

        private class ValidateCaptchaResponse
        {
            public bool success { get; set; }
        }
    }

}
