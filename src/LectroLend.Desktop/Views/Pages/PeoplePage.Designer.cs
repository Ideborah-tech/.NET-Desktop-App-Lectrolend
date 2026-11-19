// This file is written by the Visual Studio designer.
// To change how the page looks, double-click PeoplePage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class PeoplePage
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
            this.activateButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.deactivateButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.titleLabel.Text = "People";
            //
            // buttonBar
            //
            this.buttonBar.Controls.Add(this.addButton);
            this.buttonBar.Controls.Add(this.activateButton);
            this.buttonBar.Controls.Add(this.deactivateButton);
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
            this.addButton.Text = "Add account";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            //
            // activateButton
            //
            this.activateButton.BackColor = System.Drawing.Color.FromArgb(22, 128, 61);
            this.activateButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.activateButton.ForeColor = System.Drawing.Color.White;
            this.activateButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(140, 38);
            this.activateButton.Text = "Activate";
            this.activateButton.Click += new System.EventHandler(this.ActivateButton_Click);
            //
            // deactivateButton
            //
            this.deactivateButton.BackColor = System.Drawing.Color.FromArgb(200, 40, 40);
            this.deactivateButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.deactivateButton.ForeColor = System.Drawing.Color.White;
            this.deactivateButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.deactivateButton.Name = "deactivateButton";
            this.deactivateButton.Size = new System.Drawing.Size(140, 38);
            this.deactivateButton.Text = "Deactivate";
            this.deactivateButton.Click += new System.EventHandler(this.DeactivateButton_Click);
            //
            // grid  (the table; its colours are set by Ui.StyleGrid in PeoplePage.cs)
            //
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.nameColumn, this.emailColumn, this.roleColumn, this.phoneColumn, this.accountColumn });
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1000, 534);
            //
            // the columns
            //
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "Name";
            this.emailColumn.HeaderText = "Email";
            this.emailColumn.Name = "Email";
            this.roleColumn.HeaderText = "Role";
            this.roleColumn.Name = "Role";
            this.phoneColumn.HeaderText = "Phone";
            this.phoneColumn.Name = "Phone";
            this.accountColumn.HeaderText = "Account";
            this.accountColumn.Name = "Account";
            //
            // PeoplePage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonBar);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "PeoplePage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.PeoplePage_Load);
            this.buttonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonBar;
        private LectroLend.Desktop.Views.UI.RoundButton addButton;
        private LectroLend.Desktop.Views.UI.RoundButton activateButton;
        private LectroLend.Desktop.Views.UI.RoundButton deactivateButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountColumn;
    }
}
