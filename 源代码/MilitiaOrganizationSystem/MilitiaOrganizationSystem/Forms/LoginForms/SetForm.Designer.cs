namespace MilitiaOrganizationSystem
{
    partial class SetForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_sheng = new System.Windows.Forms.ComboBox();
            this.comboBox_shi = new System.Windows.Forms.ComboBox();
            this.comboBox_xian = new System.Windows.Forms.ComboBox();
            this.label_sheng = new System.Windows.Forms.Label();
            this.label_shi = new System.Windows.Forms.Label();
            this.label_xian = new System.Windows.Forms.Label();
            this.initialPsdBox = new System.Windows.Forms.TextBox();
            this.psdCombobox = new System.Windows.Forms.TextBox();
            this.psd2Combobox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.clientTypeCombobox = new System.Windows.Forms.ComboBox();
            this.textBox_BasicLevel = new System.Windows.Forms.TextBox();
            this.label_BasicLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "检测到你还没有设置身份，请确认：";
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(230, 291);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "确认设置";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(32, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "请设置用户名和密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(107, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "初始密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(107, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "确认密码：";
            // 
            // comboBox_sheng
            // 
            this.comboBox_sheng.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sheng.FormattingEnabled = true;
            this.comboBox_sheng.Location = new System.Drawing.Point(95, 70);
            this.comboBox_sheng.Name = "comboBox_sheng";
            this.comboBox_sheng.Size = new System.Drawing.Size(78, 20);
            this.comboBox_sheng.TabIndex = 6;
            // 
            // comboBox_shi
            // 
            this.comboBox_shi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_shi.FormattingEnabled = true;
            this.comboBox_shi.Location = new System.Drawing.Point(202, 70);
            this.comboBox_shi.Name = "comboBox_shi";
            this.comboBox_shi.Size = new System.Drawing.Size(82, 20);
            this.comboBox_shi.TabIndex = 7;
            this.comboBox_shi.Visible = false;
            this.comboBox_shi.SelectedIndexChanged += new System.EventHandler(this.comboBox_shi_SelectedIndexChanged);
            // 
            // comboBox_xian
            // 
            this.comboBox_xian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_xian.FormattingEnabled = true;
            this.comboBox_xian.Location = new System.Drawing.Point(313, 70);
            this.comboBox_xian.Name = "comboBox_xian";
            this.comboBox_xian.Size = new System.Drawing.Size(77, 20);
            this.comboBox_xian.TabIndex = 8;
            this.comboBox_xian.Visible = false;
            // 
            // label_sheng
            // 
            this.label_sheng.AutoSize = true;
            this.label_sheng.Location = new System.Drawing.Point(179, 78);
            this.label_sheng.Name = "label_sheng";
            this.label_sheng.Size = new System.Drawing.Size(17, 12);
            this.label_sheng.TabIndex = 9;
            this.label_sheng.Text = "省";
            // 
            // label_shi
            // 
            this.label_shi.AutoSize = true;
            this.label_shi.Location = new System.Drawing.Point(290, 78);
            this.label_shi.Name = "label_shi";
            this.label_shi.Size = new System.Drawing.Size(17, 12);
            this.label_shi.TabIndex = 10;
            this.label_shi.Text = "市";
            this.label_shi.Visible = false;
            // 
            // label_xian
            // 
            this.label_xian.AutoSize = true;
            this.label_xian.Location = new System.Drawing.Point(396, 78);
            this.label_xian.Name = "label_xian";
            this.label_xian.Size = new System.Drawing.Size(35, 12);
            this.label_xian.TabIndex = 11;
            this.label_xian.Text = "区/县";
            this.label_xian.Visible = false;
            // 
            // initialPsdBox
            // 
            this.initialPsdBox.Location = new System.Drawing.Point(221, 140);
            this.initialPsdBox.Name = "initialPsdBox";
            this.initialPsdBox.PasswordChar = '*';
            this.initialPsdBox.Size = new System.Drawing.Size(100, 21);
            this.initialPsdBox.TabIndex = 12;
            // 
            // psdCombobox
            // 
            this.psdCombobox.Location = new System.Drawing.Point(221, 173);
            this.psdCombobox.Name = "psdCombobox";
            this.psdCombobox.PasswordChar = '*';
            this.psdCombobox.Size = new System.Drawing.Size(100, 21);
            this.psdCombobox.TabIndex = 13;
            // 
            // psd2Combobox
            // 
            this.psd2Combobox.Location = new System.Drawing.Point(221, 204);
            this.psd2Combobox.Name = "psd2Combobox";
            this.psd2Combobox.PasswordChar = '*';
            this.psd2Combobox.Size = new System.Drawing.Size(100, 21);
            this.psd2Combobox.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(151, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(215, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "以上信息都为必填项,请仔细选择并填写";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(52, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "地区：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(54, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "客户端类型：";
            // 
            // clientTypeCombobox
            // 
            this.clientTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientTypeCombobox.FormattingEnabled = true;
            this.clientTypeCombobox.Location = new System.Drawing.Point(137, 36);
            this.clientTypeCombobox.Name = "clientTypeCombobox";
            this.clientTypeCombobox.Size = new System.Drawing.Size(121, 20);
            this.clientTypeCombobox.TabIndex = 19;
            this.clientTypeCombobox.SelectedIndexChanged += new System.EventHandler(this.clientTypeCombobox_SelectedIndexChanged);
            // 
            // textBox_BasicLevel
            // 
            this.textBox_BasicLevel.Location = new System.Drawing.Point(437, 70);
            this.textBox_BasicLevel.Name = "textBox_BasicLevel";
            this.textBox_BasicLevel.Size = new System.Drawing.Size(70, 21);
            this.textBox_BasicLevel.TabIndex = 20;
            this.textBox_BasicLevel.Visible = false;
            // 
            // label_BasicLevel
            // 
            this.label_BasicLevel.AutoSize = true;
            this.label_BasicLevel.Location = new System.Drawing.Point(516, 77);
            this.label_BasicLevel.Name = "label_BasicLevel";
            this.label_BasicLevel.Size = new System.Drawing.Size(29, 12);
            this.label_BasicLevel.TabIndex = 21;
            this.label_BasicLevel.Text = "基层";
            this.label_BasicLevel.Visible = false;
            // 
            // SetForm
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 326);
            this.Controls.Add(this.label_BasicLevel);
            this.Controls.Add(this.textBox_BasicLevel);
            this.Controls.Add(this.clientTypeCombobox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.psd2Combobox);
            this.Controls.Add(this.psdCombobox);
            this.Controls.Add(this.initialPsdBox);
            this.Controls.Add(this.label_xian);
            this.Controls.Add(this.label_shi);
            this.Controls.Add(this.label_sheng);
            this.Controls.Add(this.comboBox_xian);
            this.Controls.Add(this.comboBox_shi);
            this.Controls.Add(this.comboBox_sheng);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "SetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "欢迎使用民兵编组系统";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_sheng;
        private System.Windows.Forms.ComboBox comboBox_shi;
        private System.Windows.Forms.ComboBox comboBox_xian;
        private System.Windows.Forms.Label label_sheng;
        private System.Windows.Forms.Label label_shi;
        private System.Windows.Forms.Label label_xian;
        private System.Windows.Forms.TextBox initialPsdBox;
        private System.Windows.Forms.TextBox psdCombobox;
        private System.Windows.Forms.TextBox psd2Combobox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox clientTypeCombobox;
        private System.Windows.Forms.TextBox textBox_BasicLevel;
        private System.Windows.Forms.Label label_BasicLevel;
    }
}