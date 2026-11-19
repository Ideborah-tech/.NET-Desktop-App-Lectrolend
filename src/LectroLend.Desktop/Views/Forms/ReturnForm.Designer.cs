// This file is written by the Visual Studio designer.
// To change how the window looks, double-click ReturnForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class ReturnForm
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
            this.infoLabel = new System.Windows.Forms.Label();
            this.damagedLabel = new System.Windows.Forms.Label();
            this.damagedBox = new System.Windows.Forms.CheckBox();
            this.conditionLabel = new System.Windows.Forms.Label();
            this.conditionBox = new System.Windows.Forms.TextBox();
            this.remarksLabel = new System.Windows.Forms.Label();
            this.remarksBox = new System.Windows.Forms.TextBox();
            this.confirmButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Check in";
            //
            // infoLabel  (which item, who borrowed it, when it is due; set in ReturnForm.cs)
            //
            this.infoLabel.Location = new System.Drawing.Point(24, 66);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(432, 46);
            this.infoLabel.Text = "Item borrowed by ...";
            //
            // damagedLabel
            //
            this.damagedLabel.AutoSize = true;
            this.damagedLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.damagedLabel.Location = new System.Drawing.Point(24, 126);
            this.damagedLabel.Name = "damagedLabel";
            this.damagedLabel.Text = "Damaged?";
            //
            // damagedBox
            //
            this.damagedBox.AutoSize = true;
            this.damagedBox.Location = new System.Drawing.Point(150, 125);
            this.damagedBox.Name = "damagedBox";
            this.damagedBox.TabIndex = 0;
            this.damagedBox.Text = "The item is damaged";
            //
            // conditionLabel
            //
            this.conditionLabel.AutoSize = true;
            this.conditionLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.conditionLabel.Location = new System.Drawing.Point(24, 166);
            this.conditionLabel.Name = "conditionLabel";
            this.conditionLabel.Text = "Condition";
            //
            // conditionBox
            //
            this.conditionBox.Location = new System.Drawing.Point(150, 162);
            this.conditionBox.Name = "conditionBox";
            this.conditionBox.Size = new System.Drawing.Size(306, 25);
            this.conditionBox.TabIndex = 1;
            this.conditionBox.Text = "Good";
            //
            // remarksLabel
            //
            this.remarksLabel.AutoSize = true;
            this.remarksLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.remarksLabel.Location = new System.Drawing.Point(24, 206);
            this.remarksLabel.Name = "remarksLabel";
            this.remarksLabel.Text = "Remarks";
            //
            // remarksBox
            //
            this.remarksBox.Location = new System.Drawing.Point(150, 202);
            this.remarksBox.Multiline = true;
            this.remarksBox.Name = "remarksBox";
            this.remarksBox.Size = new System.Drawing.Size(306, 64);
            this.remarksBox.TabIndex = 2;
            //
            // confirmButton
            //
            this.confirmButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.confirmButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.confirmButton.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Location = new System.Drawing.Point(24, 290);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(432, 44);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "Confirm return";
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            //
            // ReturnForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.damagedLabel);
            this.Controls.Add(this.damagedBox);
            this.Controls.Add(this.conditionLabel);
            this.Controls.Add(this.conditionBox);
            this.Controls.Add(this.remarksLabel);
            this.Controls.Add(this.remarksBox);
            this.Controls.Add(this.confirmButton);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReturnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check in";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label damagedLabel;
        private System.Windows.Forms.CheckBox damagedBox;
        private System.Windows.Forms.Label conditionLabel;
        private System.Windows.Forms.TextBox conditionBox;
        private System.Windows.Forms.Label remarksLabel;
        private System.Windows.Forms.TextBox remarksBox;
        private LectroLend.Desktop.Views.UI.RoundButton confirmButton;
    }
}
