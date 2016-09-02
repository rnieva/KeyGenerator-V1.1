using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity; // NuGet packet, EntityFramework 
using KeyGenerator.Model;
using KeyGenerator.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Collections.ObjectModel;
using KeyGenerator;

namespace WpfExample1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Database.SetInitializer<DemoContext>(new DropCreateDatabaseAlways<DemoContext>()); // this eraser DB and create a new one  
            Database.SetInitializer<DemoContext>(new CreateDatabaseIfNotExists<DemoContext>());
            InitializeComponent();
            MyButton1.Click += MyButton_Click;   //generate key
            MyButton2.Click += MyButton_Click2;  //save key
            MyButton3.Click += MyButton_Click3;  //show keys
            MyButton4.Click += MyButton_Click4;  //export keys to .txt 
            MyButton5.Click += MyButton_Click5;  //sent a email with Keys
            MyButton6.Click += MyButton_Click6;  //delete by ID
            MyButton7.Click += MyButton_Click7;  //show datagrid
            MyButton8.Click += MyButton_Click8;  //show datagridview
            textBox4.Select(0, textBox1.Text.Length);
            textBox4.Focus();
        }
        private void MyButton_Click(object sender, RoutedEventArgs e)
        { //generate key
            string userWord = textBox1.Text; //work introduced by user for generate a new key
            int maxSize = MaxSize();
            string keyTypeStr = KeyTypeStr(userWord,maxSize);
            MyLabel1.Content = Generator(userWord, keyTypeStr, maxSize);
        }
        private void MyButton_Click2(object sender, RoutedEventArgs e)
        { //save Key in DB
            switch (MyKey.IsChecked)
            {
                case true:
                            if ((textBox4.Text.Length != 0) && (textBox4.Text != "Site Key") && (textBox5.Text.Length != 0) && (textBox5.Text != "Write your own Key"))
                            {
                                var dbContext = new DemoContext();
                                var info = new DaraReg() { TargetKey = textBox4.Text, key = textBox5.Text };
                                dbContext.DataAll.Add(info);
                                dbContext.SaveChanges();
                                System.Windows.Forms.MessageBox.Show("Key User Save");
                            }
                            else
                            {
                                MessageBox.Show("Site Key or Key into null");
                            }
                    break;
                case false:
                            if ((textBox4.Text.Length != 0) && (textBox4.Text != "Site Key") && (MyLabel1.Content.ToString() != "") && (MyKey.IsChecked == false))
                            {
                                var dbContext = new DemoContext();
                                var info = new DaraReg() { TargetKey = textBox4.Text, key = MyLabel1.Content.ToString() };
                                dbContext.DataAll.Add(info);
                                dbContext.SaveChanges();
                                MessageBox.Show("Key Generated Save");
                                MyButton_Click3(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Site Key or Key null");
                            }
                    break;
                default:
                    Debug.WriteLine("default switch");
                    break;
            }
        }
        private void MyButton_Click3(object sender, RoutedEventArgs e)
        { // show data from DB
            var dbContext = new DemoContext();
            listBox1.Items.Clear();
            //Others ways
            //ObservableCollection<DaraReg> valuesBd = new ObservableCollection<DaraReg>();
            //List<string> listBDlist = new List<string>();
            var listBd = from p in dbContext.DataAll select p;
            foreach (var p in listBd)
            {
                //valuesBd.Add(new DaraReg() {Id = p.Id , TargetKey = p.TargetKey, key = p.key });
                //listBDlist.Add(p.Id.ToString());
                listBox1.Items.Add(p.Id + " " + p.TargetKey + " " + p.key);
            }
            //listBox1.DataContext = listBDlist;
            //listBox1.Items.Add(listBDlist);
            //listBox1.Sorted = true;
        }
        private void MyButton_Click4(object sender, RoutedEventArgs e)
        {//export DB to TXT file
            StreamWriter fileWriter = new StreamWriter("TextFile1.txt", true);
            var dbContext = new DemoContext();
            var listBd = from p in dbContext.DataAll select p;
            foreach (var p in listBd)
            {
                fileWriter.Write(DateTime.Now + "\r\n");
                fileWriter.Write(p.TargetKey + ":" + p.key + "\r\n");    
            }
            fileWriter.Close();
            MessageBox.Show("Export Succesfull - TextFile1.txt");
        }
        private void MyButton_Click5(object sender, RoutedEventArgs e)
        { //manda por email las keys que estan en el ultimo txt exportado -- modificar para que sean las keys de la bd
            //textBox4.Text = "Email Address";
            //textBox4.Select(0, textBox1.Text.Length);
            //textBox4.Focus();
            string emailrecipient = textBox4.Text;
            if (IsValidEmailAdress(emailrecipient) == true) 
            {
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(emailrecipient));
                email.From = new MailAddress("mail_address@ser.com", "KeyGenerator");
                email.Subject = " Keys-Store ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
                email.Body = "Keys <br>";
                System.IO.StreamReader file = new System.IO.StreamReader("TextFile1.txt", true);
                string line = ""; ;
                while ((line = file.ReadLine()) != null)
                {
                    email.Body = email.Body + "<br>" + line;
                }
                file.Close();                
                email.IsBodyHtml = true;
                //email.Attachments.Add(new Attachment("TextFile1.txt")); //you can atach a file
                email.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.live.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("mail_address@server.com", "XXX"); // xxx, pass email
                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    MessageBox.Show("Send complete");
                    Debug.WriteLine("Email sent");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail sending Email");
                    Debug.WriteLine("Error to sent email: " + ex.Message);
                }
            }
            else
            {
               MessageBox.Show("Email address not valid");
            }
            
        }
        private bool IsValidEmailAdress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void MyButton_Click6(object sender, RoutedEventArgs e)
        { // delete key by ID
            TextBoxDelete.Select(0, TextBoxDelete.Text.Length);
            int IdToDel = int.Parse(TextBoxDelete.Text);
            var dbContext = new DemoContext();
            var keyToDelete = (from p in dbContext.DataAll.Where(p => p.Id == IdToDel) select p).Single();
            dbContext.DataAll.Remove(keyToDelete);         
            dbContext.SaveChanges();
        }
        private void borrarSeleccionado(int id)
        { // delete key selected by user
            int IdToDel = id;
            var dbContext = new DemoContext();
            var keyToDelete = (from p in dbContext.DataAll.Where(p => p.Id == IdToDel) select p).Single();
            dbContext.DataAll.Remove(keyToDelete);
            dbContext.SaveChanges();
        }
        private void MyButton_Click7(object sender, RoutedEventArgs e)
        { //Show DataGrid
            DataGridWindow windowData2 = new DataGridWindow();
            windowData2.Show();
        }
        private void MyButton_Click8(object sender, RoutedEventArgs e)
        { //Show DataGridView
            DataGridViewWindowForm formData = new DataGridViewWindowForm();
            formData.Show();
        }
        public int MaxSize()
        {
            int maxSize = 8;
            if (_8bits.IsChecked == true)
            {
                maxSize = 8;
            }
            if (_16bits.IsChecked == true)
            {
                maxSize = 16;
            }
            if (_32bits.IsChecked == true)
            {
                maxSize = 32;
            }
            return maxSize;
        }
        private string KeyTypeStr(string userWord,int maxSize)
        {
            userWord = userWord.Replace(" ", ""); //remove spaces
            userWord = userWord.Trim();
            string typeStr = "";
            int keyType2 = 1;
            if (Numbers.IsChecked == true) 
            {
                keyType2 = 1; //only numbers
                typeStr = "1234567890";
            }
            if (Letters.IsChecked == true) 
            {
                if (randomKey.IsChecked == true)
                {
                    keyType2 = 2;
                    if (Uppercase1.IsChecked == true)
                    {
                        keyType2 = 21;
                        typeStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    }
                    else
                    {
                        keyType2 = 22;
                        typeStr = "abcdefghijklmnopqrstuvwxyz";
                    }
                }
                else
                {
                        keyType2 = 211;
                        typeStr = userWord;
                        if (Uppercase1.IsChecked == true)
                        {
                            keyType2 = 221;
                            typeStr = userWord.ToUpper();
                        }
                        else
                        {
                            keyType2 = 222;
                            typeStr = userWord;
                        }
                 }   

            }                                   
            if (Alphanumeric.IsChecked == true)
            {
                if (randomKey.IsChecked == true)
                {
                    keyType2 = 3;
                    if (Uppercase2.IsChecked == true)
                    {
                        keyType2 = 31;
                        typeStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    }
                    else
                    {
                        keyType2 = 32;
                        typeStr = "abcdefghijklmnopqrstuvwxyz1234567890";
                    }
                }
                else
                {
                    keyType2 = 331;
                    typeStr = userWord;
                    if (Uppercase1.IsChecked == true)
                    {
                        keyType2 = 331;
                        typeStr = userWord.ToUpper() + "1234567890"; 
                    }
                    else
                    {
                        keyType2 = 332;
                        typeStr = userWord + "1234567890";
                    }
                }
                }
            if (Symbols.IsChecked == true)
            {
                keyType2 = 4;
                typeStr = "@#€%&=Ç+*-.:!¡¿?()";
            }
            if (Mixtotal.IsChecked == true)
            {
                if (randomKey.IsChecked == true)
                {
                    keyType2 = 5;
                    typeStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@#€%&=Ç+*-.:!¡¿?()";
                }
                else
                {
                    keyType2 = 51;
                    typeStr = userWord + userWord.ToUpper() + "1234567890" + "@#€%&=Ç+*-.:!¡¿?()";
                }
            }
            return typeStr;
        }
        private string Generator(string userWord, string keyTypeStr,int maxSize)
        {
            if (RNGC.IsChecked == true)    
            {
                int tamA = keyTypeStr.Length;
                char[] chars = new char[tamA];
                string a = keyTypeStr;
                chars = a.ToCharArray();
                int size = maxSize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                { result.Append(chars[b % (chars.Length - 1)]); }
                return result.ToString();
            }
            else // SnowC method
            {
                int tamA = keyTypeStr.Length;
                int size = maxSize;
                string output = new string(keyTypeStr.ToCharArray().Reverse().ToArray());
                Random r = new Random();
                int num;
                while (output.Length < size)
                {
                    num = r.Next(0,9);
                    output = output + num;
                }
                return output;
            }
        }
        private void checkBox_Checked_1(object sender, RoutedEventArgs e)
        {
        }
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void Alphanumeric_Click(object sender, RoutedEventArgs e)
        {
            if (Alphanumeric.IsChecked == true)
            {
                Numbers.IsChecked = true;
                Letters.IsChecked = true;
                Uppercase1.IsChecked = false;
                Mixtotal.IsChecked = false;
                Symbols.IsChecked = false;
            }
            else
            {
                Numbers.IsChecked = true;
                Letters.IsChecked = false;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void Uppercase2_Click(object sender, RoutedEventArgs e)
        {
            if (Uppercase2.IsChecked == true)
            {
                Numbers.IsChecked = true;
                Alphanumeric.IsChecked = true;
                Mixtotal.IsChecked = false;
                Letters.IsChecked = true;
                Uppercase1.IsChecked = true;          
                Symbols.IsChecked = false;
            }
            else
            {
                Numbers.IsChecked = true;
                Letters.IsChecked = true;
                Alphanumeric.IsChecked = true;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void Letters_Click(object sender, RoutedEventArgs e)
        {
            Symbols.IsChecked = false;
            if ((Letters.IsChecked == true) && (Numbers.IsChecked == true))
                Alphanumeric.IsChecked = true;
            if (Letters.IsChecked == false)
            {
                Numbers.IsChecked = true;
                Alphanumeric.IsChecked = false;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void Uppercase1_Click(object sender, RoutedEventArgs e)
        {
            if (Uppercase1.IsChecked == true)
            {
                Numbers.IsChecked = false;
                Alphanumeric.IsChecked = false;
                Mixtotal.IsChecked = false;
                Letters.IsChecked = true;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
            else
            {
                Numbers.IsChecked = false;
                Letters.IsChecked = true;
                Alphanumeric.IsChecked = false;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void Numbers_Click(object sender, RoutedEventArgs e)
        {
            Symbols.IsChecked = false;
            if ((Letters.IsChecked == true) && (Numbers.IsChecked == true))
            {
                Alphanumeric.IsChecked = true;
                Uppercase1.IsChecked = false;
            }
            if (Numbers.IsChecked == false)
            {
                Letters.IsChecked = true;
                Alphanumeric.IsChecked = false;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void Mixtotal_Click(object sender, RoutedEventArgs e)
        {
            if (Mixtotal.IsChecked == true)
            {
                Numbers.IsChecked = true;
                Alphanumeric.IsChecked = true;
                Letters.IsChecked = true;
                Uppercase1.IsChecked = true;
                Uppercase2.IsChecked = true;
                Symbols.IsChecked = true;
            }
            else
            {
                Numbers.IsChecked = true;
                Letters.IsChecked = false;
                Alphanumeric.IsChecked = false;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void Symbols_Click(object sender, RoutedEventArgs e)
        {
            if (Symbols.IsChecked == true)
            {
                Numbers.IsChecked = false;
                Alphanumeric.IsChecked = false;
                Letters.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Mixtotal.IsChecked = false;
            }
            else
            {
                Numbers.IsChecked = true;
                Letters.IsChecked = false;
                Alphanumeric.IsChecked = false;
                Mixtotal.IsChecked = false;
                Uppercase1.IsChecked = false;
                Uppercase2.IsChecked = false;
                Symbols.IsChecked = false;
            }
        }
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void _8bits_Click(object sender, RoutedEventArgs e)
        {
            if (_8bits.IsChecked == true)
            {
                _16bits.IsChecked = false;
            }
        }
        public void _16bits_Click(object sender, RoutedEventArgs e)
        {
            if (_16bits.IsChecked == true)
            {
                _8bits.IsChecked = false;
            }
        }
        private void randomKey_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void randomKey_Click(object sender, RoutedEventArgs e)
        {
            if (randomKey.IsChecked == false)
            {
                textBox1.IsEnabled = true;
                textBox1.Visibility = System.Windows.Visibility.Visible;
                textBox1.Select(0,textBox1.Text.Length);
                textBox1.Focus();
            }
            else
            {
                textBox1.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void RNGC_Click(object sender, RoutedEventArgs e)
        {
            SnowC.IsChecked = false;
            MyKey.IsChecked = false;
            MyButton1.IsEnabled = true;
        }
        private void SnowC_Click(object sender, RoutedEventArgs e)
        {
            RNGC.IsChecked = false;
            MyKey.IsChecked = false;
            MyButton1.IsEnabled = true;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void textbox4(object sender, MouseEventArgs e)
        {
            textBox4.Text = "Email Address";
            textBox4.Select(0, textBox1.Text.Length);
            textBox4.Focus();
        }
        private void textbox4Out(object sender, MouseEventArgs e)
        {
            textBox4.Text = "Site Key";
        }
        private void MyKey_Click(object sender, RoutedEventArgs e)
        {
            RNGC.IsChecked = false;
            SnowC.IsChecked = false;
            MyButton1.IsEnabled = false;
            textBox5.IsEnabled = true;
            textBox5.Visibility = System.Windows.Visibility.Visible;
            textBox5.Select(0, textBox5.Text.Length);
            textBox5.Focus();
        }

        private void selListiBox1(object sender, SelectionChangedEventArgs e)
        { // Delete Key selected by user
            string idYsiteYKey = "";
            string siteYKey = "";
            int id = 0;
            if (listBox1.SelectedItem != null)
            {
                idYsiteYKey = listBox1.SelectedItem.ToString();
                id = int.Parse(idYsiteYKey.Substring(0, 2));
                int leng = idYsiteYKey.Length - 2;
                siteYKey = idYsiteYKey.Substring(2, leng);
                 
                MessageBoxResult result = MessageBox.Show("Delete Key\n\rSite and Key: " + siteYKey + " ID " + id, "Info", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        borrarSeleccionado(id);
                        MyButton_Click3(sender, e);
                        MessageBox.Show("Key Deleted");
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("No Deleted");
                        break;
                }
           }
        }
    }
}
