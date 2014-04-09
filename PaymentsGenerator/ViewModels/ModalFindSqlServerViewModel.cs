using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Data.Sql;
using System.Data;
using PaymentsGenerator.ViewModelCommandsAndBehaviors;
using PaymentsGenerator.ModelCommands;

namespace PaymentsGenerator.ViewModels
{
    public class ModalFindSqlServerViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Properties
        private string connString = "connection string";

        public string ConnString
        {
            get { return connString; }
            set { connString = value; OnPropertyChanged("ConnString"); }
        }

        private ObservableCollection<string> sqlInstaces = new ObservableCollection<string>() { "instace1", "instace2" };

        public ObservableCollection<string> SqlInstances
        {
            get { return sqlInstaces; }
            set { sqlInstaces = value;  }
        }

        private ObservableCollection<string> sqlCatalogs = new ObservableCollection<string>() { "catalog1", "catalog2" };
        private string selectedCatalog;
        private string selectedInstance;

        public ObservableCollection<string> SqlCatalogs
        {
            get { return sqlCatalogs; }
            set { sqlCatalogs = value; }
        }


        public ICommand SqlInstanceChangedCmd
        {
            get; set;
            
        }

        public ICommand SqlCatalogChangedCmd
        {
            get;
            set;
        }

        public ICommand OkCmd
        {
            get;
            set;
        }

        public ICommand CancelCmd
        {
            get;
            set;
        }
        #endregion

        public ModalFindSqlServerViewModel()
        {
            SqlInstanceChangedCmd = new RelayCommand(pars => EventInstanceChanged(pars.ToString()));
            SqlCatalogChangedCmd = new RelayCommand(pars => EventCatalogChanged(pars.ToString()));          
            OkCmd = new RelayCommand(pars => OkClicked());
            CancelCmd = new RelayCommand(pars => CancelClicked());
            BackgroundWorker sqlInstancesWorker = new BackgroundWorker();
            sqlInstancesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(sqlInstancesWorker_RunWorkerCompleted);
            sqlInstancesWorker.DoWork += new DoWorkEventHandler(sqlInstancesWorker_DoWork);
            sqlInstancesWorker.RunWorkerAsync();
        }
        //TODO: AccountCache datasource
        private EnumerableRowCollection<DataRow> datasource;
        #region Events' implementation
        private void EventCatalogChanged(string msg)
        {
            selectedCatalog = msg;
            ConnString = "Connection string: " + InvokerAccountCache.Instance().Do(selectedInstance, selectedCatalog);            
        }
        
        private void EventInstanceChanged(string msg)
        {
            selectedInstance = msg;
            if (datasource != null)
            {
                sqlCatalogs.Clear();
                var catalogs = (from row in datasource where row[0].ToString() == selectedInstance && !string.IsNullOrEmpty(row[1].ToString()) select row[1]);
                foreach (var item in catalogs)
                {
                    sqlCatalogs.Add(item.ToString());
                }
            }
            
        }
        
        private void OkClicked()
        {
        }

        private void CancelClicked()
        {
            InvokerAccountCache.Instance().Undo();
        }
        #endregion

        #region Background work
        void sqlInstancesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SqlDataSourceEnumerator.Instance.GetDataSources().AsEnumerable();            
        }

        void sqlInstancesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            datasource = e.Result as EnumerableRowCollection<DataRow>;
            if (datasource != null)
            {
                var uniqueSources = (from row in datasource where !string.IsNullOrEmpty(row[1].ToString()) select row[0]).Distinct();
                if (uniqueSources != null)
                    foreach (var item in uniqueSources)
                    {
                        sqlInstaces.Add(item.ToString());
                    }
            }
        }
        #endregion
    }
}
