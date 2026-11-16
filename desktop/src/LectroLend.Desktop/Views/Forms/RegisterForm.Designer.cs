// This file is written by the Visual Studio designer.
// To change how the window looks, double-click RegisterForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class RegisterForm
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
            this.roleLabel = new System.Windows.Forms.Label();
            this.roleBox = new System.Windows.Forms.ComboBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.numberLabel = new System.Windows.Forms.Label();
            this.numberBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.facultyLabel = new System.Windows.Forms.Label();
            this.facultyBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.programLabel = new System.Windows.Forms.Label();
            this.programBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.departmentBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.saveButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Create your account";
            //
            // roleLabel
            //
            this.roleLabel.AutoSize = true;
            this.roleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.roleLabel.Location = new System.Drawing.Point(24, 86);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Text = "I am a";
            //
            // roleBox  (the list of roles is filled in RegisterForm.cs)
            //
            this.roleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleBox.Location = new System.Drawing.Point(210, 82);
            this.roleBox.Name = "roleBox";
            this.roleBox.Size = new System.Drawing.Size(346, 25);
            this.roleBox.TabIndex = 0;
            //
            // nameLabel
            //
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.nameLabel.Location = new System.Drawing.Point(24, 136);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Text = "Full name";
            //
            // nameBox
            //
            this.nameBox.Hint = "e.g. Ingabire Deborah";
            this.nameBox.Location = new System.Drawing.Point(210, 124);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(346, 44);
            this.nameBox.TabIndex = 1;
            //
            // emailLabel
            //
            this.emailLabel.AutoSize = true;
            this.emailLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.emailLabel.Location = new System.Drawing.Point(24, 190);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Text = "Email";
            //
            // emailBox
            //
            this.emailBox.Hint = "you@auca.ac.rw";
            this.emailBox.Location = new System.Drawing.Point(210, 178);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(346, 44);
            this.emailBox.TabIndex = 2;
            //
            // phoneLabel
            //
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.phoneLabel.Location = new System.Drawing.Point(24, 244);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Text = "Phone (optional)";
            //
            // phoneBox
            //
            this.phoneBox.Hint = "+250...";
            this.phoneBox.Location = new System.Drawing.Point(210, 232);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(346, 44);
            this.phoneBox.TabIndex = 3;
            //
            // passwordLabel
            //
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.passwordLabel.Location = new System.Drawing.Point(24, 298);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Text = "Password";
            //
            // passwordBox
            //
            this.passwordBox.Hint = "At least 8 characters";
            this.passwordBox.IsPassword = true;
            this.passwordBox.Location = new System.Drawing.Point(210, 286);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(346, 44);
            this.passwordBox.TabIndex = 4;
            //
            // numberLabel
            //
            this.numberLabel.AutoSize = true;
            this.numberLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.numberLabel.Location = new System.Drawing.Point(24, 352);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Text = "Reg. / staff number";
            //
            // numberBox
            //
            this.numberBox.Hint = "Registration or staff number";
            this.numberBox.Location = new System.Drawing.Point(210, 340);
            this.numberBox.Name = "numberBox";
            this.numberBox.Size = new System.Drawing.Size(346, 44);
            this.numberBox.TabIndex = 5;
            //
            // facultyLabel
            //
            this.facultyLabel.AutoSize = true;
            this.facultyLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.facultyLabel.Location = new System.Drawing.Point(24, 406);
            this.facultyLabel.Name = "facultyLabel";
            this.facultyLabel.Text = "Faculty (students)";
            //
            // facultyBox
            //
            this.facultyBox.Location = new System.Drawing.Point(210, 394);
            this.facultyBox.Name = "facultyBox";
            this.facultyBox.Size = new System.Drawing.Size(346, 44);
            this.facultyBox.TabIndex = 6;
            //
            // programLabel
            //
            this.programLabel.AutoSize = true;
            this.programLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.programLabel.Location = new System.Drawing.Point(24, 460);
            this.programLabel.Name = "programLabel";
            this.programLabel.Text = "Program (students)";
            //
            // programBox
            //
            this.programBox.Location = new System.Drawing.Point(210, 448);
            this.programBox.Name = "programBox";
            this.programBox.Size = new System.Drawing.Size(346, 44);
            this.programBox.TabIndex = 7;
            //
            // departmentLabel
            //
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.departmentLabel.Location = new System.Drawing.Point(24, 514);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Text = "Department (lecturers)";
            //
            // departmentBox
            //
            this.departmentBox.Location = new System.Drawing.Point(210, 502);
            this.departmentBox.Name = "departmentBox";
            this.departmentBox.Size = new System.Drawing.Size(346, 44);
            this.departmentBox.TabIndex = 8;
            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(24, 580);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(532, 46);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Create account";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            //
            // RegisterForm
            //
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 650);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.roleLabel);
            this.Controls.Add(this.roleBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.phoneBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.numberBox);
            this.Controls.Add(this.facultyLabel);
            this.Controls.Add(this.facultyBox);
            this.Controls.Add(this.programLabel);
            this.Controls.Add(this.programBox);
            this.Controls.Add(this.departmentLabel);
            this.Controls.Add(this.departmentBox);
            this.Controls.Add(this.saveButton);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create your account";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.ComboBox roleBox;
        private System.Windows.Forms.Label nameLabel;
        private LectroLend.Desktop.Views.UI.InputBox nameBox;
        private System.Windows.Forms.Label emailLabel;
        private LectroLend.Desktop.Views.UI.InputBox emailBox;
        private System.Windows.Forms.Label phoneLabel;
        private LectroLend.Desktop.Views.UI.InputBox phoneBox;
        private System.Windows.Forms.Label passwordLabel;
        private LectroLend.Desktop.Views.UI.InputBox passwordBox;
        private System.Windows.Forms.Label numberLabel;
        private LectroLend.Desktop.Views.UI.InputBox numberBox;
        private System.Windows.Forms.Label facultyLabel;
        private LectroLend.Desktop.Views.UI.InputBox facultyBox;
        private System.Windows.Forms.Label programLabel;
        private LectroLend.Desktop.Views.UI.InputBox programBox;
        private System.Windows.Forms.Label departmentLabel;
        private LectroLend.Desktop.Views.UI.InputBox departmentBox;
        private LectroLend.Desktop.Views.UI.RoundButton saveButton;
    }
}
