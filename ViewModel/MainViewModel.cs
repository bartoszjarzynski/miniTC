using MiniTC.ViewModel.BaseClass;
using System.Windows;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    class MainViewModel: ViewModelBase
    {
        public PanelTCViewModel Panel1 { get; set; }
        public PanelTCViewModel Panel2 { get; set; }

        Model.Kopiowanie copying;
        public MainViewModel()
        {
            copying = new Model.Kopiowanie();
            Panel1 = new PanelTCViewModel(copying, copying.Path1, 1);
            Panel2 = new PanelTCViewModel(copying, copying.Path2, 2);
        }

        private ICommand kopiuj;
        public ICommand Kopiuj
        {

            get
            {
                return kopiuj ?? (kopiuj = new RelayCommand(
                    p => {
                        try
                        {
                            copying.ExecuteCopying();
                            Panel1.Sciezka = Panel1.Sciezka;
                            Panel2.Sciezka = Panel2.Sciezka;
                        }
                        catch
                        {
                            MessageBox.Show("Cannot copy a folder.", "Error!");
                            copying.Panel = 0;
                        }
                        
                    },
                    canExecute: p => copying.CanCopy()
                    ));
            }
        }
    }
}
