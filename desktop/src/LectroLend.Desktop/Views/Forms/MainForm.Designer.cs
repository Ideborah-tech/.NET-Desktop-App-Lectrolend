// This file is written by the Visual Studio designer.
// To change how the window looks, double-click MainForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.topBar = new System.Windows.Forms.Panel();
            this.appNameLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.signOutButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.topBar.SuspendLayout();
            this.SuspendLayout();
            //
            // topBar  (the dark bar at the top)
            //
            this.topBar.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.topBar.Controls.Add(this.appNameLabel);
            this.topBar.Controls.Add(this.userLabel);
            this.topBar.Controls.Add(this.signOutButton);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Location = new System.Drawing.Point(0, 0);
            this.topBar.Name = "topBar";
            this.topBar.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.topBar.Size = new System.Drawing.Size(1280, 60);
            //
            // appNameLabel
            //
            this.appNameLabel.AutoSize = true;
            this.appNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.appNameLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.appNameLabel.ForeColor = System.Drawing.Color.White;
            this.appNameLabel.Name = "appNameLabel";
            this.appNameLabel.Text = "LectroLend";
            //
            // userLabel  (the name of the person who is signed in, set in MainForm.cs)
            //
            this.userLabel.AutoSize = true;
            this.userLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.userLabel.ForeColor = System.Drawing.Color.White;
            this.userLabel.Name = "userLabel";
            this.userLabel.Padding = new System.Windows.Forms.Padding(0, 8, 16, 0);
            this.userLabel.Text = "Name (Role)";
            //
            // signOutButton
            //
            this.signOutButton.BackColor = System.Drawing.Color.FromArgb(255, 201, 60);
            this.signOutButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.signOutButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.signOutButton.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(110, 36);
            this.signOutButton.Text = "Sign out";
            this.signOutButton.Click += new System.EventHandler(this.SignOutButton_Click);
            //
            // menuPanel  (the menu on the left; the buttons are added in MainForm.cs)
            //
            this.menuPanel.BackColor = System.Drawing.Color.White;
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 60);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
            this.menuPanel.Size = new System.Drawing.Size(200, 720);
            //
            // pagePanel  (where the chosen page is shown)
            //
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(200, 60);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.pagePanel.Size = new System.Drawing.Size(1080, 720);
            //
            // timer  (every minute: expire old bookings and mark late items)
            //
            this.timer.Interval = 60000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.ClientSize = new System.Drawing.Size(1280, 780);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.topBar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LectroLend";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.topBar.ResumeLayout(false);
            this.topBar.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Label appNameLabel;
        private System.Windows.Forms.Label userLabel;
        private LectroLend.Desktop.Views.UI.RoundButton signOutButton;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Panel pagePanel;
        private System.Windows.Forms.Timer timer;
    }
}
