using System;
using System.IO;
using System.Windows.Forms;

namespace CopySelectedFolderToAnotherLocation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            txtSource.Text = ""; // Reset the textbox to enable reuse without program restart
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fbd.SelectedPath; // Set the selected source folder path in the textbox
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            txtDestination.Text = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtDestination.Text = fbd.SelectedPath; // Set the selected destination folder path in the textbox
                btnCopy.Enabled = true; // Enable the copy button since both source and destination are selected
            }
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo destination)
        {
            Directory.CreateDirectory(destination.FullName); // Create the destination directory

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true); // Copy each file from source to destination
            }

            foreach (DirectoryInfo subDirectory in source.GetDirectories())
            {
                DirectoryInfo nextDestination = destination.CreateSubdirectory(subDirectory.Name); // Create a subdirectory in the destination
                CopyAll(subDirectory, nextDestination); // Recursively copy all contents of subdirectories
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                // Call the CopyAll method to start the copying process
                CopyAll(new DirectoryInfo(txtSource.Text), new DirectoryInfo(txtDestination.Text));
                MessageBox.Show("Selected folder successfully copied.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Display error message if copying fails
            }
        }
    }
}
