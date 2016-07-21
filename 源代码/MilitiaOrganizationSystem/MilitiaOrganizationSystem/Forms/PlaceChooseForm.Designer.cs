namespace MilitiaOrganizationSystem
{
    partial class PlaceChooseForm
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
            this.pCombobox = new System.Windows.Forms.ComboBox();
            this.labelPropertyName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cCombobox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dCombobox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.place_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pCombobox
            // 
            this.pCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pCombobox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pCombobox.FormattingEnabled = true;
            this.pCombobox.Location = new System.Drawing.Point(67, 9);
            this.pCombobox.Name = "pCombobox";
            this.pCombobox.Size = new System.Drawing.Size(121, 20);
            this.pCombobox.TabIndex = 4;
            // 
            // labelPropertyName
            // 
            this.labelPropertyName.AutoSize = true;
            this.labelPropertyName.Location = new System.Drawing.Point(14, 12);
            this.labelPropertyName.Name = "labelPropertyName";
            this.labelPropertyName.Size = new System.Drawing.Size(47, 12);
            this.labelPropertyName.TabIndex = 5;
            this.labelPropertyName.Text = "采集地:";
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
            this.btn_cancel.Location = new System.Drawing.Point(136, 107);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 37);
            this.btn_cancel.TabIndex = 11;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(334, 107);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 37);
            this.btn_ok.TabIndex = 12;
            this.btn_ok.Text = "保存条件";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // place_textBox
            // 
            this.place_textBox.Location = new System.Drawing.Point(142, 55);
            this.place_textBox.Name = "place_textBox";
            this.place_textBox.Size = new System.Drawing.Size(353, 21);
            this.place_textBox.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "具体地址:";
            // 
            // PlaceChooseForm
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(558, 178);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.place_textBox);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dCombobox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cCombobox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelPropertyName);
            this.Controls.Add(this.pCombobox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PlaceChooseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择地区";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox pCombobox;//省
        private System.Windows.Forms.Label labelPropertyName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cCombobox;//市
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox dCombobox;//区/县
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TextBox place_textBox;
        private System.Windows.Forms.Label label1;
    }
}