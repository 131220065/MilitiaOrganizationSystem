namespace MilitiaOrganizationSystem
{
    partial class ChildConditionForm_string
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
            this.propertyName = new System.Windows.Forms.Label();
            this.radio_StartsWith = new System.Windows.Forms.RadioButton();
            this.startwithCombobox = new System.Windows.Forms.ComboBox();
            this.radio_EndsWith = new System.Windows.Forms.RadioButton();
            this.endswithCombobox = new System.Windows.Forms.ComboBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.radio_Equal = new System.Windows.Forms.RadioButton();
            this.equalCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // propertyName
            // 
            this.propertyName.AutoSize = true;
            this.propertyName.Location = new System.Drawing.Point(12, 101);
            this.propertyName.Name = "propertyName";
            this.propertyName.Size = new System.Drawing.Size(41, 12);
            this.propertyName.TabIndex = 0;
            this.propertyName.Text = "属性名";
            // 
            // radio_StartsWith
            // 
            this.radio_StartsWith.AutoSize = true;
            this.radio_StartsWith.Location = new System.Drawing.Point(14, 130);
            this.radio_StartsWith.Name = "radio_StartsWith";
            this.radio_StartsWith.Size = new System.Drawing.Size(83, 16);
            this.radio_StartsWith.TabIndex = 1;
            this.radio_StartsWith.TabStop = true;
            this.radio_StartsWith.Text = "StartsWith";
            this.radio_StartsWith.UseVisualStyleBackColor = true;
            // 
            // startwithCombobox
            // 
            this.startwithCombobox.FormattingEnabled = true;
            this.startwithCombobox.Location = new System.Drawing.Point(103, 126);
            this.startwithCombobox.Name = "startwithCombobox";
            this.startwithCombobox.Size = new System.Drawing.Size(121, 20);
            this.startwithCombobox.TabIndex = 2;
            // 
            // radio_EndsWith
            // 
            this.radio_EndsWith.AutoSize = true;
            this.radio_EndsWith.Location = new System.Drawing.Point(14, 164);
            this.radio_EndsWith.Name = "radio_EndsWith";
            this.radio_EndsWith.Size = new System.Drawing.Size(71, 16);
            this.radio_EndsWith.TabIndex = 3;
            this.radio_EndsWith.TabStop = true;
            this.radio_EndsWith.Text = "EndsWith";
            this.radio_EndsWith.UseVisualStyleBackColor = true;
            // 
            // endswithCombobox
            // 
            this.endswithCombobox.FormattingEnabled = true;
            this.endswithCombobox.Location = new System.Drawing.Point(103, 160);
            this.endswithCombobox.Name = "endswithCombobox";
            this.endswithCombobox.Size = new System.Drawing.Size(121, 20);
            this.endswithCombobox.TabIndex = 4;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(303, 30);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 83);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "添加条件";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(303, 164);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 82);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // radio_Equal
            // 
            this.radio_Equal.AutoSize = true;
            this.radio_Equal.Location = new System.Drawing.Point(14, 197);
            this.radio_Equal.Name = "radio_Equal";
            this.radio_Equal.Size = new System.Drawing.Size(53, 16);
            this.radio_Equal.TabIndex = 7;
            this.radio_Equal.TabStop = true;
            this.radio_Equal.Text = "Equal";
            this.radio_Equal.UseVisualStyleBackColor = true;
            // 
            // equalCombobox
            // 
            this.equalCombobox.FormattingEnabled = true;
            this.equalCombobox.Location = new System.Drawing.Point(103, 193);
            this.equalCombobox.Name = "equalCombobox";
            this.equalCombobox.Size = new System.Drawing.Size(121, 20);
            this.equalCombobox.TabIndex = 8;
            // 
            // ChildConditionForm_string
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(396, 294);
            this.Controls.Add(this.equalCombobox);
            this.Controls.Add(this.radio_Equal);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.endswithCombobox);
            this.Controls.Add(this.radio_EndsWith);
            this.Controls.Add(this.startwithCombobox);
            this.Controls.Add(this.radio_StartsWith);
            this.Controls.Add(this.propertyName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChildConditionForm_string";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChildConditionForm_string";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label propertyName;
        private System.Windows.Forms.RadioButton radio_StartsWith;
        private System.Windows.Forms.ComboBox startwithCombobox;
        private System.Windows.Forms.RadioButton radio_EndsWith;
        private System.Windows.Forms.ComboBox endswithCombobox;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.RadioButton radio_Equal;
        private System.Windows.Forms.ComboBox equalCombobox;
    }
}