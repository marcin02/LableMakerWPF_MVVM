using LabelMakerWPF_Plain.Models;
using LabelMakerWPF_Plain.PrinterSettings;
using LabelMakerWPF_Plain.Properties;
using LabelMakerWPF_Plain.StartUp;
using LabelMakerWPF_Plain.Tools;


namespace LabelMakerWPF_Plain.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Constructor

        public MainWindowViewModel()
        {
            OnStartUp onStartUp = new OnStartUp();
            onStartUp.RunOnStartUp();
            ShowBoxCommand = new RelayCommand(ShowBox);
            ShowSettingsCommand = new RelayCommand(ShowSettings);
            ShowBigBoxCommand = new RelayCommand(ShowBigBox);
            ShowCustomCommand = new RelayCommand(ShowCustom);
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
        private bool _boxBtnCheck;
        private bool _smallBoxBtnCheck;
        private bool _bigBoxBtnCheck;
        private bool _certificationBtnCheck;
        private bool _certificationNominateBtnCheck;
        private bool _certificationLvlBtnCheck;
        private bool _customBtnCheck;

        #endregion

        #region Public properties

        public bool BoxBtnCheck
        {
            get { return _boxBtnCheck; }
            set { _boxBtnCheck = Box(value); OnPropertyChanged(nameof(BoxBtnCheck)); }
        }
        public bool SmallBoxBtnCheck
        {
            get { return _smallBoxBtnCheck; }
            set { _smallBoxBtnCheck = SmallBox(value); OnPropertyChanged(nameof(SmallBoxBtnCheck)); }
        }
        public bool BigBoxBtnCheck
        {
            get { return _bigBoxBtnCheck; }
            set { _bigBoxBtnCheck = BigBox(value); OnPropertyChanged(nameof(BigBoxBtnCheck)); }
        }
        public bool CertificationBtnCheck
        {
            get { return _certificationBtnCheck; }
            set { _certificationBtnCheck = Certification(value); OnPropertyChanged(nameof(CertificationBtnCheck));  }
        }
        public bool CertificationNominateBtnCheck
        {
            get { return _certificationNominateBtnCheck; }
            set { _certificationNominateBtnCheck = CertificationNominate(value); OnPropertyChanged(nameof(CertificationNominateBtnCheck)); }
        }
        public bool CertificationLvlBtnCheck
        {
            get { return _certificationLvlBtnCheck; }
            set { _certificationLvlBtnCheck = CertificationLvl(value); OnPropertyChanged(nameof(CertificationLvlBtnCheck)); }
        }
        public bool CustomBtnCheck
        {
            get { return _customBtnCheck; }
            set { _customBtnCheck = Custom(value); OnPropertyChanged(nameof(CustomBtnCheck)); }
        }
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
                UpdateMenu();
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        #endregion

        #region Methods

        private void UpdateMenu()
        {
            SmallBoxBtnCheck = true;
            BigBoxBtnCheck = true;
            CertificationNominateBtnCheck = true;
            CertificationLvlBtnCheck = true;
            CustomBtnCheck = true;
        }

        private bool Box(bool value)
        {
            if (value)
            {
                CertificationBtnCheck = false;
            }

            return value;
        }

        private bool SmallBox(bool value)
        {
            if (_selectedViewModel != null)
            {
                if (_selectedViewModel.ViewModelName == "BoxViewModel")
                {
                    value = true;
                    return value;
                }
                else
                {
                    value = false;
                    return value;
                } 
            }

            value = false;
            return value;
        }

        private bool BigBox(bool value)
        {
            if (_selectedViewModel != null)
            {
                if (_selectedViewModel.ViewModelName == "BigBoxViewModel")
                {
                    value = true;
                    return value;
                }
                else
                {
                    value = false;
                    return value;
                } 
            }

            value = false;
            return value;
        }

        private bool Certification(bool value)
        {
            if (value)
            {
                BoxBtnCheck = false;
            }

            return value;
        }

        private bool CertificationNominate(bool value)
        {
            if (_selectedViewModel != null)
            {
                if (_selectedViewModel.ViewModelName == "CertificationNominateViewModel")
                {
                    value = true;
                    return value;
                }
                else
                {
                    value = false;
                    return value;
                } 
            }

            value = false;
            return value;
        }

        private bool CertificationLvl(bool value)
        {
            if (_selectedViewModel != null)
            {
                if (_selectedViewModel.ViewModelName == "CertificationLvlViewModel")
                {
                    value = true;
                    return value;
                }
                else
                {
                    value = false;
                    return value;
                } 
            }

            value = false;
            return value;
        }

        private bool Custom(bool value)
        {     
                if (_selectedViewModel != null)
                {
                    if (_selectedViewModel.ViewModelName == "CustomViewModel")
                    {
                        BoxBtnCheck = false;
                        CertificationBtnCheck = false;
                        value = true;
                        return value;
                    }
                    else
                    {
                        value = false;
                        return value;
                    }
                }

                value = false;
                return value;
        }        

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
