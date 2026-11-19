// This file is written by the Visual Studio designer.
// To change how the window looks, double-click ConnectionForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class ConnectionForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.introLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseBox = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.connectButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Connect to PostgreSQL";
            //
            // introLabel
            //
            this.introLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.introLabel.Location = new System.Drawing.Point(24, 66);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(412, 40);
            this.introLabel.Text = "Enter your PostgreSQL details. The database and tables are created automatically.";
            //
            // errorLabel
            //
            this.errorLabel.ForeColor = System.Drawing.Color.FromArgb(200, 40, 40);
            this.errorLabel.Location = new System.Drawing.Point(24, 106);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(412, 40);
            //
            // hostLabel
            //
            this.hostLabel.AutoSize = true;
            this.hostLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.hostLabel.Location = new System.Drawing.Point(24, 155);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Text = "Host";
            //
            // hostBox
            //
            this.hostBox.Location = new System.Drawing.Point(150, 152);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(286, 25);
            this.hostBox.TabIndex = 0;
            //
            // portLabel
            //
            this.portLabel.AutoSize = true;
            this.portLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.portLabel.Location = new System.Drawing.Point(24, 195);
            this.portLabel.Name = "portLabel";
            this.portLabel.Text = "Port";
            //
            // portBox
            //
            this.portBox.Location = new System.Drawing.Point(150, 192);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(286, 25);
            this.portBox.TabIndex = 1;
            //
            // databaseLabel
            //
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.databaseLabel.Location = new System.Drawing.Point(24, 235);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Text = "Database";
            //
            // databaseBox
            //
            this.databaseBox.Location = new System.Drawing.Point(150, 232);
            this.databaseBox.Name = "databaseBox";
            this.databaseBox.Size = new System.Drawing.Size(286, 25);
            this.databaseBox.TabIndex = 2;
            //
            // userLabel
            //
            this.userLabel.AutoSize = true;
            this.userLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.userLabel.Location = new System.Drawing.Point(24, 275);
            this.userLabel.Name = "userLabel";
            this.userLabel.Text = "User name";
            //
            // userBox
            //
            this.userBox.Location = new System.Drawing.Point(150, 272);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(286, 25);
            this.userBox.TabIndex = 3;
            //
            // passwordLabel
            //
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.passwordLabel.Location = new System.Drawing.Point(24, 315);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Text = "Password";
            //
            // passwordBox
            //
            this.passwordBox.Location = new System.Drawing.Point(150, 312);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(286, 25);
            this.passwordBox.TabIndex = 4;
            this.passwordBox.UseSystemPasswordChar = true;
            //
            // connectButton
            //
            this.connectButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.connectButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.connectButton.ForeColor = System.Drawing.Color.White;
            this.connectButton.Location = new System.Drawing.Point(24, 356);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(412, 42);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect";
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            //
            // ConnectionForm
            //
            this.AcceptButton = this.connectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 420);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.introLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.hostBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.databaseBox);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.connectButton);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to PostgreSQL";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.TextBox databaseBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private LectroLend.Desktop.Views.UI.RoundButton connectButton;
    }
}
