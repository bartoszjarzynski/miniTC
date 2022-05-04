using System.Collections.Generic;
using System.IO;

namespace MiniTC.Model
{
    static class Lokalizacje
    {
        public static List<string> AvailableDrives()
        {
            List<string> drives = new List<string>();
            drives.AddRange(Directory.GetLogicalDrives());
            return drives;
        }

        public static string FolderFileConnect(string path,string file)
        {
            string x = "";
            if (path.Length > 3)
            {
                x = @"\";
            }
            return path + x + file;
        }

        public static bool IfFolder(string name)
        {
            if (name == ".." || name.Remove(3) == "<D>")
                return true;
            else
                return false;
        }
    }
}