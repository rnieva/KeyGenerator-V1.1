using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class EditKeyForm : Form
    {
        private string strNew;
        private int nFileEdit2;
        private int nColum2;
        private DataGridViewWindowForm newForm1 = new DataGridViewWindowForm();
        public EditKeyForm(String strSelect,int nFileEdit,int nColum, DataGridViewWindowForm form)
        {
            InitializeComponent();
            textBox1Edit.Text = strSelect; //field for edit from DataGridViewForm
            newForm1 = form;  //assignment from form
            nFileEdit2 = nFileEdit; //to know later the ID number
            nColum2 = nColum; //to know later the field to edit
        }

        private void button1A_Click(object sender, EventArgs e)
        {
            strNew = textBox1Edit.Text;
            newForm1.edit(strNew,nFileEdit2,nColum2);
            this.Close();
        }
        private void button1C_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
