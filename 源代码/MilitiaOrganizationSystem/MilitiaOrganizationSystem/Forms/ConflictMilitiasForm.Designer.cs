namespace MilitiaOrganizationSystem
{
    partial class ConflictMilitiasForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConflictMilitiasForm));
            this.conflictGroup_ListView = new System.Windows.Forms.ListView();
            this.militiaImageList = new System.Windows.Forms.ImageList(this.components);
            this.label_help = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // conflictGroup_ListView
            // 
            this.conflictGroup_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conflictGroup_ListView.CheckBoxes = true;
            this.conflictGroup_ListView.Location = new System.Drawing.Point(12, 47);
            this.conflictGroup_ListView.Name = "conflictGroup_ListView";
            this.conflictGroup_ListView.Size = new System.Drawing.Size(613, 304);
            this.conflictGroup_ListView.SmallImageList = this.militiaImageList;
            this.conflictGroup_ListView.TabIndex = 0;
            this.conflictGroup_ListView.UseCompatibleStateImageBehavior = false;
            this.conflictGroup_ListView.View = System.Windows.Forms.View.Details;
            // 
            // militiaImageList
            // 
            this.militiaImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("militiaImageList.ImageStream")));
            this.militiaImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.militiaImageList.Images.SetKeyName(0, "u=1978385908,3347853851&fm=21&gp=0.jpg");
            // 
            // label_help
            // 
            this.label_help.AutoSize = true;
            this.label_help.Location = new System.Drawing.Point(10, 18);
            this.label_help.Name = "label_help";
            this.label_help.Size = new System.Drawing.Size(161, 12);
            this.label_help.TabIndex = 1;
            this.label_help.Text = "请勾选每组中想保留的一项：";
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(273, 357);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 44);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "保留所选";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // ConflictMilitiasForm
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 402);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label_help);
            this.Controls.Add(this.conflictGroup_ListView);
            this.DoubleBuffered = true;
            this.Name = "ConflictMilitiasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 冲突检测";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView conflictGroup_ListView;
        private System.Windows.Forms.Label label_help;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.ImageList militiaImageList;
    }
}