using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLocker
{
    class Constants
    {
        public static string steamPath = @"C:\Program Files (x86)\Steam\steam.exe",
                             savePath = @"C:\Program Files (x86)\Steam\DataChunk",
                             r6path1 = @"C:\Program Files (x86)\Steam\steamapps\common\Tom Clancy's Rainbow Six Siege\RainbowSix.exe",
                             r6path2 = @"C:\Program Files (x86)\Steam\steamapps\common\Tom Clancy's Rainbow Six Siege\RainbowSix_Vulkan.exe";

        public enum Game
        {
            Steam, R6
        }

        private enum ExistStatus
        {
            NA, Exists, Locked
        }

    }


}
