using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using PaymentsGenerator.Model;
using System.Windows.Input;
using PaymentsGenerator.ViewModelCommandsAndBehaviors;
using PaymentsGenerator.Windows;
using System.Windows.Threading;
using PaymentsGenerator.ModelCommands;
using System.Collections;

namespace PaymentsGenerator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public delegate void UpdateOutputMsg(string textMsg);

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Properties
        public ObservableCollection<Log> ElixirOutputMsgs { get; set; }       
        private bool isElixirGenerated;
        public bool IsElixirGenerated { get { return isElixirGenerated; } set { isElixirGenerated = value; OnPropertyChanged("IsElixirGenerated"); } }
        private IList<account> accounts;
        public IList<account> Accounts { get { return accounts; } set { accounts = value; OnPropertyChanged("Accounts"); } }
        private bool noOfSelectedAcntIsValid = true;
        public bool NoOfSelectedAcntIsValid { get { return noOfSelectedAcntIsValid; } set { noOfSelectedAcntIsValid = value; OnPropertyChanged("NoOfSelectedAcntIsValid"); } }       
        private int selectedCount;
        public int SelectedCount { get { return selectedCount; } set { selectedCount = value; OnPropertyChanged("SelectedCount"); } }
        private int totalCount;
        public int TotalCount { get { return totalCount; } set { totalCount = value; OnPropertyChanged("TotalCount"); } }
        private bool fastGeneration = true;
        public bool FastGeneration
        {
            get { return fastGeneration; }
            set { 
                fastGeneration = value; 
                OnPropertyChanged("FastGeneration");
                NoOfSelectedAcntIsValid = value;
                if (!fastGeneration)
                {
                    Accounts = localCache.Accounts();
                    if (Accounts != null)
                    {
                        TotalCount = Accounts.Count;
                    }
                }
                else
                {
                    Accounts = null;
                }
            }
        }
        private ObservableCollection<account> selectedAccounts = new ObservableCollection<account>();
        public ObservableCollection<account> SelectedAccunts
        {
            get { return selectedAccounts; }
            set { selectedAccounts = value; OnPropertyChanged("SelectedAccounts"); }
        }
        
        public ICommand ElxGenCmd { get; set; }
        public ICommand SelectionChangedCmd { get; set; }
        public ICommand ShowFindSqlServerCmd { get; set; }
        #endregion

        #region Events' implementation
        private void ElxGenClicked()
        {
            int noOfFilesParam = 0, noOfRecordsParam = 0, noOfAcntParam = noOfAcnt;
            string fileNameParam = expressFileName;
            ModalWithFilesCountViewModel vm = new ModalWithFilesCountViewModel();
            ModalWithFilesCount mwfc = new ModalWithFilesCount(vm);
            if (mwfc.ShowDialog().HasValue)
            {
                if (int.TryParse(vm.FilesCount, out noOfFilesParam) && int.TryParse(vm.RowsCount, out noOfRecordsParam))
                {
                    //mnoze przez 8 poniewaz ma to wplyw na wyliczana kwote w platnosciach
                    noOfRecordsParam *= 8;
                }
            }
            if (!fastGeneration)
            {
                fileNameParam = Utils.ExportToCsv(selectedAccounts);
                noOfAcntParam = selectedCount;
            }

            ElixirGenThread egt = new ElixirGenThread(this, noOfFilesParam, noOfRecordsParam, fileNameParam, noOfAcntParam);
            egt.Start();
            ElixirOutputMsgs.Add(new Log() { LogMsg = "Skrypt uruchomiony", LogTime = DateTime.Now });
        }
        private void ShowFindSqlServerClicked()
        {
            ModalFindSqlServer mfss = new ModalFindSqlServer();
            mfss.Show();
        }
        private void SelectionChanged(object param)
        {
            var list = param as IList;

            selectedAccounts.Clear();
            foreach (account item in list)
                selectedAccounts.Add(item);

            SelectedCount = selectedAccounts.Count;
            NoOfSelectedAcntIsValid = SelectedCount >= 8;
        }

        #endregion

        public void AddOutputMsg(string textMsg)
        {
            MainWindowViewModel.UpdateOutputMsg deleg = this.AddOutputMsgFromDiffThread;
            mainWindowDispatcher.Invoke(deleg, textMsg);
        }
       
        public MainWindowViewModel()
        {
            ElixirOutputMsgs = new ObservableCollection<Log>();
            ElxGenCmd = new RelayCommand(pars => ElxGenClicked());
            ShowFindSqlServerCmd = new RelayCommand(pars => ShowFindSqlServerClicked());
            SelectionChangedCmd = new RelayCommand(pars => SelectionChanged(pars));
        }

       

       

        public MainWindowViewModel(Dispatcher Dispatcher) :this()
        {
            this.mainWindowDispatcher = Dispatcher;
        }

        private void AddOutputMsgFromDiffThread(string txt)
        {
            ElixirOutputMsgs.Add(new Log() { LogMsg = txt, LogTime = DateTime.Now });
            IsElixirGenerated = true;
        }

        private const string expressFileName = "rach.csv";
        private const int noOfAcnt = 133061;
        private Dispatcher mainWindowDispatcher;
        private InvokerAccountCache localCache = InvokerAccountCache.Instance();

    }
}
