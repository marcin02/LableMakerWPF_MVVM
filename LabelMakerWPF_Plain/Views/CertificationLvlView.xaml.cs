using LabelMakerWPF_Plain.ViewModels;
using System.Windows.Controls;

namespace LabelMakerWPF_Plain.Views
{
    /// <summary>
    /// Interaction logic for CertificationLvlView.xaml
    /// </summary>
    public partial class CertificationLvlView : UserControl
    {
        public CertificationLvlView()
        {
            InitializeComponent();
            DataContext = new CertificationLvlViewModel();
        }
    }
}
