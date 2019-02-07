using LabelMakerWPF_Plain.ViewModels;
using System.Windows;

namespace LabelMakerWPF_Plain
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        #endregion   
    }
}
