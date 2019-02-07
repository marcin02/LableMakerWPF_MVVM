using LabelMakerWPF_Plain.ViewModels;
using System.Windows.Controls;

namespace LabelMakerWPF_Plain.Views
{
    /// <summary>
    /// Interaction logic for BigBoxView.xaml
    /// </summary>
    public partial class BigBoxView : UserControl
    {
        public BigBoxView()
        {
            InitializeComponent();
            DataContext = new BigBoxViewModel();
        }
    }
}
