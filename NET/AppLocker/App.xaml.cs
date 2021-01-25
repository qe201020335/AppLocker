using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace AppLocker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string steamPath = @"C:\Program Files (x86)\Steam\steam.exe",
                             savePath = @"C:\Program Files (x86)\Steam\DataChunk";

        private static int steamExists = 0;
        private const int EXIST = 2;
        private const int NOT_EXIST = 1;

        public static void LockApp()
        {
            TransferBinaries(steamPath, savePath);
        }

        public static void ReleaseApp()
        {
            TransferBinaries(savePath, steamPath);
        }

        public static void TransferBinaries(string src, string des)
        {
            var binaries = File.ReadAllBytes(src);
            File.WriteAllBytes(des, binaries);
            File.Delete(src);
        }

        public static bool CheckSteamExist()
        {
            if (App.steamExists == 0)
            {
                App.steamExists = File.Exists(steamPath) ? EXIST : NOT_EXIST;
            }
            return steamExists == App.EXIST;
        }
    }
}
