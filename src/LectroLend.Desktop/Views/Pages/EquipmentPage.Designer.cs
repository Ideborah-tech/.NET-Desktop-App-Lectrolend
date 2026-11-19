// This file is written by the Visual Studio designer.
// To change how the page looks, double-click EquipmentPage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class EquipmentPage
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
            this.removeButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.titleLabel.Text = "Equipment";
            //
            // buttonBar
            //
            this.buttonBar.Controls.Add(this.addButton);
            this.buttonBar.Controls.Add(this.editButton);
            this.buttonBar.Controls.Add(this.removeButton);
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
            this.addButton.Text = "Add item";
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
            // removeButton
            //
            this.removeButton.BackColor = System.Drawing.Color.FromArgb(200, 40, 40);
            this.removeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.removeButton.ForeColor = System.Drawing.Color.White;
            this.removeButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(140, 38);
            this.removeButton.Text = "Remove";
            this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
            //
            // grid  (the table; its colours are set by Ui.StyleGrid in EquipmentPage.cs)
            //
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.nameColumn, this.tagColumn, this.typeColumn, this.statusColumn, this.descriptionColumn });
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1000, 534);
            //
            // the columns
            //
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "Name";
            this.tagColumn.HeaderText = "Asset tag";
            this.tagColumn.Name = "Tag";
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Name = "Type";
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "Status";
            this.descriptionColumn.HeaderText = "Description";
            this.descriptionColumn.Name = "Description";
            //
            // EquipmentPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonBar);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "EquipmentPage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.EquipmentPage_Load);
            this.buttonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonBar;
        private LectroLend.Desktop.Views.UI.RoundButton addButton;
        private LectroLend.Desktop.Views.UI.RoundButton editButton;
        private LectroLend.Desktop.Views.UI.RoundButton removeButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
    }
}
