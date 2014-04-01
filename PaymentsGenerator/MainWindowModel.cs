using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PaymentsGenerator
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private string elixirOutputMsg;
        public string ElixirOutputMsg { get { return elixirOutputMsg; } set { elixirOutputMsg = value; OnPropertyChanged("ElixirOutputMsg"); } }
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
    }
}
