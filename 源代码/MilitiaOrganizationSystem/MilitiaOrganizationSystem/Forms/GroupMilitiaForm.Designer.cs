namespace MilitiaOrganizationSystem
{
    partial class GroupMilitiaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupMilitiaForm));
            this.nextPage = new System.Windows.Forms.Button();
            this.currentPage = new System.Windows.Forms.Button();
            this.lastPage = new System.Windows.Forms.Button();
            this.pageUpDown = new System.Windows.Forms.NumericUpDown();
            this.labelDi = new System.Windows.Forms.Label();
            this.labelPage = new System.Windows.Forms.Label();
            this.skipPage = new System.Windows.Forms.Button();
            this.militia_ListView = new System.Windows.Forms.ListView();
            this.militiaImageList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.statistics = new System.Windows.Forms.ToolStripMenuItem();
            this.finalPage = new System.Windows.Forms.Button();
            this.conditionLabel = new System.Windows.Forms.Label();
            this.firstPage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pageUpDown)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextPage
            // 
            this.nextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextPage.Location = new System.Drawing.Point(724, 496);
            this.nextPage.Name = "nextPage";
            this.nextPage.Size = new System.Drawing.Size(75, 23);
            this.nextPage.TabIndex = 5;
            this.nextPage.Text = "下一页";
            this.nextPage.UseVisualStyleBackColor = true;
            this.nextPage.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // currentPage
            // 
            this.currentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPage.Location = new System.Drawing.Point(643, 496);
            this.currentPage.Name = "currentPage";
            this.currentPage.Size = new System.Drawing.Size(75, 23);
            this.currentPage.TabIndex = 6;
            this.currentPage.Text = "刷新本页";
            this.currentPage.UseVisualStyleBackColor = true;
            this.currentPage.Click += new System.EventHandler(this.currentPage_Click);
            // 
            // lastPage
            // 
            this.lastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lastPage.Location = new System.Drawing.Point(562, 496);
            this.lastPage.Name = "lastPage";
            this.lastPage.Size = new System.Drawing.Size(75, 23);
            this.lastPage.TabIndex = 7;
            this.lastPage.Text = "上一页";
            this.lastPage.UseVisualStyleBackColor = true;
            this.lastPage.Click += new System.EventHandler(this.lastPage_Click);
            // 
            // pageUpDown
            // 
            this.pageUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pageUpDown.Location = new System.Drawing.Point(27, 496);
            this.pageUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageUpDown.Name = "pageUpDown";
            this.pageUpDown.Size = new System.Drawing.Size(42, 21);
            this.pageUpDown.TabIndex = 8;
            this.pageUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelDi
            // 
            this.labelDi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDi.AutoSize = true;
            this.labelDi.Location = new System.Drawing.Point(4, 501);
            this.labelDi.Name = "labelDi";
            this.labelDi.Size = new System.Drawing.Size(17, 12);
            this.labelDi.TabIndex = 9;
            this.labelDi.Text = "第";
            // 
            // labelPage
            // 
            this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(75, 501);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(17, 12);
            this.labelPage.TabIndex = 10;
            this.labelPage.Text = "页";
            // 
            // skipPage
            // 
            this.skipPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.skipPage.Location = new System.Drawing.Point(98, 496);
            this.skipPage.Name = "skipPage";
            this.skipPage.Size = new System.Drawing.Size(75, 23);
            this.skipPage.TabIndex = 11;
            this.skipPage.Text = "确认跳转";
            this.skipPage.UseVisualStyleBackColor = true;
            this.skipPage.Click += new System.EventHandler(this.skipPage_Click);
            // 
            // militia_ListView
            // 
            this.militia_ListView.AllowColumnReorder = true;
            this.militia_ListView.AllowDrop = true;
            this.militia_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.militia_ListView.FullRowSelect = true;
            this.militia_ListView.Location = new System.Drawing.Point(0, 57);
            this.militia_ListView.Name = "militia_ListView";
            this.militia_ListView.ShowItemToolTips = true;
            this.militia_ListView.Size = new System.Drawing.Size(894, 433);
            this.militia_ListView.SmallImageList = this.militiaImageList;
            this.militia_ListView.TabIndex = 0;
            this.militia_ListView.UseCompatibleStateImageBehavior = false;
            this.militia_ListView.View = System.Windows.Forms.View.Details;
            // 
            // militiaImageList
            // 
            this.militiaImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("militiaImageList.ImageStream")));
            this.militiaImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.militiaImageList.Images.SetKeyName(0, "u=1978385908,3347853851&fm=21&gp=0.jpg");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "筛选条件：";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statistics});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(895, 25);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip1";
            // 
            // statistics
            // 
            this.statistics.Name = "statistics";
            this.statistics.Size = new System.Drawing.Size(44, 21);
            this.statistics.Text = "统计";
            this.statistics.Click += new System.EventHandler(this.statistics_Click);
            // 
            // finalPage
            // 
            this.finalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finalPage.Location = new System.Drawing.Point(805, 496);
            this.finalPage.Name = "finalPage";
            this.finalPage.Size = new System.Drawing.Size(75, 23);
            this.finalPage.TabIndex = 14;
            this.finalPage.Text = "最后一页";
            this.finalPage.UseVisualStyleBackColor = true;
            this.finalPage.Click += new System.EventHandler(this.finalPage_Click);
            // 
            // conditionLabel
            // 
            this.conditionLabel.AutoSize = true;
            this.conditionLabel.Location = new System.Drawing.Point(75, 33);
            this.conditionLabel.Name = "conditionLabel";
            this.conditionLabel.Size = new System.Drawing.Size(41, 12);
            this.conditionLabel.TabIndex = 15;
            this.conditionLabel.Text = "未分组";
            this.conditionLabel.Click += new System.EventHandler(this.conditionLabel_Click);
            // 
            // firstPage
            // 
            this.firstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.firstPage.Location = new System.Drawing.Point(481, 496);
            this.firstPage.Name = "firstPage";
            this.firstPage.Size = new System.Drawing.Size(75, 23);
            this.firstPage.TabIndex = 16;
            this.firstPage.Text = "首页";
            this.firstPage.UseVisualStyleBackColor = true;
            this.firstPage.Click += new System.EventHandler(this.firstPage_Click);
            // 
            // GroupMilitiaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 520);
            this.Controls.Add(this.firstPage);
            this.Controls.Add(this.conditionLabel);
            this.Controls.Add(this.finalPage);
            this.Controls.Add(this.skipPage);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.labelDi);
            this.Controls.Add(this.pageUpDown);
            this.Controls.Add(this.lastPage);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.currentPage);
            this.Controls.Add(this.militia_ListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip);
            this.Name = "GroupMilitiaForm";
            this.Text = "民兵列表";
            ((System.ComponentModel.ISupportInitialize)(this.pageUpDown)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button nextPage;
        private System.Windows.Forms.Button currentPage;
        private System.Windows.Forms.Button lastPage;
        private System.Windows.Forms.NumericUpDown pageUpDown;
        private System.Windows.Forms.Label labelDi;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button skipPage;
        private System.Windows.Forms.ListView militia_ListView;
        private System.Windows.Forms.ImageList militiaImageList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem statistics;
        private System.Windows.Forms.Button finalPage;
        private System.Windows.Forms.Label conditionLabel;
        private System.Windows.Forms.Button firstPage;
    }
}