using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace launchpad_backend.Models
{
    public class SqlServerLoggingItem
    {        
        public string Username { get; set; }
        public string Api { get; set; }
        public string App { get; set; }
        public string Endpoint { get; set; }
        public string Query { get; set; }
        public int Timing { get; set; }

        public int StatusCode { get; set; }

        public SqlServerLoggingItem()
        {
        }

        public SqlServerLoggingItem(string username, string api, string app, string endpoint, string query, int timing, int statusCode)
        {            
            Username = username;
            Api = api;
            App = app;
            Endpoint = endpoint;
            Query = query;
            Timing = timing;
            StatusCode = statusCode;
        }
    }
}
