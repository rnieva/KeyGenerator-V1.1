using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using KeyGenerator.Model;
using KeyGenerator.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace KeyGenerator 
{
    public partial class DataGridWindow : Window
    {
        public DataGridWindow()
        {
            Database.SetInitializer<DemoContext>(new CreateDatabaseIfNotExists<DemoContext>());
            InitializeComponent();
            showDataGrid();
        }
        void showDataGrid()
        {
            var dbContext = new DemoContext();
            System.Collections.ObjectModel.ObservableCollection<DaraReg> valuesBd = new System.Collections.ObjectModel.ObservableCollection<DaraReg>();
            var listBd = from p in dbContext.DataAll select p;
            foreach (var p in listBd)
            {
                valuesBd.Add(new DaraReg() {Id = p.Id , TargetKey = p.TargetKey, key = p.key });
            }
            dataGrid1.ItemsSource = valuesBd;  
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string a = dataGrid1.SelectedCells[1].Column.ToString();
            //string b = dataGrid1.SelectedItem.ToString();
            //string c = dataGrid1.SelectedItems.ToString();
            //MessageBox.Show(c);
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string a = dataGrid1.SelectedCells[1].Column.ToString();
            string b = dataGrid1.SelectedItem.ToString();
            string c = dataGrid1.SelectedItems.ToString();
            string d = "";

            var selected = (DaraReg)dataGrid1.SelectedItem;
            //var select = selected as DaraReg;
            d = selected.key.ToString();

            //APUNTE: para recorrerlo
            //var selected = dataGrid1.SelectedItems;
            //foreach (var item in selected)
            //{
            //    var select = item as DaraReg;
            //    d = select.key.ToString();
            //} 

            
            MessageBox.Show(d);
        }
        private void copyAlltoClipboard()
        {
            var dbContext = new DemoContext();
            System.Collections.ObjectModel.ObservableCollection<DaraReg> valuesBd = new System.Collections.ObjectModel.ObservableCollection<DaraReg>();
            var listBd = from p in dbContext.DataAll select p;
            foreach (var p in listBd)
            {
                valuesBd.Add(new DaraReg() { Id = p.Id, TargetKey = p.TargetKey, key = p.key });
            }
            //dataGrid1.SelectAll();
            //DataObject dataObj = dataGrid1.GetClipboardContent();
            //if (dataObj != null)
                Clipboard.SetDataObject(valuesBd);
            
        }
        private void buttonExportExcel_Click(object sender, RoutedEventArgs e)
        {
            //copyAlltoClipboard();
            //Microsoft.Office.Interop.Excel.Application xlexcel;
            //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;
            //xlexcel = new Excel.Application();
            //xlexcel.Visible = true;
            //xlWorkBook = xlexcel.Workbooks.Add(misValue);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
            //CR.Select();
            //xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            

            MessageBox.Show("File Exported");

           
        }
    }
}
