namespace MilitiaOrganizationSystem
{
    partial class OptionForm
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
            this.pagesizeLabel = new System.Windows.Forms.Label();
            this.parasCheckBox = new System.Windows.Forms.CheckedListBox();
            this.pageSize = new System.Windows.Forms.NumericUpDown();
            this.checkedParaListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.checkAll = new System.Windows.Forms.CheckBox();
            this.invertCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pageSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pagesizeLabel
            // 
            this.pagesizeLabel.AutoSize = true;
            this.pagesizeLabel.Location = new System.Drawing.Point(21, 42);
            this.pagesizeLabel.Name = "pagesizeLabel";
            this.pagesizeLabel.Size = new System.Drawing.Size(131, 12);
            this.pagesizeLabel.TabIndex = 0;
            this.pagesizeLabel.Text = "每页显示民兵最大数量:";
            // 
            // parasCheckBox
            // 
            this.parasCheckBox.CheckOnClick = true;
            this.parasCheckBox.FormattingEnabled = true;
            this.parasCheckBox.Location = new System.Drawing.Point(37, 103);
            this.parasCheckBox.Name = "parasCheckBox";
            this.parasCheckBox.Size = new System.Drawing.Size(175, 324);
            this.parasCheckBox.TabIndex = 1;
            // 
            // pageSize
            // 
            this.pageSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.pageSize.Location = new System.Drawing.Point(164, 33);
            this.pageSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.pageSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.pageSize.Name = "pageSize";
            this.pageSize.Size = new System.Drawing.Size(48, 21);
            this.pageSize.TabIndex = 2;
            this.pageSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // checkedParaListBox
            // 
            this.checkedParaListBox.FormattingEnabled = true;
            this.checkedParaListBox.ItemHeight = 12;
            this.checkedParaListBox.Location = new System.Drawing.Point(291, 153);
            this.checkedParaListBox.Name = "checkedParaListBox";
            this.checkedParaListBox.Size = new System.Drawing.Size(120, 184);
            this.checkedParaListBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "已选的参数及顺序:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "选择要显示的民兵属性:";
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(368, 404);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.Text = "保存设置";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(287, 404);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // checkAll
            // 
            this.checkAll.AutoSize = true;
            this.checkAll.Location = new System.Drawing.Point(164, 78);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(48, 16);
            this.checkAll.TabIndex = 8;
            this.checkAll.Text = "全选";
            this.checkAll.UseVisualStyleBackColor = true;
            // 
            // invertCheckbox
            // 
            this.invertCheckbox.AutoSize = true;
            this.invertCheckbox.Location = new System.Drawing.Point(218, 78);
            this.invertCheckbox.Name = "invertCheckbox";
            this.invertCheckbox.Size = new System.Drawing.Size(48, 16);
            this.invertCheckbox.TabIndex = 9;
            this.invertCheckbox.Text = "反选";
            this.invertCheckbox.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(455, 439);
            this.Controls.Add(this.invertCheckbox);
            this.Controls.Add(this.checkAll);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedParaListBox);
            this.Controls.Add(this.pageSize);
            this.Controls.Add(this.parasCheckBox);
            this.Controls.Add(this.pagesizeLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            ((System.ComponentModel.ISupportInitialize)(this.pageSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pagesizeLabel;
        private System.Windows.Forms.CheckedListBox parasCheckBox;
        private System.Windows.Forms.NumericUpDown pageSize;
        private System.Windows.Forms.ListBox checkedParaListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.CheckBox checkAll;
        private System.Windows.Forms.CheckBox invertCheckbox;
    }
}