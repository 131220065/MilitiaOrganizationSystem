using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace CrumbBar
{
    public partial class Form1 : Office2007Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CrumbBarSelectedItemChanging(object sender, CrumbBarSelectionEventArgs e)
        {
            // Note that at this point you can cancel the selected item change by setting
            // e.Cancel=true
            AddToLog("SelectedItemChanging", (DevComponents.DotNetBar.CrumbBar)sender, e.NewSelectedItem);
        }

        private void CrumbBarSelectedItemChanged(object sender, CrumbBarSelectionEventArgs e)
        {
            AddToLog("SelectedItemChanged", (DevComponents.DotNetBar.CrumbBar)sender, e.NewSelectedItem);
        }

        private void AddToLog(string eventName, DevComponents.DotNetBar.CrumbBar crumbBar, CrumbBarItem crumbBarItem)
        {
            textBoxX1.Text += DateTime.Now.ToString() + " - " + 
                eventName + " (" + crumbBar.Name + ")";
            textBoxX1.Select(textBoxX1.Text.Length - 1, 0);
            textBoxX1.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load first batch of items
            CrumbBarItem myComputer = new CrumbBarItem();
            myComputer.Text = "My Computer";
            myComputer.Image = global::CrumbBar.Properties.Resources.computer;
            crumbBar2.Items.Add(myComputer);
            // Load disks, we will lazy load folders are disk are selected
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in drives)
            {
                if (driveInfo.DriveType != DriveType.Fixed) continue;
                CrumbBarItem node = new CrumbBarItem();
                node.Tag = driveInfo;
                node.Text = "Local Disk " + driveInfo.Name.Replace(@"\", "");
                node.Image = global::CrumbBar.Properties.Resources.hdd;
                myComputer.SubItems.Add(node);
            }
            // Finally select My Computer
            crumbBar2.SelectedItem = myComputer;
        }

        private void crumbBar2_SelectedItemChanging(object sender, CrumbBarSelectionEventArgs e)
        {
            AddToLog("SelectedItemChanging", (DevComponents.DotNetBar.CrumbBar)sender, e.NewSelectedItem);

            // Load folders are selection changes...
            CrumbBarItem parent = e.NewSelectedItem;
            if (parent == null || parent.SubItems.Count > 0) return;

            DirectoryInfo dirInfo=null;
            if (parent.Tag is DriveInfo)
            {
                DriveInfo driveInfo = (DriveInfo)parent.Tag;
                dirInfo = driveInfo.RootDirectory;
            }
            else if (parent.Tag is DirectoryInfo)
            {
                dirInfo = (DirectoryInfo)parent.Tag;
            }

            DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in subDirectories)
            {
                CrumbBarItem node = new CrumbBarItem();
                node.Tag = directoryInfo;
                node.Text = directoryInfo.Name;
                node.Image = global::CrumbBar.Properties.Resources.folder;
                parent.SubItems.Add(node);
            }
        }
    }
}