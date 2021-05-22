using CrystalFramework.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalFramework.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogInfo(this CrystalLogger logger, string message)
        {
            logger.Log(LogType.Info, message);
        }

        public static void LogWarning(this CrystalLogger logger, string message)
        {
            logger.Log(LogType.Warning, message);
        }

        public static void LogError(this CrystalLogger logger, string message)
        {
            logger.Log(LogType.Error, message);
        }
    }
}
