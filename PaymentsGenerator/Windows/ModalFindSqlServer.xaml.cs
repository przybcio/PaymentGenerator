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
using System.Windows.Shapes;
using PaymentsGenerator.ViewModels;

namespace PaymentsGenerator.Windows
{
    /// <summary>
    /// Interaction logic for ModalFindSqlServer.xaml
    /// </summary>
    public partial class ModalFindSqlServer : Window
    {
        public ModalFindSqlServer()
        {
            InitializeComponent();
            //TODO: IoC here
            this.DataContext = new ModalFindSqlServerViewModel();
        }
    }
}
