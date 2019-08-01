
using launchpad_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;

namespace launchpad_backend.Services
{
    public class LogginService
    {
        public static void InsertItem(SqlServerLoggingItem item)
        {
            using (LoggerContext ctx = new LoggerContext())
            {
                try
                {
                    var x = item.Api?? "";
                    ctx.Database.ExecuteSqlCommand($"INSERT INTO BACKEND_LOG(TST,USERNAME,API,APP,ENDPOINT,QUERY,TIMING,STATUSCODE) " +
                        $"VALUES (CURRENT_TIMESTAMP,@Username,@Api,@App,@Endpoint,@Query,@Timing,@StatusCode)",
                        new SqlParameter("@Username", item.Username ?? ""),
                        new SqlParameter("@Api", item.Api ?? ""),
                        new SqlParameter("@App", item.App ?? ""),
                        new SqlParameter("@Endpoint", item.Endpoint ?? ""),
                        new SqlParameter("@Query", item.Query ?? ""),
                        new SqlParameter("@Timing", item.Timing),
                        new SqlParameter("@StatusCode", item.StatusCode));                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to insert log entry: {ex}");
                }

            }
        }
    }
}
