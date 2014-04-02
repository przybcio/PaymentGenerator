using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Threading;

namespace PaymentsGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void UpdateContext(string text);

        public MainWindowModel MyModel { get; set; }

        public void MainWindowDeleg(string msgContext)
        {
            MyModel.ElixirOutputMsgs.Add(new Log() { LogMsg = msgContext, LogTime = DateTime.Now });
            MyModel.IsElixirGenerated = true;

        }

        public MainWindow()
        {
            MyModel = new MainWindowModel();      
            InitializeComponent();                 
            this.DataContext = MyModel; 
            
        }

        private void elxGenBtn_Click(object sender, RoutedEventArgs e)
        {
            int noOfFilesParam = 0, noOfRecordsParam = 0, noOfAcntParam = noOfAcnt;
            string fileNameParam = expressFileName;
            ModalWithFilesCount mwfc = new ModalWithFilesCount();
            if (mwfc.ShowDialog().HasValue)
            {

                if (int.TryParse(mwfc.noOfFilesTb.Text, out noOfFilesParam) && int.TryParse(mwfc.noOfRecordsTb.Text, out noOfRecordsParam))
                {
                    //mnoze przez 8 poniewaz ma to wplyw na wyliczana kwote w platnosciach
                    noOfRecordsParam *= 8;
                }
            }
            if (fastGenCB.IsChecked.HasValue && fastGenCB.IsChecked.Value == false)
            {
                fileNameParam = Utils.ExportToCsv(this.accountDataGrid.SelectedItems);            
                noOfAcntParam = this.accountDataGrid.SelectedItems.Count;
            }

            ElixirGenThread egt = new ElixirGenThread(this, noOfFilesParam, noOfRecordsParam, fileNameParam, noOfAcntParam);
            egt.Start();
            MyModel.ElixirOutputMsgs.Add(new Log() { LogMsg = "Skrypt uruchomiony", LogTime = DateTime.Now });
        }

        private void accountDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyModel.SelCount = this.accountDataGrid.SelectedItems.Count;
            MyModel.NoOfSelectedAcntIsValid = MyModel.SelCount  >= 8;
        }

        private void fastGenCB_Unchecked(object sender, RoutedEventArgs e)
        {
            MyModel.NoOfSelectedAcntIsValid = false;
            MyModel.Accounts = localCache.GetAccounts();
            MyModel.TotalCount = MyModel.Accounts.Count;
        }

        private void fastGenCB_Checked(object sender, RoutedEventArgs e)
        {
            MyModel.NoOfSelectedAcntIsValid = true;
            MyModel.Accounts = null;
        }

        private void elxExpBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private Cache localCache = Cache.Instance();
        private const string expressFileName = "rach.csv";
        private const int noOfAcnt = 133061;

        private void sqlConnBtn_Click(object sender, RoutedEventArgs e)
        {
            ModalSqlServerInstance mssi = new ModalSqlServerInstance();
            mssi.ShowDialog();
        }

        
    }
}
