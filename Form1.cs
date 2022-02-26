using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyTheSelectedFolderAnotherLocation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnKaynak_Click(object sender, EventArgs e)
        {
            txtKaynak.Text = "";//If it is wanted to be used again without closing the program, I use it so that the textbox resets itself.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtKaynak.Text = Path.Combine(fbd.SelectedPath, Path.GetFileName(txtKaynak.Text));
            }
        }
        private void btnHedef_Click(object sender, EventArgs e)
        {
            txtHedef.Text = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtHedef.Text = Path.Combine(fbd.SelectedPath, Path.GetFileName(txtHedef.Text));
                btnKopyala.Enabled = true;
            }
        }
        public static void CopyAll(DirectoryInfo Kaynak, DirectoryInfo Hedef)
        {
            Directory.CreateDirectory(Hedef.FullName);
            foreach (FileInfo fi in Kaynak.GetFiles())
            {
                fi.CopyTo(Path.Combine(Hedef.FullName, fi.Name), true);
            }
            foreach (DirectoryInfo subDirectory in Kaynak.GetDirectories())
            {
                DirectoryInfo SiradakiDosya = Hedef.CreateSubdirectory(subDirectory.Name);
                CopyAll(subDirectory, SiradakiDosya);
            }
        }
        private void btnKopyala_Click(object sender, EventArgs e)
        {
            CopyAll(new DirectoryInfo(txtKaynak.Text), new DirectoryInfo(txtHedef.Text));
            MessageBox.Show("Seçilen dosya başarıyla kopyalandı.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

