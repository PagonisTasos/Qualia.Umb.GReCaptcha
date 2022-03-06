using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualia.Umb.GReCaptcha
{
    internal class SettingsManager
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMemoryCache memoryCache;
        private const string settingsFolder = @"App_Plugins\Qualia.Umb.GReCaptcha\settings";
        private const string CAPTCHA_CACHE_KEY = "__captcha_keys__";

        public SettingsManager(IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.memoryCache = memoryCache;
        }

        public void SaveKeysToFile(CaptchaKeys keys)
        {
            var filepath = System.IO.Path.Combine(webHostEnvironment.ContentRootPath, $@"{settingsFolder}\keys.json");
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(keys));
            memoryCache.Remove(CAPTCHA_CACHE_KEY);
        }

        public CaptchaKeys ReadKeys()
        {
            return memoryCache.GetOrCreate(CAPTCHA_CACHE_KEY, (_) => ReadKeysFromFile());
        }

        private CaptchaKeys ReadKeysFromFile()
        {
            var filepath = System.IO.Path.Combine(webHostEnvironment.ContentRootPath, $@"{settingsFolder}\keys.json");
            var fileContent = System.IO.File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<CaptchaKeys>(fileContent);
        }

        public string GetCaptchaHelperJsScript()
        {
            var filepath = System.IO.Path.Combine(webHostEnvironment.ContentRootPath, $@"{settingsFolder}\captcha-helper.js");
            return System.IO.File.ReadAllText(filepath).Replace("reCAPTCHA_site_key", ReadKeys().sitekey);
        }
    }
}
