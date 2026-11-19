// This file is written by the Visual Studio designer.
// To change how the page looks, double-click MyReservationsPage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class MyReservationsPage
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
            this.cancelButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.refreshButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.itemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.titleLabel.Text = "My reservations";
            //
            // buttonBar
            //
            this.buttonBar.Controls.Add(this.cancelButton);
            this.buttonBar.Controls.Add(this.refreshButton);
            this.buttonBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonBar.Name = "buttonBar";
            this.buttonBar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.buttonBar.Size = new System.Drawing.Size(1000, 56);
            //
            // cancelButton
            //
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(200, 40, 40);
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(160, 38);
            this.cancelButton.Text = "Cancel booking";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            //
            // refreshButton
            //
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.refreshButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(140, 38);
            this.refreshButton.Text = "Refresh";
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            //
            // grid  (the table; its colours are set by Ui.StyleGrid in MyReservationsPage.cs)
            //
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.itemColumn, this.tagColumn, this.fromColumn, this.untilColumn, this.roomColumn, this.statusColumn });
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1000, 534);
            //
            // the columns
            //
            this.itemColumn.HeaderText = "Item";
            this.itemColumn.Name = "Item";
            this.tagColumn.HeaderText = "Asset tag";
            this.tagColumn.Name = "Tag";
            this.fromColumn.HeaderText = "From";
            this.fromColumn.Name = "From";
            this.untilColumn.HeaderText = "Until";
            this.untilColumn.Name = "Until";
            this.roomColumn.HeaderText = "Room";
            this.roomColumn.Name = "Room";
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "Status";
            //
            // MyReservationsPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonBar);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "MyReservationsPage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.RefreshButton_Click);
            this.buttonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonBar;
        private LectroLend.Desktop.Views.UI.RoundButton cancelButton;
        private LectroLend.Desktop.Views.UI.RoundButton refreshButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn untilColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
    }
}
