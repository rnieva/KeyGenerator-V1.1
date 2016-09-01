using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public partial class DataGridViewWindowForm : Form
    {
        public DataGridViewWindowForm()
        {
            Database.SetInitializer<DemoContext>(new CreateDatabaseIfNotExists<DemoContext>());
            InitializeComponent();
            showDataGridView();
        }
        void showDataGridView()
        {
            var dbContext = new DemoContext();
            System.Collections.ObjectModel.ObservableCollection<DaraReg> valuesBd = new System.Collections.ObjectModel.ObservableCollection<DaraReg>();
            var listBd = from p in dbContext.DataAll select p;
            foreach (var p in listBd)
            {
                valuesBd.Add(new DaraReg() { Id = p.Id, TargetKey = p.TargetKey, key = p.key });
            }
            dataGridView1.DataSource = valuesBd;
        }
        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int i = 0;
            int j = 0;
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    xlWorkSheet.Cells[1,j+1] = dataGridView1.Columns[j].Name.ToString().ToUpper();
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 1, j + 1] = cell.Value;
                }
            }
            xlWorkBook.SaveAs("KeyGeneratorDataBase.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            //xlApp.Visible = true;
            System.Windows.MessageBox.Show("Excel file created, KeyGeneratorDataBase.xls");   
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            showDataGridView();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int nFila = dataGridView1.CurrentCell.RowIndex;
            int IdToDel = Int32.Parse(dataGridView1.Rows[nFila].Cells[0].Value.ToString());
            var dbContext = new DemoContext();
            var keyToDelete = (from p in dbContext.DataAll.Where(p => p.Id == IdToDel) select p).Single();
            dbContext.DataAll.Remove(keyToDelete);
            dbContext.SaveChanges();
            System.Windows.Forms.MessageBox.Show("Row Deleted: "+ nFila );
            showDataGridView();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int nColum = dataGridView1.CurrentCell.ColumnIndex;
            if (nColum != 0)
            {
                String strSelect = dataGridView1.CurrentCell.Value.ToString();
                int nFileEdit = dataGridView1.CurrentCell.RowIndex;
                EditKeyForm EditKeyForm = new EditKeyForm(strSelect, nFileEdit,nColum, this);
                EditKeyForm.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You cannot Edit this field");
            }
        }
        public void edit(string strNew,int nFileEdit2,int nColum2)
        {
            int IdToEdit = Int32.Parse(dataGridView1.Rows[nFileEdit2].Cells[0].Value.ToString());
            var dbContext = new DemoContext();
            var regToEdit = (from p in dbContext.DataAll.Where(p => p.Id == IdToEdit) select p).Single();
            switch (nColum2)
            {
                case 1:
                    regToEdit.TargetKey = strNew;
                    break;
                case 2:
                    regToEdit.key = strNew;
                    break;
            }
            dbContext.Entry(regToEdit).CurrentValues.SetValues(regToEdit);
            dbContext.SaveChanges();
            showDataGridView();
            System.Windows.Forms.MessageBox.Show("File Edited ");
        }
    }
}
