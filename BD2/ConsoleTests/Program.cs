using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Components;
using UI.Managers;

namespace ConsoleTests
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Common.Encryption.Encrypt("root");

        }
    }
}
