// This file is written by the Visual Studio designer.
// To change how the page looks, double-click ProfilePage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class ProfilePage
    {
        private System.ComponentModel.IContainer components = null;

        // Frees the page's resources when it closes.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.detailsGroup = new System.Windows.Forms.GroupBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneBox = new System.Windows.Forms.TextBox();
            this.facultyLabel = new System.Windows.Forms.Label();
            this.facultyBox = new System.Windows.Forms.TextBox();
            this.programLabel = new System.Windows.Forms.Label();
            this.programBox = new System.Windows.Forms.TextBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentBox = new System.Windows.Forms.TextBox();
            this.saveButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.spacePanel = new System.Windows.Forms.Panel();
            this.passwordGroup = new System.Windows.Forms.GroupBox();
            this.currentPasswordLabel = new System.Windows.Forms.Label();
            this.currentPasswordBox = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordBox = new System.Windows.Forms.TextBox();
            this.repeatPasswordLabel = new System.Windows.Forms.Label();
            this.repeatPasswordBox = new System.Windows.Forms.TextBox();
            this.changeButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.detailsGroup.SuspendLayout();
            this.passwordGroup.SuspendLayout();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1000, 50);
            this.titleLabel.Text = "Profile";
            //
            // detailsGroup  (box 1: your details)
            //
            this.detailsGroup.BackColor = System.Drawing.Color.White;
            this.detailsGroup.Controls.Add(this.nameLabel);
            this.detailsGroup.Controls.Add(this.nameBox);
            this.detailsGroup.Controls.Add(this.emailLabel);
            this.detailsGroup.Controls.Add(this.emailBox);
            this.detailsGroup.Controls.Add(this.phoneLabel);
            this.detailsGroup.Controls.Add(this.phoneBox);
            this.detailsGroup.Controls.Add(this.facultyLabel);
            this.detailsGroup.Controls.Add(this.facultyBox);
            this.detailsGroup.Controls.Add(this.programLabel);
            this.detailsGroup.Controls.Add(this.programBox);
            this.detailsGroup.Controls.Add(this.departmentLabel);
            this.detailsGroup.Controls.Add(this.departmentBox);
            this.detailsGroup.Controls.Add(this.saveButton);
            this.detailsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailsGroup.Name = "detailsGroup";
            this.detailsGroup.Size = new System.Drawing.Size(1000, 336);
            this.detailsGroup.Text = "Your details";
            //
            // nameLabel / nameBox
            //
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.nameLabel.Location = new System.Drawing.Point(20, 40);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Text = "Full name";
            this.nameBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.nameBox.Location = new System.Drawing.Point(200, 36);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(760, 25);
            this.nameBox.TabIndex = 0;
            //
            // emailLabel / emailBox  (the email cannot be changed)
            //
            this.emailLabel.AutoSize = true;
            this.emailLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.emailLabel.Location = new System.Drawing.Point(20, 80);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Text = "Email";
            this.emailBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.emailBox.Location = new System.Drawing.Point(200, 76);
            this.emailBox.Name = "emailBox";
            this.emailBox.ReadOnly = true;
            this.emailBox.Size = new System.Drawing.Size(760, 25);
            this.emailBox.TabStop = false;
            //
            // phoneLabel / phoneBox
            //
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.phoneLabel.Location = new System.Drawing.Point(20, 120);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Text = "Phone";
            this.phoneBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.phoneBox.Location = new System.Drawing.Point(200, 116);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(760, 25);
            this.phoneBox.TabIndex = 1;
            //
            // facultyLabel / facultyBox  (students only)
            //
            this.facultyLabel.AutoSize = true;
            this.facultyLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.facultyLabel.Location = new System.Drawing.Point(20, 160);
            this.facultyLabel.Name = "facultyLabel";
            this.facultyLabel.Text = "Faculty";
            this.facultyBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.facultyBox.Location = new System.Drawing.Point(200, 156);
            this.facultyBox.Name = "facultyBox";
            this.facultyBox.Size = new System.Drawing.Size(760, 25);
            this.facultyBox.TabIndex = 2;
            //
            // programLabel / programBox  (students only)
            //
            this.programLabel.AutoSize = true;
            this.programLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.programLabel.Location = new System.Drawing.Point(20, 200);
            this.programLabel.Name = "programLabel";
            this.programLabel.Text = "Program";
            this.programBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.programBox.Location = new System.Drawing.Point(200, 196);
            this.programBox.Name = "programBox";
            this.programBox.Size = new System.Drawing.Size(760, 25);
            this.programBox.TabIndex = 3;
            //
            // departmentLabel / departmentBox  (lecturers only)
            //
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.departmentLabel.Location = new System.Drawing.Point(20, 240);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Text = "Department";
            this.departmentBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.departmentBox.Location = new System.Drawing.Point(200, 236);
            this.departmentBox.Name = "departmentBox";
            this.departmentBox.Size = new System.Drawing.Size(760, 25);
            this.departmentBox.TabIndex = 4;
            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(200, 280);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(160, 38);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save changes";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            //
            // spacePanel  (a small empty space between the two boxes)
            //
            this.spacePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spacePanel.Name = "spacePanel";
            this.spacePanel.Size = new System.Drawing.Size(1000, 16);
            //
            // passwordGroup  (box 2: change password)
            //
            this.passwordGroup.BackColor = System.Drawing.Color.White;
            this.passwordGroup.Controls.Add(this.currentPasswordLabel);
            this.passwordGroup.Controls.Add(this.currentPasswordBox);
            this.passwordGroup.Controls.Add(this.newPasswordLabel);
            this.passwordGroup.Controls.Add(this.newPasswordBox);
            this.passwordGroup.Controls.Add(this.repeatPasswordLabel);
            this.passwordGroup.Controls.Add(this.repeatPasswordBox);
            this.passwordGroup.Controls.Add(this.changeButton);
            this.passwordGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordGroup.Name = "passwordGroup";
            this.passwordGroup.Size = new System.Drawing.Size(1000, 216);
            this.passwordGroup.Text = "Change password";
            //
            // currentPasswordLabel / currentPasswordBox
            //
            this.currentPasswordLabel.AutoSize = true;
            this.currentPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.currentPasswordLabel.Location = new System.Drawing.Point(20, 40);
            this.currentPasswordLabel.Name = "currentPasswordLabel";
            this.currentPasswordLabel.Text = "Current password";
            this.currentPasswordBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.currentPasswordBox.Location = new System.Drawing.Point(200, 36);
            this.currentPasswordBox.Name = "currentPasswordBox";
            this.currentPasswordBox.Size = new System.Drawing.Size(760, 25);
            this.currentPasswordBox.TabIndex = 6;
            this.currentPasswordBox.UseSystemPasswordChar = true;
            //
            // newPasswordLabel / newPasswordBox
            //
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.newPasswordLabel.Location = new System.Drawing.Point(20, 80);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Text = "New password";
            this.newPasswordBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.newPasswordBox.Location = new System.Drawing.Point(200, 76);
            this.newPasswordBox.Name = "newPasswordBox";
            this.newPasswordBox.Size = new System.Drawing.Size(760, 25);
            this.newPasswordBox.TabIndex = 7;
            this.newPasswordBox.UseSystemPasswordChar = true;
            //
            // repeatPasswordLabel / repeatPasswordBox
            //
            this.repeatPasswordLabel.AutoSize = true;
            this.repeatPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.repeatPasswordLabel.Location = new System.Drawing.Point(20, 120);
            this.repeatPasswordLabel.Name = "repeatPasswordLabel";
            this.repeatPasswordLabel.Text = "Repeat new password";
            this.repeatPasswordBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.repeatPasswordBox.Location = new System.Drawing.Point(200, 116);
            this.repeatPasswordBox.Name = "repeatPasswordBox";
            this.repeatPasswordBox.Size = new System.Drawing.Size(760, 25);
            this.repeatPasswordBox.TabIndex = 8;
            this.repeatPasswordBox.UseSystemPasswordChar = true;
            //
            // changeButton
            //
            this.changeButton.BackColor = System.Drawing.Color.FromArgb(44, 116, 179);
            this.changeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.changeButton.ForeColor = System.Drawing.Color.White;
            this.changeButton.Location = new System.Drawing.Point(200, 160);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(180, 38);
            this.changeButton.TabIndex = 9;
            this.changeButton.Text = "Change password";
            this.changeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            //
            // ProfilePage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.passwordGroup);
            this.Controls.Add(this.spacePanel);
            this.Controls.Add(this.detailsGroup);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ProfilePage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.ProfilePage_Load);
            this.detailsGroup.ResumeLayout(false);
            this.detailsGroup.PerformLayout();
            this.passwordGroup.ResumeLayout(false);
            this.passwordGroup.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.GroupBox detailsGroup;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox phoneBox;
        private System.Windows.Forms.Label facultyLabel;
        private System.Windows.Forms.TextBox facultyBox;
        private System.Windows.Forms.Label programLabel;
        private System.Windows.Forms.TextBox programBox;
        private System.Windows.Forms.Label departmentLabel;
        private System.Windows.Forms.TextBox departmentBox;
        private LectroLend.Desktop.Views.UI.RoundButton saveButton;
        private System.Windows.Forms.Panel spacePanel;
        private System.Windows.Forms.GroupBox passwordGroup;
        private System.Windows.Forms.Label currentPasswordLabel;
        private System.Windows.Forms.TextBox currentPasswordBox;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.TextBox newPasswordBox;
        private System.Windows.Forms.Label repeatPasswordLabel;
        private System.Windows.Forms.TextBox repeatPasswordBox;
        private LectroLend.Desktop.Views.UI.RoundButton changeButton;
    }
}
