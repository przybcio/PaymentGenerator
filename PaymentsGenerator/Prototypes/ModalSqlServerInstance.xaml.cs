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
using System.Data.Sql;
using System.Data;
using System.ComponentModel;

namespace PaymentsGenerator.Prototypes
{
    /// <summary>
    /// Interaction logic for ModalSqlServerInstance.xaml
    /// </summary>
    public partial class ModalSqlServerInstance : Window
    {
        public ModalSqlServerInstance()
        {
            InitializeComponent();
           
            var enumerable = SqlDataSourceEnumerator.Instance.GetDataSources().AsEnumerable();
            //var uniqueSources = (from row in enumerable where row[1] != null select row[0]).Distinct();
           
        }

        private void sqlInstDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = sqlInstDG.SelectedItems[0] as DataRowView;

            if (drv != null)
            {
                string dataSourceName = drv[0].ToString();
                string initCatalogName = drv[1].ToString();
                //connStringTB.Text = "Connection string: " + AccountCache.Instance().SetEntityConnection(dataSourceName, initCatalogName);
            }
        }

        
       
        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            //undo changing conn string on cache
            this.Close();
        }       

        private void ObjectDataProvider_DataChanged(object sender, EventArgs e)
        {
            var dataTable = ((ObjectDataProvider)sender).Data as DataTable;
            if (dataTable != null)
            {
                var uniqueSources = (from row in dataTable.AsEnumerable() where !string.IsNullOrEmpty(row[1].ToString()) select row[0]).Distinct();
                if (uniqueSources != null)
                    sqlInstancesCB.ItemsSource = uniqueSources;
            }
        }

        private void sqlInstancesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectDataProvider odp = TryFindResource("odp") as ObjectDataProvider;
            if (odp != null)
            {
                var selVal = ((ComboBox)sender).SelectedValue;
                var catalogs = (from row in ((DataTable)odp.Data).AsEnumerable() where row[0].ToString() == selVal.ToString() && !string.IsNullOrEmpty(row[1].ToString()) select row[1]);
                sqlCatalogsCB.ItemsSource = catalogs;
            }
        }

        private void sqlCatalogsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var instance = ((ComboBox)sender).SelectedValue as string;
            var catalog = ((ComboBox)sender).SelectedValue as string;

            //if (!string.IsNullOrEmpty(instance) && !string.IsNullOrEmpty(catalog))
            //    connStringTB.Text = "Connection string: " + AccountCache.Instance().SetEntityConnection(instance, catalog);

        }
    }
}
