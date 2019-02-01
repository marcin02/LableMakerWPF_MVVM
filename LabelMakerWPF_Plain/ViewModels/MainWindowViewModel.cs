using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.PrinterSettings;
using LabelMakerWPF_Plain.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabelMakerWPF_Plain.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Constructor

        public MainWindowViewModel()
        {
            ShowBoxCommand = new RelayCommand(ShowBox);
            ShowSettingsCommand = new RelayCommand(ShowSettings);
            ShowBigBoxCommand = new RelayCommand(ShowBigBox);
            ShowCustomCommand = new RelayCommand(ShowCustom);
            SelectedViewModel = new SelectedViewModel { ViewModel = new BoxViewModel(), ViewModelName = nameof(BoxViewModel) };
            ShowCertificationNominateCommand = new RelayCommand(ShowCertificationNominate);
            ShowCertificationLvlCommand = new RelayCommand(ShowCertificationLvl);
        }

        #endregion

        #region Commands

        public RelayCommand ShowBoxCommand { get; private set; }
        public RelayCommand ShowBigBoxCommand { get; private set; }
        public RelayCommand ShowCertificationNominateCommand { get; private set; }
        public RelayCommand ShowCertificationLvlCommand { get; private set; }
        public RelayCommand ShowCustomCommand { get; private set; }
        public RelayCommand ShowSettingsCommand { get; private set; }

        #endregion

        #region Private properties

        private string _headerLable;
        private SelectedViewModel _selectedViewModel;

        #endregion

        #region Public properties

        public string HeaderLable
        {
            get { return _headerLable; }
            set {
                _headerLable = value;
                OnPropertyChanged(nameof(HeaderLable));
            }
        }
        public SelectedViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        #endregion

        #region UI Methods

        private void ShowBox(object obj)
        {
            SelectedViewModel = new SelectedViewModel { ViewModel = new BoxViewModel(), ViewModelName = nameof(BoxViewModel) };
            
        }

        private void ShowBigBox(object obj)
        {
            SelectedViewModel = new SelectedViewModel { ViewModel = new BigBoxViewModel(), ViewModelName = nameof(BigBoxViewModel) };
        }

        private void ShowCertificationNominate(object obj)
        {
            SelectedViewModel = new SelectedViewModel { ViewModel = new CertificationNominateViewModel(), ViewModelName = nameof(CertificationNominateViewModel) };
        }

        private void ShowCertificationLvl(object obj)
        {
            SelectedViewModel = new SelectedViewModel { ViewModel = new CertificationLvlViewModel(), ViewModelName = nameof(CertificationLvlViewModel) };
        }

        private void ShowCustom(object obj)
        {
            SelectedViewModel = new SelectedViewModel { ViewModel = new CustomViewModel(), ViewModelName = nameof(CustomViewModel) };
        }

        private void ShowSettings(object obj)
        {
            PSettings ps = new PSettings();
        }

        #endregion
    }
}
