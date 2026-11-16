// This file is written by the Visual Studio designer.
// To change how the page looks, double-click RoomsPage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class RoomsPage
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
            this.addButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.editButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.deleteButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.codeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buildingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.titleLabel.Text = "Rooms";
            //
            // buttonBar
            //
            this.buttonBar.Controls.Add(this.addButton);
            this.buttonBar.Controls.Add(this.editButton);
            this.buttonBar.Controls.Add(this.deleteButton);
            this.buttonBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonBar.Name = "buttonBar";
            this.buttonBar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.buttonBar.Size = new System.Drawing.Size(1000, 56);
            //
            // addButton
            //
            this.addButton.BackColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.addButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(140, 38);
            this.addButton.Text = "Add room";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            //
            // editButton
            //
            this.editButton.BackColor = System.Drawing.Color.FromArgb(44, 116, 179);
            this.editButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.editButton.ForeColor = System.Drawing.Color.White;
            this.editButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(140, 38);
            this.editButton.Text = "Edit";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            //
            // deleteButton
            //
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(200, 40, 40);
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(140, 38);
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            //
            // grid  (the table; its colours are set by Ui.StyleGrid in RoomsPage.cs)
            //
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.codeColumn, this.buildingColumn, this.capacityColumn });
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1000, 534);
            //
            // the columns
            //
            this.codeColumn.HeaderText = "Room code";
            this.codeColumn.Name = "Code";
            this.buildingColumn.HeaderText = "Building";
            this.buildingColumn.Name = "Building";
            this.capacityColumn.HeaderText = "Capacity";
            this.capacityColumn.Name = "Capacity";
            //
            // RoomsPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonBar);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "RoomsPage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.RoomsPage_Load);
            this.buttonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonBar;
        private LectroLend.Desktop.Views.UI.RoundButton addButton;
        private LectroLend.Desktop.Views.UI.RoundButton editButton;
        private LectroLend.Desktop.Views.UI.RoundButton deleteButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn buildingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacityColumn;
    }
}
