using LabelMakerWPF_Plain.ViewModels;
using System.Windows.Controls;


namespace LabelMakerWPF_Plain.Views
{
    /// <summary>
    /// Interaction logic for BoxView.xaml
    /// </summary>
    public partial class BoxView : UserControl
    {
        public BoxView()
        {
            InitializeComponent();
            DataContext = new BoxViewModel();
        }
    }
}
