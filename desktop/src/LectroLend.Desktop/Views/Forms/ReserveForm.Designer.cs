// This file is written by the Visual Studio designer.
// To change how the window looks, double-click ReserveForm.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class ReserveForm
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
            this.itemLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.endLabel = new System.Windows.Forms.Label();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.roomLabel = new System.Windows.Forms.Label();
            this.roomBox = new System.Windows.Forms.ComboBox();
            this.bookedLabel = new System.Windows.Forms.Label();
            this.bookedList = new System.Windows.Forms.ListBox();
            this.freeLabel = new System.Windows.Forms.Label();
            this.rulesLabel = new System.Windows.Forms.Label();
            this.reserveButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.SuspendLayout();
            //
            // titleLabel  (the item's name, set in ReserveForm.cs)
            //
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Location = new System.Drawing.Point(24, 18);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(532, 42);
            this.titleLabel.Text = "Item name";
            //
            // itemLabel  (asset tag, type and description)
            //
            this.itemLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.itemLabel.Location = new System.Drawing.Point(24, 64);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(532, 46);
            this.itemLabel.Text = "Asset tag · Type";
            //
            // startLabel
            //
            this.startLabel.AutoSize = true;
            this.startLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.startLabel.Location = new System.Drawing.Point(24, 125);
            this.startLabel.Name = "startLabel";
            this.startLabel.Text = "From";
            //
            // startPicker
            //
            this.startPicker.CustomFormat = "ddd d MMM yyyy   HH:mm";
            this.startPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startPicker.Location = new System.Drawing.Point(150, 120);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(406, 25);
            this.startPicker.TabIndex = 0;
            this.startPicker.ValueChanged += new System.EventHandler(this.Time_Changed);
            //
            // endLabel
            //
            this.endLabel.AutoSize = true;
            this.endLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.endLabel.Location = new System.Drawing.Point(24, 167);
            this.endLabel.Name = "endLabel";
            this.endLabel.Text = "Until";
            //
            // endPicker
            //
            this.endPicker.CustomFormat = "ddd d MMM yyyy   HH:mm";
            this.endPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endPicker.Location = new System.Drawing.Point(150, 162);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(406, 25);
            this.endPicker.TabIndex = 1;
            this.endPicker.ValueChanged += new System.EventHandler(this.Time_Changed);
            //
            // roomLabel
            //
            this.roomLabel.AutoSize = true;
            this.roomLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.roomLabel.Location = new System.Drawing.Point(24, 209);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Text = "Room";
            //
            // roomBox  (the rooms are added in ReserveForm.cs)
            //
            this.roomBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomBox.Location = new System.Drawing.Point(150, 204);
            this.roomBox.Name = "roomBox";
            this.roomBox.Size = new System.Drawing.Size(406, 25);
            this.roomBox.TabIndex = 2;
            //
            // bookedLabel
            //
            this.bookedLabel.AutoSize = true;
            this.bookedLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.bookedLabel.Location = new System.Drawing.Point(24, 251);
            this.bookedLabel.Name = "bookedLabel";
            this.bookedLabel.Text = "Already booked";
            //
            // bookedList
            //
            this.bookedList.IntegralHeight = false;
            this.bookedList.Location = new System.Drawing.Point(150, 246);
            this.bookedList.Name = "bookedList";
            this.bookedList.Size = new System.Drawing.Size(406, 90);
            this.bookedList.TabStop = false;
            //
            // freeLabel  ("Free for that time" in green, or the reason in red)
            //
            this.freeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.freeLabel.Location = new System.Drawing.Point(24, 350);
            this.freeLabel.Name = "freeLabel";
            this.freeLabel.Size = new System.Drawing.Size(532, 26);
            //
            // rulesLabel
            //
            this.rulesLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.rulesLabel.Location = new System.Drawing.Point(24, 382);
            this.rulesLabel.Name = "rulesLabel";
            this.rulesLabel.Size = new System.Drawing.Size(532, 62);
            this.rulesLabel.Text = "Rules: pick up from the IT office up to 30 minutes before the start. If you do not collect it within 30 minutes after the start, the booking expires. A booking can last up to 7 days.";
            //
            // reserveButton
            //
            this.reserveButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.reserveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.reserveButton.ForeColor = System.Drawing.Color.White;
            this.reserveButton.Location = new System.Drawing.Point(24, 460);
            this.reserveButton.Name = "reserveButton";
            this.reserveButton.Size = new System.Drawing.Size(532, 46);
            this.reserveButton.TabIndex = 3;
            this.reserveButton.Text = "Reserve";
            this.reserveButton.Click += new System.EventHandler(this.ReserveButton_Click);
            //
            // ReserveForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 530);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.itemLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.startPicker);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.endPicker);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.roomBox);
            this.Controls.Add(this.bookedLabel);
            this.Controls.Add(this.bookedList);
            this.Controls.Add(this.freeLabel);
            this.Controls.Add(this.rulesLabel);
            this.Controls.Add(this.reserveButton);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReserveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reserve";
            this.Load += new System.EventHandler(this.Time_Changed);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.DateTimePicker startPicker;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.DateTimePicker endPicker;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.ComboBox roomBox;
        private System.Windows.Forms.Label bookedLabel;
        private System.Windows.Forms.ListBox bookedList;
        private System.Windows.Forms.Label freeLabel;
        private System.Windows.Forms.Label rulesLabel;
        private LectroLend.Desktop.Views.UI.RoundButton reserveButton;
    }
}
