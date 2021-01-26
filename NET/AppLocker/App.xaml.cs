using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace AppLocker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string SUFFIX = ".applock";  // suffix to add for locked files

        private static string steamDir = "C:/Program Files/Steam"; // for testing purpose, x86 is removed.
        private const string STEAM = "/steam.exe";
        
        private static string r6Dir = "C:/Program Files (x86)/Steam/steamapps/common/Tom Clancy's Rainbow Six Siege";
        private const string R6V = "/RainbowSix_Vulkan.exe";
        private const string R6 = "/RainbowSix.exe";
        private const string R6ID = "359550";
            
        private static int steamExists = 0,
                           r6Exists = 0;
        private const int EXIST = 2;
        private const int NOT_EXIST = 1;

        public static void LockSteam()
        {
            TransferBinaries(steamDir+STEAM, steamDir+STEAM+SUFFIX);
        }
        public static void ReleaseSteam()
        {
            TransferBinaries(steamDir+STEAM+SUFFIX, steamDir+STEAM);
        }

        public static void LockR6()
        {
            TransferBinaries(r6Dir+R6, r6Dir+R6+SUFFIX);
            TransferBinaries(r6Dir+R6V, r6Dir+R6V+SUFFIX);
        }
        public static void ReleaseR6()
        {
            TransferBinaries(r6Dir+R6+SUFFIX, r6Dir+R6);
            TransferBinaries(r6Dir+R6V+SUFFIX, r6Dir+R6V);
        }

        private static void TransferBinaries(string src, string des)
        {
            var binaries = File.ReadAllBytes(src);
            File.WriteAllBytes(des, binaries);
            File.Delete(src);
        }

        public static bool CheckSteamExist()
        {
            if (App.steamExists == 0)
            {
                App.steamExists = File.Exists(steamDir+STEAM) ? EXIST : NOT_EXIST;
            }

            if (App.steamExists == App.NOT_EXIST)  // fall back to find steam from registry.
            {
                FindSteam();
                App.steamExists = File.Exists(steamDir+STEAM) ? EXIST : NOT_EXIST;  // Check again
            }
            return steamExists == App.EXIST;
        }

        private static void FindSteam()
        {
            try
            {
                RegistryKey key1 = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");

                if (key1 != null)
                {
                   // String exePath = key1.GetValue("SteamExe").ToString();
                    String dirPath = key1.GetValue("SteamPath").ToString();
                    steamDir = dirPath;
                }
                
            }
            catch (Exception ex)
            {
                // Just do nothing ,,, for now
            }
        }
        
        public static bool CheckR6Exist()
        {
            if (App.r6Exists == 0)
            {
                App.r6Exists = File.Exists(r6Dir+R6) && File.Exists(r6Dir+R6V) ? EXIST : NOT_EXIST;
            }

            if (r6Exists == NOT_EXIST) // fall back to find R6 from steam libraries.
            {
                FindR6();
                App.r6Exists = File.Exists(r6Dir+R6) && File.Exists(r6Dir+R6V) ? EXIST : NOT_EXIST; // check again
            }
            return r6Exists == App.EXIST;
        }
        
        private static void FindR6()
        {
            if (!CheckSteamExist())
            {
                return;
            }
            List<string> libs = getSteamLibs();
            foreach (var lib in libs)
            {   
                if (File.Exists(lib+"/appmanifest_" + R6ID + ".acf"))
                {
                    r6Dir = lib + "/common/Tom Clancy's Rainbow Six Siege";
                    return;
                }
            }
        }

        private static List<string> getSteamLibs()
        {
            List<string> libs = new List<string>();
            if (!CheckSteamExist())
            {
                return libs;
            } 
            libs.Add(steamDir+"/steamapps");
            string[] configLines = File.ReadAllLines(steamDir + "/steamapps/libraryfolders.vdf");
            foreach (var item in configLines)
            {
                Match match = Regex.Match(item, @"[A-Z]:\\");
                if (item != string.Empty && match.Success)
                {
                    string matched = match.ToString();
                    string item2 = item.Substring(item.IndexOf(matched));
                    item2 = item2.Replace("\\\\", "/");
                    item2 = item2.Replace("\"", "/steamapps");
                    libs.Add(item2);
                }
            }
            return libs;
        }
        
        public static String GetSteamPath()
        {
            return steamDir;
        }
        
        public static String GetR6Path()
        {
            return r6Dir;
        }
    }
}
