using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalFramework.Logger
{
    public class CrystalLogger
    {
        internal void Log(LogType type, string message)
        {
            Log($"{nameof(type)}: {message}");
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
