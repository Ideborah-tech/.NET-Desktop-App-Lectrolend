// This file is written by the Visual Studio designer.
// To change how the window looks, double-click RoomForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class RoomForm
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
            this.codeLabel = new System.Windows.Forms.Label();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.buildingLabel = new System.Windows.Forms.Label();
            this.buildingBox = new System.Windows.Forms.TextBox();
            this.capacityLabel = new System.Windows.Forms.Label();
            this.capacityBox = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new LectroLend.Desktop.Views.UI.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.capacityBox)).BeginInit();
            this.SuspendLayout();
            //
            // titleLabel  ("Add room" or "Edit room", set in RoomForm.cs)
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Add room";
            //
            // codeLabel
            //
            this.codeLabel.AutoSize = true;
            this.codeLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.codeLabel.Location = new System.Drawing.Point(24, 80);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Text = "Room code";
            //
            // codeBox
            //
            this.codeBox.Location = new System.Drawing.Point(140, 76);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(256, 25);
            this.codeBox.TabIndex = 0;
            //
            // buildingLabel
            //
            this.buildingLabel.AutoSize = true;
            this.buildingLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.buildingLabel.Location = new System.Drawing.Point(24, 120);
            this.buildingLabel.Name = "buildingLabel";
            this.buildingLabel.Text = "Building";
            //
            // buildingBox
            //
            this.buildingBox.Location = new System.Drawing.Point(140, 116);
            this.buildingBox.Name = "buildingBox";
            this.buildingBox.Size = new System.Drawing.Size(256, 25);
            this.buildingBox.TabIndex = 1;
            //
            // capacityLabel
            //
            this.capacityLabel.AutoSize = true;
            this.capacityLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.capacityLabel.Location = new System.Drawing.Point(24, 160);
            this.capacityLabel.Name = "capacityLabel";
            this.capacityLabel.Text = "Capacity";
            //
            // capacityBox
            //
            this.capacityBox.Location = new System.Drawing.Point(140, 156);
            this.capacityBox.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            this.capacityBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.capacityBox.Name = "capacityBox";
            this.capacityBox.Size = new System.Drawing.Size(120, 25);
            this.capacityBox.TabIndex = 2;
            this.capacityBox.Value = new decimal(new int[] { 30, 0, 0, 0 });
            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(24, 214);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(372, 44);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            //
            // RoomForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 282);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.buildingLabel);
            this.Controls.Add(this.buildingBox);
            this.Controls.Add(this.capacityLabel);
            this.Controls.Add(this.capacityBox);
            this.Controls.Add(this.saveButton);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add room";
            ((System.ComponentModel.ISupportInitialize)(this.capacityBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.Label buildingLabel;
        private System.Windows.Forms.TextBox buildingBox;
        private System.Windows.Forms.Label capacityLabel;
        private System.Windows.Forms.NumericUpDown capacityBox;
        private LectroLend.Desktop.Views.UI.RoundButton saveButton;
    }
}
