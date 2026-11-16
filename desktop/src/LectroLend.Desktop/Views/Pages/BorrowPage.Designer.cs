// This file is written by the Visual Studio designer.
// To change how the page looks, double-click BorrowPage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class BorrowPage
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
            this.searchBar = new System.Windows.Forms.FlowLayoutPanel();
            this.searchBox = new LectroLend.Desktop.Views.UI.InputBox();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.cardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchBar.SuspendLayout();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1000, 50);
            this.titleLabel.Text = "What do you need today?";
            //
            // searchBar  (the search box and the type filter, side by side)
            //
            this.searchBar.Controls.Add(this.searchBox);
            this.searchBar.Controls.Add(this.typeBox);
            this.searchBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBar.Name = "searchBar";
            this.searchBar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.searchBar.Size = new System.Drawing.Size(1000, 56);
            //
            // searchBox
            //
            this.searchBox.Hint = "Search by name or asset tag";
            this.searchBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(320, 44);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.Filter_Changed);
            //
            // typeBox
            //
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.typeBox.Items.AddRange(new object[] { "All types", "Projector", "HDMI cable", "Remote", "Other" });
            this.typeBox.Margin = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(170, 28);
            this.typeBox.TabIndex = 1;
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            //
            // infoLabel  (for example "12 items · You have 1 of 3 active bookings")
            //
            this.infoLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(1000, 34);
            //
            // cardsPanel  (the equipment cards are added here in BorrowPage.cs while the app runs)
            //
            this.cardsPanel.AutoScroll = true;
            this.cardsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Size = new System.Drawing.Size(1000, 500);
            //
            // BorrowPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.cardsPanel);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "BorrowPage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.BorrowPage_Load);
            this.searchBar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.FlowLayoutPanel searchBar;
        private LectroLend.Desktop.Views.UI.InputBox searchBox;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.FlowLayoutPanel cardsPanel;
    }
}
