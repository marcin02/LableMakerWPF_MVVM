using LabelMakerWPF_Plain.ViewModels;
using System.Windows.Controls;

namespace LabelMakerWPF_Plain.Views
{
    /// <summary>
    /// Interaction logic for CustomView.xaml
    /// </summary>
    public partial class CustomView : UserControl
    {
        public CustomView()
        {
            InitializeComponent();
            DataContext = new CustomViewModel();
        }
    }
}
