using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;

namespace AppLocker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string steamPath = @"C:\Program Files\Steam\steam.exe",
                             steamSavePath = @"C:\Program Files\Steam\DataChunk",
                             r6Path1 = @"C:\Program Files (x86)\Steam\steamapps\common\Tom Clancy's Rainbow Six Siege\RainbowSix.exe",
                             r6Path2 = @"C:\Program Files (x86)\Steam\steamapps\common\Tom Clancy's Rainbow Six Siege\RainbowSix_Vulkan.exe",
                             r6SavePath1 = @"C:\Program Files (x86)\Steam\steamapps\common\Tom Clancy's Rainbow Six Siege\DataChunk1",
                             r6SavePath2 = @"C:\Program Files (x86)\Steam\steamapps\common\Tom Clancy's Rainbow Six Siege\DataChunk2";


        private static int steamExists = 0,
                           r6Exists = 0;
        private const int EXIST = 2;
        private const int NOT_EXIST = 1;

        public static void LockSteam()
        {
            TransferBinaries(steamPath, steamSavePath);
        }
        public static void ReleaseSteam()
        {
            TransferBinaries(steamSavePath, steamPath);
        }

        public static void LockR6()
        {
            TransferBinaries(r6Path1, r6SavePath1);
            TransferBinaries(r6Path2, r6SavePath2);
        }
        public static void ReleaseR6()
        {
            TransferBinaries(r6SavePath1, r6Path1);
            TransferBinaries(r6SavePath2, r6Path2);
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

            if (App.steamExists == App.NOT_EXIST)  // fall back to find steam from registry.
            {
                FindSteam();
                App.steamExists = File.Exists(steamPath) ? EXIST : NOT_EXIST;  // Check again
            }
            return steamExists == App.EXIST;
        }

        public static void FindSteam()
        {
            try
            {
                RegistryKey key1 = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");

                if (key1 != null)
                {
                    String exePath = key1.GetValue("SteamExe").ToString();
                    String dirPath = key1.GetValue("SteamPath").ToString();
                    steamPath = exePath;
                    steamSavePath = dirPath + "/DataChunk";
                }
                
            }
            catch (Exception ex)
            {
                // Just do nothing ,,, for now
            }
        }

        public static String GetSteamPath()
        {
            return steamPath;
        }
        
        public static bool CheckR6Exist()
        {
            if (App.r6Exists == 0)
            {
                App.r6Exists = File.Exists(r6Path1) && File.Exists(r6Path2) ? EXIST : NOT_EXIST;
            }
            return r6Exists == App.EXIST;
        }
    }
}
