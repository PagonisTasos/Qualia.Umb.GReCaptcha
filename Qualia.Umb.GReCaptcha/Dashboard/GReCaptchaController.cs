using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Qualia.Umb.GReCaptcha
{
    public class GReCaptchaController : UmbracoAuthorizedApiController
    {
        private readonly SettingsManager settingsManager;

        public GReCaptchaController(IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache)
        {
            this.settingsManager = new SettingsManager(webHostEnvironment, memoryCache);
        }

        [HttpGet]
        public ActionResult GetKeys()
        {
            CaptchaKeys keys = settingsManager.ReadKeys();
            return Ok(keys);
        }

        [HttpPost]
        public ActionResult SaveKeys([FromBody] CaptchaKeys keys)
        {
            settingsManager.SaveKeysToFile(keys);
            return NoContent();
        }
    }
}
