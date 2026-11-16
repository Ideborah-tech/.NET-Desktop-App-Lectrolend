// This file is written by the Visual Studio designer.
// To change how the window looks, double-click LoginForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class LoginForm
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
            this.formPanel = new System.Windows.Forms.Panel();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.brandLabel = new System.Windows.Forms.Label();
            this.universityLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.signInButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.newHereLabel = new System.Windows.Forms.Label();
            this.registerButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.demoCard = new LectroLend.Desktop.Views.UI.CardPanel();
            this.demoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.formPanel.SuspendLayout();
            this.demoCard.SuspendLayout();
            this.SuspendLayout();
            //
            // picture  (the welcome picture; it is loaded from Assets\welcome.jpg when the app runs)
            //
            this.picture.BackColor = System.Drawing.Color.White;
            this.picture.Dock = System.Windows.Forms.DockStyle.Left;
            this.picture.Location = new System.Drawing.Point(0, 0);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(520, 680);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabStop = false;
            //
            // formPanel  (the white right side)
            //
            this.formPanel.Controls.Add(this.logoPicture);
            this.formPanel.Controls.Add(this.brandLabel);
            this.formPanel.Controls.Add(this.universityLabel);
            this.formPanel.Controls.Add(this.titleLabel);
            this.formPanel.Controls.Add(this.subtitleLabel);
            this.formPanel.Controls.Add(this.emailLabel);
            this.formPanel.Controls.Add(this.emailBox);
            this.formPanel.Controls.Add(this.passwordLabel);
            this.formPanel.Controls.Add(this.passwordBox);
            this.formPanel.Controls.Add(this.signInButton);
            this.formPanel.Controls.Add(this.newHereLabel);
            this.formPanel.Controls.Add(this.registerButton);
            this.formPanel.Controls.Add(this.demoCard);
            this.formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formPanel.Location = new System.Drawing.Point(520, 0);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(580, 680);
            //
            // brandLabel
            //
            this.brandLabel.AutoSize = true;
            this.brandLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.brandLabel.ForeColor = System.Drawing.Color.FromArgb(44, 116, 179);
            this.brandLabel.Location = new System.Drawing.Point(158, 28);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.Text = "AUCA  ·  LECTROLEND";
            //
            // universityLabel
            //
            this.universityLabel.AutoSize = true;
            this.universityLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.universityLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.universityLabel.Location = new System.Drawing.Point(158, 48);
            this.universityLabel.Name = "universityLabel";
            this.universityLabel.Text = "Adventist University of Central Africa";
            //
            // logoPicture  (the AUCA logo; it is loaded from Assets\auca-logo.png when the app runs)
            //
            this.logoPicture.Location = new System.Drawing.Point(95, 20);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(54, 54);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPicture.TabStop = false;
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(90, 82);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Welcome back";
            //
            // subtitleLabel
            //
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.subtitleLabel.Location = new System.Drawing.Point(95, 140);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(390, 46);
            this.subtitleLabel.Text = "Sign in to borrow projectors, HDMI cables and remotes from the IT office.";
            //
            // emailLabel
            //
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.emailLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.emailLabel.Location = new System.Drawing.Point(95, 194);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Text = "Email";
            //
            // emailBox
            //
            this.emailBox.Hint = "you@auca.ac.rw";
            this.emailBox.Location = new System.Drawing.Point(95, 220);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(390, 44);
            this.emailBox.TabIndex = 0;
            //
            // passwordLabel
            //
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.passwordLabel.Location = new System.Drawing.Point(95, 280);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Text = "Password";
            //
            // passwordBox
            //
            this.passwordBox.Hint = "Your password";
            this.passwordBox.IsPassword = true;
            this.passwordBox.Location = new System.Drawing.Point(95, 306);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(390, 44);
            this.passwordBox.TabIndex = 1;
            //
            // signInButton
            //
            this.signInButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.signInButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.signInButton.ForeColor = System.Drawing.Color.White;
            this.signInButton.Location = new System.Drawing.Point(95, 376);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(390, 46);
            this.signInButton.TabIndex = 2;
            this.signInButton.Text = "Sign in";
            this.signInButton.Click += new System.EventHandler(this.SignInButton_Click);
            //
            // newHereLabel
            //
            this.newHereLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.newHereLabel.Location = new System.Drawing.Point(95, 440);
            this.newHereLabel.Name = "newHereLabel";
            this.newHereLabel.Size = new System.Drawing.Size(390, 26);
            this.newHereLabel.Text = "New to LectroLend?";
            this.newHereLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // registerButton
            //
            this.registerButton.BackColor = System.Drawing.Color.FromArgb(232, 240, 250);
            this.registerButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.registerButton.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.registerButton.Location = new System.Drawing.Point(95, 470);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(390, 46);
            this.registerButton.TabIndex = 3;
            this.registerButton.Text = "Create an account";
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            //
            // demoCard  (the grey box with the demo accounts)
            //
            this.demoCard.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.demoCard.Controls.Add(this.demoLabel);
            this.demoCard.Location = new System.Drawing.Point(95, 556);
            this.demoCard.Name = "demoCard";
            this.demoCard.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.demoCard.Size = new System.Drawing.Size(390, 96);
            //
            // demoLabel
            //
            this.demoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.demoLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.demoLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.demoLabel.Name = "demoLabel";
            this.demoLabel.Text = "Demo accounts\r\nStudent:  student@auca.ac.rw  /  Student@123\r\nLecturer:  lecturer@auca.ac.rw  /  Lecturer@123\r\nIT admin:  admin@auca.ac.rw  /  Admin@123";
            //
            // LoginForm
            //
            this.AcceptButton = this.signInButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.formPanel);
            this.Controls.Add(this.picture);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LectroLend - Sign in";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.demoCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label universityLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label emailLabel;
        private LectroLend.Desktop.Views.UI.InputBox emailBox;
        private System.Windows.Forms.Label passwordLabel;
        private LectroLend.Desktop.Views.UI.InputBox passwordBox;
        private LectroLend.Desktop.Views.UI.RoundButton signInButton;
        private System.Windows.Forms.Label newHereLabel;
        private LectroLend.Desktop.Views.UI.RoundButton registerButton;
        private LectroLend.Desktop.Views.UI.CardPanel demoCard;
        private System.Windows.Forms.Label demoLabel;
    }
}
