using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace launchpad_backend.Helper
{
    public class InputVariableHelper
    {
        public static void ValidateInputs()
        {

            string serverNode = GetServernode();
            string database = GetDatabase();
            string uid = GetUid();
            string pwd = GetPwd();
            string loggeruid = LoggerGetUid();
            string loggerpwd = LoggerGetPwd();
            string loggerdb = LoggerGetDb();
            if (string.IsNullOrEmpty(serverNode))
            {
                Environment.SetEnvironmentVariable("SERVERNODE", "KOB04187.koehlerpaper.com");
                Console.WriteLine("Variable SERVERNODE not provided. Using the default Test System KOB04187.koehlerpaper.com");
            }
            if (string.IsNullOrEmpty(database))
            {
                Environment.SetEnvironmentVariable("DB", "APP_DEV_LAUNCHPAD");
                Console.WriteLine("Variable DB not provided. Using the default System APP_DEV_LAUNCHPAD");
            }
            if (string.IsNullOrEmpty(uid))
            {
                throw new Exception("Envionment Variable UID not provided");
            }
            if (string.IsNullOrEmpty(pwd))
            {
                throw new Exception("Envionment Variable PWD not provided");
            }
            if (string.IsNullOrEmpty(loggeruid))
            {
                throw new Exception("Envionment Variable LOGGERUID not provided");
            }
            if (string.IsNullOrEmpty(loggerpwd))
            {
                throw new Exception("Envionment Variable LOGGERPWD not provided");
            }
            if (string.IsNullOrEmpty(loggerdb))
            {
                throw new Exception("Envionment Variable LOGGERDB not provided");
            }
        }

        public static string GetServernode()
        {
            return Environment.GetEnvironmentVariable("SERVERNODE");
        }

        public static string GetDatabase()
        {
            return Environment.GetEnvironmentVariable("DB");
        }

        public static string GetUid()
        {
            return Environment.GetEnvironmentVariable("UID");
        }
        public static string GetPwd()
        {
            return Environment.GetEnvironmentVariable("PWD");
        }
        public static string LoggerGetUid()
        {
            return Environment.GetEnvironmentVariable("LOGGERUID");
        }
        public static string LoggerGetPwd()
        {
            return Environment.GetEnvironmentVariable("LOGGERPWD");
        }
        public static string LoggerGetDb()
        {
            return Environment.GetEnvironmentVariable("LOGGERDB");
        }
    }
}
