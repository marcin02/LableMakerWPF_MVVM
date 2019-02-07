
using LabelMakerWPF_Plain.ViewModels;
using System.Windows.Controls;

namespace LabelMakerWPF_Plain.Views
{
    /// <summary>
    /// Interaction logic for CertificationNominateView.xaml
    /// </summary>
    public partial class CertificationNominateView : UserControl
    {
        public CertificationNominateView()
        {
            InitializeComponent();
            DataContext = new CertificationNominateViewModel();
        }
    }
}
