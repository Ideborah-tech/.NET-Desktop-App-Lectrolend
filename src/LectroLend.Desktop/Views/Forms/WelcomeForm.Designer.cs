// This file is written by the Visual Studio designer.
// To change how the window looks, double-click WelcomeForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class WelcomeForm
    {
        private System.ComponentModel.IContainer components = null;

        // Frees the window's resources when it closes.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.universityLabel = new System.Windows.Forms.Label();
            this.officeLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.headlineLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.getStartedButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.featuresLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.SuspendLayout();
            //
            // picture  (the welcome picture on the right; loaded from Assets\welcome.jpg when the app runs)
            //
            this.picture.BackColor = System.Drawing.Color.White;
            this.picture.Dock = System.Windows.Forms.DockStyle.Right;
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(560, 680);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabStop = false;
            //
            // logoPicture  (the AUCA logo; loaded from Assets\auca-logo.png when the app runs)
            //
            this.logoPicture.Location = new System.Drawing.Point(70, 56);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(64, 64);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPicture.TabStop = false;
            //
            // universityLabel
            //
            this.universityLabel.AutoSize = true;
            this.universityLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.universityLabel.ForeColor = System.Drawing.Color.FromArgb(44, 116, 179);
            this.universityLabel.Location = new System.Drawing.Point(146, 66);
            this.universityLabel.Name = "universityLabel";
            this.universityLabel.Text = "ADVENTIST UNIVERSITY OF CENTRAL AFRICA";
            //
            // officeLabel
            //
            this.officeLabel.AutoSize = true;
            this.officeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.officeLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.officeLabel.Location = new System.Drawing.Point(146, 88);
            this.officeLabel.Name = "officeLabel";
            this.officeLabel.Text = "IT Office  ·  Equipment lending";
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(60, 176);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "LectroLend";
            //
            // headlineLabel
            //
            this.headlineLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.headlineLabel.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.headlineLabel.Location = new System.Drawing.Point(70, 256);
            this.headlineLabel.Name = "headlineLabel";
            this.headlineLabel.Size = new System.Drawing.Size(440, 66);
            this.headlineLabel.Text = "Borrow the equipment you need for your classes.";
            //
            // descriptionLabel
            //
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.descriptionLabel.Location = new System.Drawing.Point(70, 332);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(440, 70);
            this.descriptionLabel.Text = "Book a projector, an HDMI cable or a remote in a minute, pick it up at the IT office, and bring it back on time.";
            //
            // getStartedButton
            //
            this.getStartedButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.getStartedButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.getStartedButton.ForeColor = System.Drawing.Color.White;
            this.getStartedButton.Location = new System.Drawing.Point(70, 428);
            this.getStartedButton.Name = "getStartedButton";
            this.getStartedButton.Size = new System.Drawing.Size(220, 54);
            this.getStartedButton.TabIndex = 0;
            this.getStartedButton.Text = "Get started  →";
            this.getStartedButton.Click += new System.EventHandler(this.GetStartedButton_Click);
            //
            // featuresLabel
            //
            this.featuresLabel.AutoSize = true;
            this.featuresLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.featuresLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.featuresLabel.Location = new System.Drawing.Point(70, 510);
            this.featuresLabel.Name = "featuresLabel";
            this.featuresLabel.Text = "Projectors  ·  HDMI cables  ·  Remotes  ·  Adapters";
            //
            // WelcomeForm
            //
            this.AcceptButton = this.getStartedButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.universityLabel);
            this.Controls.Add(this.officeLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.headlineLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.getStartedButton);
            this.Controls.Add(this.featuresLabel);
            this.Controls.Add(this.picture);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LectroLend - Welcome";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.Label universityLabel;
        private System.Windows.Forms.Label officeLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label headlineLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private LectroLend.Desktop.Views.UI.RoundButton getStartedButton;
        private System.Windows.Forms.Label featuresLabel;
    }
}
