namespace CrumbBar
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crumbBar1 = new DevComponents.DotNetBar.CrumbBar();
            this.crumbBarItem1 = new DevComponents.DotNetBar.CrumbBarItem();
            this.crumbBarItem2 = new DevComponents.DotNetBar.CrumbBarItem();
            this.crumbBarItem4 = new DevComponents.DotNetBar.CrumbBarItem();
            this.crumbBarItem5 = new DevComponents.DotNetBar.CrumbBarItem();
            this.crumbBarItem6 = new DevComponents.DotNetBar.CrumbBarItem();
            this.crumbBarItem7 = new DevComponents.DotNetBar.CrumbBarItem();
            this.crumbBarItem3 = new DevComponents.DotNetBar.CrumbBarItem();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.crumbBar2 = new DevComponents.DotNetBar.CrumbBar();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // crumbBar1
            // 
            this.crumbBar1.AutoSize = true;
            // 
            // 
            // 
            this.crumbBar1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.crumbBar1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar1.BackgroundStyle.BorderBottomWidth = 1;
            this.crumbBar1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(89)))), ((int)(((byte)(94)))));
            this.crumbBar1.BackgroundStyle.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.crumbBar1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar1.BackgroundStyle.BorderLeftWidth = 1;
            this.crumbBar1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar1.BackgroundStyle.BorderRightWidth = 1;
            this.crumbBar1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar1.BackgroundStyle.BorderTopWidth = 1;
            this.crumbBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.crumbBar1.ContainerControlProcessDialogKey = true;
            this.crumbBar1.Items.Add(this.crumbBarItem1);
            this.crumbBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.crumbBar1.Location = new System.Drawing.Point(11, 51);
            this.crumbBar1.Name = "crumbBar1";
            this.crumbBar1.PathSeparator = ";";
            this.crumbBar1.SelectedItem = this.crumbBarItem1;
            this.crumbBar1.Size = new System.Drawing.Size(329, 24);
            this.crumbBar1.TabIndex = 0;
            this.crumbBar1.SelectedItemChanging += new DevComponents.DotNetBar.CrumBarSelectionEventHandler(this.CrumbBarSelectedItemChanging);
            this.crumbBar1.SelectedItemChanged += new DevComponents.DotNetBar.CrumBarSelectionEventHandler(this.CrumbBarSelectedItemChanged);
            // 
            // crumbBarItem1
            // 
            this.crumbBarItem1.Image = global::CrumbBar.Properties.Resources.computer;
            this.crumbBarItem1.Name = "crumbBarItem1";
            this.crumbBarItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.crumbBarItem2,
            this.crumbBarItem3});
            this.crumbBarItem1.Text = "My Computer";
            // 
            // crumbBarItem2
            // 
            this.crumbBarItem2.Image = global::CrumbBar.Properties.Resources.hdd;
            this.crumbBarItem2.Name = "crumbBarItem2";
            this.crumbBarItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.crumbBarItem4,
            this.crumbBarItem5,
            this.crumbBarItem6,
            this.crumbBarItem7});
            this.crumbBarItem2.Text = "Drive C";
            // 
            // crumbBarItem4
            // 
            this.crumbBarItem4.Image = global::CrumbBar.Properties.Resources.folder;
            this.crumbBarItem4.Name = "crumbBarItem4";
            this.crumbBarItem4.Text = "Drivers";
            // 
            // crumbBarItem5
            // 
            this.crumbBarItem5.Image = global::CrumbBar.Properties.Resources.folder;
            this.crumbBarItem5.Name = "crumbBarItem5";
            this.crumbBarItem5.Text = "Program Files";
            // 
            // crumbBarItem6
            // 
            this.crumbBarItem6.Image = global::CrumbBar.Properties.Resources.folder;
            this.crumbBarItem6.Name = "crumbBarItem6";
            this.crumbBarItem6.Text = "InetPub";
            // 
            // crumbBarItem7
            // 
            this.crumbBarItem7.Image = global::CrumbBar.Properties.Resources.folder;
            this.crumbBarItem7.Name = "crumbBarItem7";
            this.crumbBarItem7.Text = "Windows";
            // 
            // crumbBarItem3
            // 
            this.crumbBarItem3.Image = global::CrumbBar.Properties.Resources.dvdrom;
            this.crumbBarItem3.Name = "crumbBarItem3";
            this.crumbBarItem3.Text = "DVD ROM";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(11, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(329, 43);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Control with items added using VS.NET designer. Use Edit Items task in VS.NET to " +
    "start items editor. Use SelectedItem property to select an item.";
            this.labelX1.WordWrap = true;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(11, 83);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(329, 50);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "Control with items created using code only. Use SelectedItemChanging and Selected" +
    "ItemChanged events to be notified when selected item is changing.";
            this.labelX2.WordWrap = true;
            // 
            // crumbBar2
            // 
            this.crumbBar2.AutoSize = true;
            // 
            // 
            // 
            this.crumbBar2.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.crumbBar2.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar2.BackgroundStyle.BorderBottomWidth = 1;
            this.crumbBar2.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(89)))), ((int)(((byte)(94)))));
            this.crumbBar2.BackgroundStyle.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.crumbBar2.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar2.BackgroundStyle.BorderLeftWidth = 1;
            this.crumbBar2.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar2.BackgroundStyle.BorderRightWidth = 1;
            this.crumbBar2.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.crumbBar2.BackgroundStyle.BorderTopWidth = 1;
            this.crumbBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.crumbBar2.ContainerControlProcessDialogKey = true;
            this.crumbBar2.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.crumbBar2.Location = new System.Drawing.Point(11, 139);
            this.crumbBar2.Name = "crumbBar2";
            this.crumbBar2.PathSeparator = ";";
            this.crumbBar2.Size = new System.Drawing.Size(329, 22);
            this.crumbBar2.TabIndex = 3;
            this.crumbBar2.SelectedItemChanging += new DevComponents.DotNetBar.CrumBarSelectionEventHandler(this.crumbBar2_SelectedItemChanging);
            this.crumbBar2.SelectedItemChanged += new DevComponents.DotNetBar.CrumBarSelectionEventHandler(this.CrumbBarSelectedItemChanged);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(13, 197);
            this.textBoxX1.Multiline = true;
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxX1.Size = new System.Drawing.Size(327, 112);
            this.textBoxX1.TabIndex = 4;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 176);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(123, 15);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "Event log:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(353, 321);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.crumbBar2);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.crumbBar1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "CrumbBar Control Sample Application";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.CrumbBar crumbBar1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem1;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem2;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem4;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem5;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem6;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem7;
        private DevComponents.DotNetBar.CrumbBarItem crumbBarItem3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.CrumbBar crumbBar2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}

