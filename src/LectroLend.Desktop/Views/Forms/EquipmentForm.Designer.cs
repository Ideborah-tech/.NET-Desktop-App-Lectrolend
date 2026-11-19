// This file is written by the Visual Studio designer.
// To change how the window looks, double-click EquipmentForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class EquipmentForm
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.tagLabel = new System.Windows.Forms.Label();
            this.tagBox = new System.Windows.Forms.TextBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusBox = new System.Windows.Forms.ComboBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.saveButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.SuspendLayout();
            //
            // titleLabel  ("Add item" or "Edit item", set in EquipmentForm.cs)
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Add item";
            //
            // nameLabel
            //
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.nameLabel.Location = new System.Drawing.Point(24, 80);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Text = "Name";
            //
            // nameBox
            //
            this.nameBox.Location = new System.Drawing.Point(140, 76);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(316, 25);
            this.nameBox.TabIndex = 0;
            //
            // tagLabel
            //
            this.tagLabel.AutoSize = true;
            this.tagLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.tagLabel.Location = new System.Drawing.Point(24, 120);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Text = "Asset tag";
            //
            // tagBox
            //
            this.tagBox.Location = new System.Drawing.Point(140, 116);
            this.tagBox.Name = "tagBox";
            this.tagBox.Size = new System.Drawing.Size(316, 25);
            this.tagBox.TabIndex = 1;
            //
            // typeLabel
            //
            this.typeLabel.AutoSize = true;
            this.typeLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.typeLabel.Location = new System.Drawing.Point(24, 160);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Text = "Type";
            //
            // typeBox  (the types are added in EquipmentForm.cs)
            //
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.Location = new System.Drawing.Point(140, 156);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(316, 25);
            this.typeBox.TabIndex = 2;
            //
            // statusLabel
            //
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.statusLabel.Location = new System.Drawing.Point(24, 200);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Text = "Status";
            //
            // statusBox  (the statuses are added in EquipmentForm.cs)
            //
            this.statusBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusBox.Location = new System.Drawing.Point(140, 196);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(316, 25);
            this.statusBox.TabIndex = 3;
            //
            // descriptionLabel
            //
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.descriptionLabel.Location = new System.Drawing.Point(24, 240);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Text = "Description";
            //
            // descriptionBox
            //
            this.descriptionBox.Location = new System.Drawing.Point(140, 236);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(316, 64);
            this.descriptionBox.TabIndex = 4;
            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(24, 326);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(432, 44);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            //
            // EquipmentForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 394);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.tagLabel);
            this.Controls.Add(this.tagBox);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.saveButton);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EquipmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add item";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label tagLabel;
        private System.Windows.Forms.TextBox tagBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox statusBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox descriptionBox;
        private LectroLend.Desktop.Views.UI.RoundButton saveButton;
    }
}
