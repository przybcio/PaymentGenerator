using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace PaymentsGenerator.ViewModels
{
    public class MainWindowModel : INotifyPropertyChanged
    {

        public ObservableCollection<Log> ElixirOutputMsgs { get; set; }       
        private bool isElixirGenerated;
        public bool IsElixirGenerated { get { return isElixirGenerated; } set { isElixirGenerated = value; OnPropertyChanged("IsElixirGenerated"); } }
        private IList<account> accounts;
        public IList<account> Accounts { get { return accounts; } set { accounts = value; OnPropertyChanged("Accounts"); } }
        private bool noOfSelectedAcntIsValid;
        public bool NoOfSelectedAcntIsValid { get { return noOfSelectedAcntIsValid; } set { noOfSelectedAcntIsValid = value; OnPropertyChanged("NoOfSelectedAcntIsValid"); } }       

        public event PropertyChangedEventHandler PropertyChanged;
        
        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private int selCount;
       
        public int SelCount { get { return selCount; } set { selCount = value; OnPropertyChanged("SelCount"); } }

        private int totalCount;
        public int TotalCount { get { return totalCount; } set { totalCount = value; OnPropertyChanged("TotalCount"); } }


        public MainWindowModel()
        {
            ElixirOutputMsgs = new ObservableCollection<Log>();
        }
        
    }
}
