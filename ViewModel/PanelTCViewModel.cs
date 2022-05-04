using System.Collections.Generic;

namespace MiniTC.ViewModel
{
    using BaseClass;
    using MiniTC.Model;
    using System.Windows;
    using System.Windows.Input;

    class PanelTCViewModel : ViewModelBase
    {
        #region Zmienne prywatne

        private string path;
        private List<string> drives;
        private List<string> pathCapacity;
        private string drive;
        private int index = -1;
        private bool ListBoxChange = true;
        private Sciezka pathClass;
        private int number;
        private Kopiowanie toCopy;

        #endregion

        #region Konstruktor
        public PanelTCViewModel(Kopiowanie k, Sciezka s, int nr)
        {
            drives = new List<string>();
            pathCapacity = new List<string>();
            toCopy = k;
            pathClass = s;
            number = nr;
        }
        #endregion

        #region Definicje
        public string Sciezka
        {
            get { return path; }
            set
            {
                path = value;
                onPropertyChanged(nameof(Sciezka));
                pathClass.Path = path;
                try { ZawartoscSciezki = pathClass.Capacity(); }
                catch
                {
                    MessageBox.Show("Cannot reach a folder.", "Error!");
                    Sciezka = pathClass.GetBack();
                }
            }
        }


        public List<string> Napedy
        {
            get { return drives; }
            set { drives = value; onPropertyChanged(nameof(Napedy)); }
        }

        public List<string> ZawartoscSciezki
        {
            get { return pathCapacity; }
            set
            {
                Index = -1;
                ListBoxChange = false;
                pathCapacity = value;
                onPropertyChanged(nameof(ZawartoscSciezki));
                ListBoxChange = true;
                toCopy.Panel = 0;
            }
        }

        public string Dysk
        {
            get { return drive; }
            set { drive = value; onPropertyChanged(nameof(Dysk)); }
        }

        public int Index
        {
            get { return index; }
            set { index = value; onPropertyChanged(nameof(Index)); }
        }
        #endregion

        #region Wybieranie

        private ICommand sprawdzNapedy;
        public ICommand SprawdzNapedy
        {

            get
            {
                return sprawdzNapedy ?? (sprawdzNapedy = new RelayCommand(
                    p => { Napedy = Lokalizacje.AvailableDrives(); },
                    p => true
                    ));
            }
        }

        private ICommand wybranoNaped;
        public ICommand WybranoNaped
        {

            get
            {
                return wybranoNaped ?? (wybranoNaped = new RelayCommand(
                    p => { Sciezka = Dysk; },
                    p => true
                    ));
            }
        }

        private ICommand zmianaSciezki;
        public ICommand ZmianaSciezki
        {

            get
            {
                return zmianaSciezki ?? (zmianaSciezki = new RelayCommand(
                    p => {
                        pathClass.WybranyElement = Index;
                        if (Lokalizacje.IfFolder(ZawartoscSciezki[Index]))
                            Sciezka = pathClass.PathChange();
                    },
                    p => Index > -1
                    ));
            }
        }

        private ICommand focus;
        public ICommand Focus
        {

            get
            {
                return focus ?? (focus = new RelayCommand(
                    p =>
                    {
                        toCopy.Panel = (Panel.Activity)number;
                    },
                    p => ListBoxChange
                    ));
            }
        }

        #endregion

    }
}
