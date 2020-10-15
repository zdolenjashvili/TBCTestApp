using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppApi.Models;
using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;

namespace TbcTestAppApi.Middleware
{
    public class ExeptionLogMiddleware
    {
        private readonly RequestDelegate _next;
        public ExeptionLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ApplicationDBContext db)//, ISecurity security
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                    
                    // ლოგირება ბაზაში
                    await db.UnHandledExeptionLog.AddAsync(new UnHandledExeptionLog
                    {
                        ExeptionMassage = $"errorMessage: { ex.Message }; StackTrace: { ex.StackTrace }; InnerException: { ex.InnerException }",
                        DateCreated = DateTime.Now
                    }); 

                    await db.SaveChangesAsync();
                    await db.DisposeAsync();

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ExeptionModel
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Exeption= "მოხდა გაუთვალიწინებელი შეცდომა."
                }));
            }
            
            return;


        }
    }
}
