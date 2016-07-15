
using System.Windows.Forms;

namespace MilitiaOrganizationSystem
{
    partial class XMLGroupTaskForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLGroupTaskForm));
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.rMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.view2 = new System.Windows.Forms.ToolStripMenuItem();
            this.groups_treeView = new System.Windows.Forms.TreeView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.ToolStripMenuItem();
            this.view = new System.Windows.Forms.ToolStripMenuItem();
            this.refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.rMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Maroon;
            this.treeViewImageList.Images.SetKeyName(0, "u=1978385908,3347853851&fm=21&gp=0.jpg");
            // 
            // rMenu
            // 
            this.rMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.view2});
            this.rMenu.Name = "rMenu";
            this.rMenu.Size = new System.Drawing.Size(173, 26);
            // 
            // view2
            // 
            this.view2.Name = "view2";
            this.view2.Size = new System.Drawing.Size(172, 22);
            this.view2.Text = "查看此组下的民兵";
            // 
            // groups_treeView
            // 
            this.groups_treeView.AllowDrop = true;
            this.groups_treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groups_treeView.HotTracking = true;
            this.groups_treeView.ImageIndex = 0;
            this.groups_treeView.ImageList = this.treeViewImageList;
            this.groups_treeView.LabelEdit = true;
            this.groups_treeView.Location = new System.Drawing.Point(-1, 27);
            this.groups_treeView.Name = "groups_treeView";
            this.groups_treeView.PathSeparator = "/";
            this.groups_treeView.SelectedImageIndex = 0;
            this.groups_treeView.ShowNodeToolTips = true;
            this.groups_treeView.Size = new System.Drawing.Size(416, 518);
            this.groups_treeView.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(414, 25);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "菜单流";
            // 
            // menu
            // 
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.view,
            this.refresh});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(44, 21);
            this.menu.Text = "菜单";
            // 
            // view
            // 
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(172, 22);
            this.view.Text = "查看此组下的民兵";
            // 
            // refresh
            // 
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(172, 22);
            this.refresh.Text = "刷新";
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // XMLGroupTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 542);
            this.Controls.Add(this.groups_treeView);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "XMLGroupTaskForm";
            this.Text = "分组任务";
            this.rMenu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.TreeView groups_treeView;//树控件
        private System.Windows.Forms.ContextMenuStrip rMenu;//右键菜单
        private ToolStripMenuItem view2;
        private ImageList treeViewImageList;
        private ToolStripMenuItem menu;
        private ToolStripMenuItem view;
        private MenuStrip menuStrip;
        private ToolStripMenuItem refresh;
    }


}