using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppApi.Models;
using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;

namespace TbcTestAppApi.Middleware
{
    public class AcceptLanguageMiddleware
    {
        private readonly RequestDelegate _next;
        public AcceptLanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var currentCulture = httpContext.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
            var defaultCulture = CultureInfo.CurrentCulture.ToString();

            if (!string.IsNullOrWhiteSpace(currentCulture))
            {
                var checkCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).Any(p => string.Equals(p.Name, currentCulture, StringComparison.CurrentCultureIgnoreCase));
                if (!checkCulture)
                {
                    currentCulture = defaultCulture;
                }
            }
            else
            {
                currentCulture = defaultCulture;
            }

            var cultureInfo = new CultureInfo(currentCulture);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            await _next(httpContext);

            return;


        }
    }
}
