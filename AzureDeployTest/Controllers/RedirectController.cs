using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AzureDeployTest.Controllers
{
    using System.Text.RegularExpressions;
    using System.Web.Hosting;
    using Newtonsoft.Json;

    public class RedirectMappingContainer
    {
        [JsonProperty("items")]
        public List<RedirectMapping> Items { get; set; } = new List<RedirectMapping>();
    }

    public class RedirectMapping
    {
        [JsonProperty("template")]
        public string Template;
        [JsonProperty("redirectUrl")]
        public string RedirectUrl;

        public bool IsMatch(string originalUrl)
        {
            return Regex.IsMatch(originalUrl, Template);
        }
    }
    public class RedirectController : Controller
    {
        

        public RedirectMappingContainer LoadMappingsFromFIle()
        {
            string mappingsFilePath = HostingEnvironment.MapPath("~/App_Data/RedirectMappings.Json");
            if (string.IsNullOrWhiteSpace(mappingsFilePath))
            {
                throw new ApplicationException("Can't map path ~/App_Data/RedirectMappings.Json");
            }
            if (!System.IO.File.Exists(mappingsFilePath))
            {
                System.IO.File.Create(mappingsFilePath).Close();
            }

            string dataJson = System.IO.File.ReadAllText(mappingsFilePath);
            if (string.IsNullOrEmpty(dataJson))
            {
                return new RedirectMappingContainer();
            }
            return JsonConvert.DeserializeObject<RedirectMappingContainer>(dataJson);
        }

        // GET: Redirect
        public ActionResult Index(string original)
        {
            if (!string.IsNullOrEmpty(original))
            {
                var mappings = LoadMappingsFromFIle();
                var redirect = mappings.Items.FirstOrDefault(x => x.IsMatch(original));
                if (redirect != null)
                {
                    Response.RedirectPermanent(redirect.RedirectUrl);
                }
            }

            ViewBag.Original = original;
            return View();
        }
    }
}