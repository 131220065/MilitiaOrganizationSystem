namespace MilitiaOrganizationSystem
{
    partial class ProvinceForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProvinceForm));
            this.militia_ListView = new System.Windows.Forms.ListView();
            this.rMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.rEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.rDele = new System.Windows.Forms.ToolStripMenuItem();
            this.militiaImageList = new System.Windows.Forms.ImageList(this.components);
            this.menu_basicLevel = new System.Windows.Forms.MenuStrip();
            this.btn_militaInfomation = new System.Windows.Forms.ToolStripMenuItem();
            this.add = new System.Windows.Forms.ToolStripMenuItem();
            this.modify = new System.Windows.Forms.ToolStripMenuItem();
            this.dele = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_importXMLGroupTask = new System.Windows.Forms.ToolStripMenuItem();
            this.options = new System.Windows.Forms.ToolStripMenuItem();
            this.doConflict = new System.Windows.Forms.ToolStripMenuItem();
            this.latestMilitias = new System.Windows.Forms.ToolStripMenuItem();
            this.stastistics = new System.Windows.Forms.ToolStripMenuItem();
            this.import = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCondition = new System.Windows.Forms.Label();
            this.nextPage = new System.Windows.Forms.Button();
            this.currentPage = new System.Windows.Forms.Button();
            this.lastPage = new System.Windows.Forms.Button();
            this.labelDi = new System.Windows.Forms.Label();
            this.labelPage = new System.Windows.Forms.Label();
            this.finalPage = new System.Windows.Forms.Button();
            this.conditionLabel = new System.Windows.Forms.Label();
            this.firstPage = new System.Windows.Forms.Button();
            this.pageLabel = new System.Windows.Forms.Label();
            this.rMenuStrip.SuspendLayout();
            this.menu_basicLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // militia_ListView
            // 
            this.militia_ListView.AllowColumnReorder = true;
            this.militia_ListView.AllowDrop = true;
            this.militia_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.militia_ListView.ContextMenuStrip = this.rMenuStrip;
            this.militia_ListView.FullRowSelect = true;
            this.militia_ListView.Location = new System.Drawing.Point(0, 55);
            this.militia_ListView.Name = "militia_ListView";
            this.militia_ListView.ShowItemToolTips = true;
            this.militia_ListView.Size = new System.Drawing.Size(850, 512);
            this.militia_ListView.SmallImageList = this.militiaImageList;
            this.militia_ListView.TabIndex = 0;
            this.militia_ListView.UseCompatibleStateImageBehavior = false;
            this.militia_ListView.View = System.Windows.Forms.View.Details;
            // 
            // rMenuStrip
            // 
            this.rMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rAdd,
            this.rEdit,
            this.rDele});
            this.rMenuStrip.Name = "rMenuStrip";
            this.rMenuStrip.Size = new System.Drawing.Size(101, 70);
            // 
            // rAdd
            // 
            this.rAdd.Name = "rAdd";
            this.rAdd.Size = new System.Drawing.Size(100, 22);
            this.rAdd.Text = "添加";
            this.rAdd.Click += new System.EventHandler(this.rAdd_Click);
            // 
            // rEdit
            // 
            this.rEdit.Name = "rEdit";
            this.rEdit.Size = new System.Drawing.Size(100, 22);
            this.rEdit.Text = "编辑";
            this.rEdit.Click += new System.EventHandler(this.rEdit_Click);
            // 
            // rDele
            // 
            this.rDele.Name = "rDele";
            this.rDele.Size = new System.Drawing.Size(100, 22);
            this.rDele.Text = "删除";
            this.rDele.Click += new System.EventHandler(this.rDele_Click);
            // 
            // militiaImageList
            // 
            this.militiaImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("militiaImageList.ImageStream")));
            this.militiaImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.militiaImageList.Images.SetKeyName(0, "u=1978385908,3347853851&fm=21&gp=0.jpg");
            // 
            // menu_basicLevel
            // 
            this.menu_basicLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_militaInfomation,
            this.btn_importXMLGroupTask,
            this.options,
            this.doConflict,
            this.latestMilitias,
            this.stastistics,
            this.import});
            this.menu_basicLevel.Location = new System.Drawing.Point(0, 0);
            this.menu_basicLevel.Name = "menu_basicLevel";
            this.menu_basicLevel.Size = new System.Drawing.Size(850, 25);
            this.menu_basicLevel.TabIndex = 2;
            this.menu_basicLevel.Text = "basicLevelMenuStrip";
            // 
            // btn_militaInfomation
            // 
            this.btn_militaInfomation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add,
            this.modify,
            this.dele});
            this.btn_militaInfomation.Name = "btn_militaInfomation";
            this.btn_militaInfomation.Size = new System.Drawing.Size(68, 21);
            this.btn_militaInfomation.Text = "民兵信息";
            // 
            // add
            // 
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(152, 22);
            this.add.Text = "添加";
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // modify
            // 
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(152, 22);
            this.modify.Text = "编辑";
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // dele
            // 
            this.dele.Name = "dele";
            this.dele.Size = new System.Drawing.Size(152, 22);
            this.dele.Text = "删除";
            this.dele.Click += new System.EventHandler(this.dele_Click);
            // 
            // btn_importXMLGroupTask
            // 
            this.btn_importXMLGroupTask.Name = "btn_importXMLGroupTask";
            this.btn_importXMLGroupTask.Size = new System.Drawing.Size(92, 21);
            this.btn_importXMLGroupTask.Text = "导入分组任务";
            this.btn_importXMLGroupTask.Click += new System.EventHandler(this.importXMLGroupTask_Click);
            // 
            // options
            // 
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(44, 21);
            this.options.Text = "设置";
            this.options.Click += new System.EventHandler(this.options_Click);
            // 
            // doConflict
            // 
            this.doConflict.Name = "doConflict";
            this.doConflict.Size = new System.Drawing.Size(68, 21);
            this.doConflict.Text = "检测冲突";
            this.doConflict.Click += new System.EventHandler(this.doConflict_Click);
            // 
            // latestMilitias
            // 
            this.latestMilitias.Name = "latestMilitias";
            this.latestMilitias.Size = new System.Drawing.Size(104, 21);
            this.latestMilitias.Text = "最近操作的民兵";
            this.latestMilitias.Click += new System.EventHandler(this.latestMilitias_Click);
            // 
            // stastistics
            // 
            this.stastistics.Name = "stastistics";
            this.stastistics.Size = new System.Drawing.Size(44, 21);
            this.stastistics.Text = "统计";
            this.stastistics.Click += new System.EventHandler(this.stastistics_Click);
            // 
            // import
            // 
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(68, 21);
            this.import.Text = "导入数据";
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // labelCondition
            // 
            this.labelCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCondition.AutoSize = true;
            this.labelCondition.Location = new System.Drawing.Point(12, 31);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(65, 12);
            this.labelCondition.TabIndex = 4;
            this.labelCondition.Text = "筛选条件：";
            // 
            // nextPage
            // 
            this.nextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextPage.Location = new System.Drawing.Point(685, 573);
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
            this.currentPage.Location = new System.Drawing.Point(604, 573);
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
            this.lastPage.Location = new System.Drawing.Point(523, 573);
            this.lastPage.Name = "lastPage";
            this.lastPage.Size = new System.Drawing.Size(75, 23);
            this.lastPage.TabIndex = 7;
            this.lastPage.Text = "上一页";
            this.lastPage.UseVisualStyleBackColor = true;
            this.lastPage.Click += new System.EventHandler(this.lastPage_Click);
            // 
            // labelDi
            // 
            this.labelDi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDi.AutoSize = true;
            this.labelDi.Location = new System.Drawing.Point(25, 577);
            this.labelDi.Name = "labelDi";
            this.labelDi.Size = new System.Drawing.Size(17, 12);
            this.labelDi.TabIndex = 9;
            this.labelDi.Text = "第";
            // 
            // labelPage
            // 
            this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(93, 578);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(143, 12);
            this.labelPage.TabIndex = 10;
            this.labelPage.Text = "页(‘-k’表示倒数第k页)";
            // 
            // finalPage
            // 
            this.finalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finalPage.Location = new System.Drawing.Point(766, 573);
            this.finalPage.Name = "finalPage";
            this.finalPage.Size = new System.Drawing.Size(75, 23);
            this.finalPage.TabIndex = 12;
            this.finalPage.Text = "尾页";
            this.finalPage.UseVisualStyleBackColor = true;
            this.finalPage.Click += new System.EventHandler(this.finalPage_Click);
            // 
            // conditionLabel
            // 
            this.conditionLabel.AutoSize = true;
            this.conditionLabel.Location = new System.Drawing.Point(83, 31);
            this.conditionLabel.Name = "conditionLabel";
            this.conditionLabel.Size = new System.Drawing.Size(41, 12);
            this.conditionLabel.TabIndex = 13;
            this.conditionLabel.Text = "未分组";
            this.conditionLabel.Click += new System.EventHandler(this.conditionLabel_Click);
            // 
            // firstPage
            // 
            this.firstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.firstPage.Location = new System.Drawing.Point(442, 574);
            this.firstPage.Name = "firstPage";
            this.firstPage.Size = new System.Drawing.Size(75, 22);
            this.firstPage.TabIndex = 17;
            this.firstPage.Text = "首页";
            this.firstPage.UseVisualStyleBackColor = true;
            this.firstPage.Click += new System.EventHandler(this.firstPage_Click);
            // 
            // pageLabel
            // 
            this.pageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pageLabel.AutoSize = true;
            this.pageLabel.Location = new System.Drawing.Point(60, 577);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(11, 12);
            this.pageLabel.TabIndex = 18;
            this.pageLabel.Text = "0";
            // 
            // ProvinceForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 596);
            this.Controls.Add(this.pageLabel);
            this.Controls.Add(this.firstPage);
            this.Controls.Add(this.conditionLabel);
            this.Controls.Add(this.finalPage);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.labelDi);
            this.Controls.Add(this.lastPage);
            this.Controls.Add(this.currentPage);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.labelCondition);
            this.Controls.Add(this.militia_ListView);
            this.Controls.Add(this.menu_basicLevel);
            this.Name = "ProvinceForm";
            this.Text = "省级主页";
            this.Load += new System.EventHandler(this.BasicLevelForm_Load);
            this.rMenuStrip.ResumeLayout(false);
            this.menu_basicLevel.ResumeLayout(false);
            this.menu_basicLevel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView militia_ListView;
        private System.Windows.Forms.MenuStrip menu_basicLevel;
        private System.Windows.Forms.ToolStripMenuItem btn_militaInfomation;
        private System.Windows.Forms.ToolStripMenuItem btn_importXMLGroupTask;
        private System.Windows.Forms.ToolStripMenuItem add;
        private System.Windows.Forms.ToolStripMenuItem modify;
        private System.Windows.Forms.ToolStripMenuItem dele;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.ContextMenuStrip rMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem rAdd;
        private System.Windows.Forms.ToolStripMenuItem rEdit;
        private System.Windows.Forms.ToolStripMenuItem rDele;
        private System.Windows.Forms.Button nextPage;
        private System.Windows.Forms.Button currentPage;
        private System.Windows.Forms.Button lastPage;
        private System.Windows.Forms.Label labelDi;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button finalPage;
        private System.Windows.Forms.ToolStripMenuItem import;
        private System.Windows.Forms.ToolStripMenuItem options;
        private System.Windows.Forms.Label conditionLabel;
        private System.Windows.Forms.ImageList militiaImageList;
        private System.Windows.Forms.ToolStripMenuItem doConflict;
        private System.Windows.Forms.ToolStripMenuItem latestMilitias;
        private System.Windows.Forms.ToolStripMenuItem stastistics;
        private System.Windows.Forms.Button firstPage;
        private System.Windows.Forms.Label pageLabel;
    }
}

