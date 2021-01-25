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
        private const string steamPath = @"C:\Users\Lemon\Desktop\steam.exe",
                             savePath = @"C:\Users\Lemon\Desktop\DataChunk";
        public static String GetNewString() { return "YEET"; }

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
    }
}
