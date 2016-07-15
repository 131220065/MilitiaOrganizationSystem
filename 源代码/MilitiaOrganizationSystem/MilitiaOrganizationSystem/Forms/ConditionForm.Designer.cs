namespace MilitiaOrganizationSystem
{
    partial class ConditionForm
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
            this.parasCheckBox = new System.Windows.Forms.CheckedListBox();
            this.conditionListBox = new System.Windows.Forms.ListBox();
            this.rMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteCondition = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pCombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cCombobox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dCombobox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.rMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // parasCheckBox
            // 
            this.parasCheckBox.CheckOnClick = true;
            this.parasCheckBox.FormattingEnabled = true;
            this.parasCheckBox.Location = new System.Drawing.Point(12, 69);
            this.parasCheckBox.Name = "parasCheckBox";
            this.parasCheckBox.Size = new System.Drawing.Size(215, 324);
            this.parasCheckBox.TabIndex = 0;
            // 
            // conditionListBox
            // 
            this.conditionListBox.ContextMenuStrip = this.rMenuStrip;
            this.conditionListBox.FormattingEnabled = true;
            this.conditionListBox.HorizontalScrollbar = true;
            this.conditionListBox.ItemHeight = 12;
            this.conditionListBox.Location = new System.Drawing.Point(305, 65);
            this.conditionListBox.Name = "conditionListBox";
            this.conditionListBox.Size = new System.Drawing.Size(225, 328);
            this.conditionListBox.TabIndex = 1;
            // 
            // rMenuStrip
            // 
            this.rMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteCondition});
            this.rMenuStrip.Name = "rMenuStrip";
            this.rMenuStrip.Size = new System.Drawing.Size(125, 26);
            // 
            // deleteCondition
            // 
            this.deleteCondition.Name = "deleteCondition";
            this.deleteCondition.Size = new System.Drawing.Size(124, 22);
            this.deleteCondition.Text = "删除条件";
            this.deleteCondition.Click += new System.EventHandler(this.deleteCondition_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "双击选择属性添加条件:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "已添加条件(双击可编辑条件):";
            // 
            // pCombobox
            // 
            this.pCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pCombobox.FormattingEnabled = true;
            this.pCombobox.Location = new System.Drawing.Point(67, 9);
            this.pCombobox.Name = "pCombobox";
            this.pCombobox.Size = new System.Drawing.Size(121, 20);
            this.pCombobox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "采集地:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "省";
            // 
            // cCombobox
            // 
            this.cCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cCombobox.FormattingEnabled = true;
            this.cCombobox.Location = new System.Drawing.Point(217, 9);
            this.cCombobox.Name = "cCombobox";
            this.cCombobox.Size = new System.Drawing.Size(121, 20);
            this.cCombobox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(344, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "市";
            // 
            // dCombobox
            // 
            this.dCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dCombobox.FormattingEnabled = true;
            this.dCombobox.Location = new System.Drawing.Point(368, 8);
            this.dCombobox.Name = "dCombobox";
            this.dCombobox.Size = new System.Drawing.Size(121, 20);
            this.dCombobox.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(495, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "区/县";
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(152, 420);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 37);
            this.btn_cancel.TabIndex = 11;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(299, 420);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 37);
            this.btn_ok.TabIndex = 12;
            this.btn_ok.Text = "保存条件";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // ConditionForm
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(558, 469);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dCombobox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cCombobox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pCombobox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.conditionListBox);
            this.Controls.Add(this.parasCheckBox);
            this.DoubleBuffered = true;
            this.Name = "ConditionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "筛选条件";
            this.rMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox parasCheckBox;
        private System.Windows.Forms.ListBox conditionListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox pCombobox;//省
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cCombobox;//市
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox dCombobox;//区/县
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.ContextMenuStrip rMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteCondition;
    }
}