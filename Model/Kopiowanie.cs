using System.IO;

namespace MiniTC.Model
{
    class Kopiowanie
    {
        public Panel.Activity Panel { get; set; }
        public Sciezka Path1 { get; set; }
        public Sciezka Path2 { get; set; }

        public Kopiowanie()
        {
            Path1 = new Sciezka();
            Path2 = new Sciezka();
            Panel = 0;
        }

        public void ExecuteCopying()
        {
            if (Panel == (Panel.Activity)1)
            { 
                File.Copy(Path1.PathTarget(),Lokalizacje.FolderFileConnect(Path2.Path, Path1.WybranyPlik), true);
            }
            else
            {
                File.Copy(Path2.PathTarget(), Lokalizacje.FolderFileConnect(Path1.Path, Path2.WybranyPlik), true);
            }
            Panel = 0;
        }
        public bool CanCopy()
        {
            if (Panel != 0 && Path1.Path != null && Path2.Path !=null)
                return true;
            else
                return false;
        }

    }
}
