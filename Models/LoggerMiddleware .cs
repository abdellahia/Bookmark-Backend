
using launchpad_backend.Helper;
using launchpad_backend.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace launchpad_backend.Models
{
    public class LoggerMiddleware
    {
        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            // Start the Timer using Stopwatch  
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {              
                try
                {
                    watch.Stop();
                    var req = context.Request;
                    var claims = context.User.Claims;
                    //Username
                    var user = KeyCloak.GetProperty(claims, ClaimTypes.Username);               
                    var app = KeyCloak.GetProperty(claims, ClaimTypes.Client);
                    var logEntry = new SqlServerLoggingItem()
                    {
                        Username = user,
                        Api = "Launchpad",
                        App = app,
                        Endpoint = req.Path,
                        Query = req.QueryString.Value,
                        Timing = (int)watch.ElapsedMilliseconds,
                        StatusCode = context.Response.StatusCode
                    };
                    Task.Run(() => LogginService.InsertItem(logEntry));
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Failed to log entry {ex}");
                }

                return Task.CompletedTask;
            });

            // Call the next delegate/middleware in the pipeline   
            return _next(context);
        }
    }
}
