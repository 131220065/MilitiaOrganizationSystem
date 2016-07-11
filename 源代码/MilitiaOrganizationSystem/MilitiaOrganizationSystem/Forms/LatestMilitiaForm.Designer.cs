namespace MilitiaOrganizationSystem
{
    partial class LatestMilitiaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LatestMilitiaForm));
            this.latestMilitias_listview = new System.Windows.Forms.ListView();
            this.militiaImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // latestMilitias_listview
            // 
            this.latestMilitias_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.latestMilitias_listview.FullRowSelect = true;
            this.latestMilitias_listview.Location = new System.Drawing.Point(12, 12);
            this.latestMilitias_listview.Name = "latestMilitias_listview";
            this.latestMilitias_listview.ShowItemToolTips = true;
            this.latestMilitias_listview.Size = new System.Drawing.Size(598, 499);
            this.latestMilitias_listview.SmallImageList = this.militiaImageList;
            this.latestMilitias_listview.TabIndex = 0;
            this.latestMilitias_listview.UseCompatibleStateImageBehavior = false;
            this.latestMilitias_listview.View = System.Windows.Forms.View.Details;
            // 
            // militiaImageList
            // 
            this.militiaImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("militiaImageList.ImageStream")));
            this.militiaImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.militiaImageList.Images.SetKeyName(0, "u=1978385908,3347853851&fm=21&gp=0.jpg");
            // 
            // LatestMilitiaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 523);
            this.Controls.Add(this.latestMilitias_listview);
            this.Name = "LatestMilitiaForm";
            this.Text = "最近编辑的民兵";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView latestMilitias_listview;
        private System.Windows.Forms.ImageList militiaImageList;
    }
}