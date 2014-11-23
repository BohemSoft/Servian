using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using Microsoft.Win32;
using bohem;
using System.Security.Principal;

namespace bohemform
{
    public partial class Form1 : Form
    {
        public string servisAdi, gorunenAd;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            
        }

        public bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public Class1 o1=new Class1();
        public string gec;
        private void button1_Click(object sender, EventArgs e)
        {
          listele();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[2].Value.ToString() == "Stopped")
            {
                o1.hizmetCalistir(dataGridView1.SelectedCells[0].Value.ToString());
                dataGridView1.Refresh();
            }
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[2].Value.ToString() == "Running")
            {
                o1.hizmetDurdur(dataGridView1.SelectedCells[0].Value.ToString());
                dataGridView1.Refresh();
            }
            
        }
        public int j;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length>=3)
            {
                                arama(textBox1.Text);
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        void listele()
        {
            dataGridView1.Rows.Clear();
            o1 = new Class1();
            string servisAdi, durum, servisAciklamasi;
            foreach (ServiceController service in ServiceController.GetServices())
            {
                servisAdi = service.ServiceName;
                servisAciklamasi = service.DisplayName;
                durum = service.Status.ToString();
                dataGridView1.Rows.Add(servisAdi, servisAciklamasi, durum);
            }
        }

        void arama(string ara)
        {
            j = 0;
            if (textBox1.Text.Length >= 3)
            {
                ara = textBox1.Text;
                string[] aranan = new string[1001];
                string[] aranan2 = new string[1001];
                string[] aranan3 = new string[1001];
                string[] bulunan = new string[1001];
                string[] bulunan2 = new string[1001];
                string[] bulunan3 = new string[1001];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    aranan[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    aranan2[i] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    aranan3[i] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    if (aranan[i].IndexOf(ara, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        bulunan[j] = aranan[i];
                        bulunan2[j] = aranan2[i];
                        bulunan3[j] = aranan3[i];
                        j++;
                    }
                }
                dataGridView1.Rows.Clear();
                for (int k = 0; k < bulunan.Length; k++)
                {
                    dataGridView1.Rows.Add(bulunan[k], bulunan2[k], bulunan3[k]);
                }
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
    }
}
