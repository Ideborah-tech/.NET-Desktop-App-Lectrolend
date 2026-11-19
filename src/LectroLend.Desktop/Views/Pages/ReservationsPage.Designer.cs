// This file is written by the Visual Studio designer.
// To change how the page looks, double-click ReservationsPage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class ReservationsPage
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
            this.buttonBar = new System.Windows.Forms.FlowLayoutPanel();
            this.statusBox = new System.Windows.Forms.ComboBox();
            this.handOverButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.checkInButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.cancelButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.exportButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.borrowerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.untilColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1000, 50);
            this.titleLabel.Text = "Reservations";
            //
            // buttonBar
            //
            this.buttonBar.Controls.Add(this.statusBox);
            this.buttonBar.Controls.Add(this.handOverButton);
            this.buttonBar.Controls.Add(this.checkInButton);
            this.buttonBar.Controls.Add(this.cancelButton);
            this.buttonBar.Controls.Add(this.exportButton);
            this.buttonBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonBar.Name = "buttonBar";
            this.buttonBar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.buttonBar.Size = new System.Drawing.Size(1000, 56);
            //
            // statusBox  (filter by status; the statuses are added in ReservationsPage.cs)
            //
            this.statusBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusBox.Margin = new System.Windows.Forms.Padding(0, 6, 10, 0);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(150, 25);
            this.statusBox.SelectedIndexChanged += new System.EventHandler(this.StatusBox_SelectedIndexChanged);
            //
            // handOverButton
            //
            this.handOverButton.BackColor = System.Drawing.Color.FromArgb(44, 116, 179);
            this.handOverButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.handOverButton.ForeColor = System.Drawing.Color.White;
            this.handOverButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.handOverButton.Name = "handOverButton";
            this.handOverButton.Size = new System.Drawing.Size(140, 38);
            this.handOverButton.Text = "Hand over";
            this.handOverButton.Click += new System.EventHandler(this.HandOverButton_Click);
            //
            // checkInButton
            //
            this.checkInButton.BackColor = System.Drawing.Color.FromArgb(255, 201, 60);
            this.checkInButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.checkInButton.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.checkInButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.checkInButton.Name = "checkInButton";
            this.checkInButton.Size = new System.Drawing.Size(140, 38);
            this.checkInButton.Text = "Check in";
            this.checkInButton.Click += new System.EventHandler(this.CheckInButton_Click);
            //
            // cancelButton
            //
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(200, 40, 40);
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(140, 38);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            //
            // exportButton
            //
            this.exportButton.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.exportButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.exportButton.ForeColor = System.Drawing.Color.White;
            this.exportButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(140, 38);
            this.exportButton.Text = "Export CSV";
            this.exportButton.Click += new System.EventHandler(this.ExportButton_Click);
            //
            // grid  (the table; its colours are set by Ui.StyleGrid in ReservationsPage.cs)
            //
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.idColumn, this.borrowerColumn, this.itemColumn, this.fromColumn,
                this.untilColumn, this.roomColumn, this.statusColumn });
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1000, 534);
            //
            // the columns ("#" stays narrow)
            //
            this.idColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.idColumn.HeaderText = "#";
            this.idColumn.Name = "Id";
            this.idColumn.Width = 55;
            this.borrowerColumn.HeaderText = "Borrower";
            this.borrowerColumn.Name = "Borrower";
            this.itemColumn.HeaderText = "Item";
            this.itemColumn.Name = "Item";
            this.fromColumn.HeaderText = "From";
            this.fromColumn.Name = "From";
            this.untilColumn.HeaderText = "Until";
            this.untilColumn.Name = "Until";
            this.roomColumn.HeaderText = "Room";
            this.roomColumn.Name = "Room";
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "Status";
            //
            // ReservationsPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonBar);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ReservationsPage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.buttonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonBar;
        private System.Windows.Forms.ComboBox statusBox;
        private LectroLend.Desktop.Views.UI.RoundButton handOverButton;
        private LectroLend.Desktop.Views.UI.RoundButton checkInButton;
        private LectroLend.Desktop.Views.UI.RoundButton cancelButton;
        private LectroLend.Desktop.Views.UI.RoundButton exportButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn borrowerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn untilColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
    }
}
