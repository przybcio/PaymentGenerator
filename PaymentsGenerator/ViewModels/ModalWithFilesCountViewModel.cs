using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using PaymentsGenerator.ViewModelCommandsAndBehaviors;

namespace PaymentsGenerator.ViewModels
{
    public class ModalWithFilesCountViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private string filesCount;

        public string FilesCount
        {
            get { return filesCount; }
            set { filesCount = value; OnPropertyChanged("FilesCount"); }
        }

        private string rowsCount;

        public string RowsCount
        {
            get { return rowsCount; }
            set { rowsCount = value; OnPropertyChanged("RowsCount"); }
        }


        public ICommand OkCmd
        {
            get;
            set;
        }


        public ModalWithFilesCountViewModel()
        {
            OkCmd = new RelayCommand(pars => OkClicked());
        }
        private void OkClicked()
        {
        }

    }
}
