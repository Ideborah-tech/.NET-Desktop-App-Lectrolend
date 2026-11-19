// This file is written by the Visual Studio designer.
// To change how the page looks, double-click DashboardPage.cs and use Design view.

namespace LectroLend.Desktop.Views
{
    partial class DashboardPage
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
            this.numbersTable = new System.Windows.Forms.TableLayoutPanel();
            this.outNowBox = new System.Windows.Forms.Panel();
            this.outNowCaption = new System.Windows.Forms.Label();
            this.outNowNumber = new System.Windows.Forms.Label();
            this.toCollectBox = new System.Windows.Forms.Panel();
            this.toCollectCaption = new System.Windows.Forms.Label();
            this.toCollectNumber = new System.Windows.Forms.Label();
            this.lateBox = new System.Windows.Forms.Panel();
            this.lateCaption = new System.Windows.Forms.Label();
            this.lateNumber = new System.Windows.Forms.Label();
            this.onShelfBox = new System.Windows.Forms.Panel();
            this.onShelfCaption = new System.Windows.Forms.Label();
            this.onShelfNumber = new System.Windows.Forms.Label();
            this.listsTable = new System.Windows.Forms.TableLayoutPanel();
            this.toCollectPanel = new System.Windows.Forms.Panel();
            this.toCollectGrid = new System.Windows.Forms.DataGridView();
            this.collectBorrowerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collectItemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collectStartColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toCollectTop = new System.Windows.Forms.Panel();
            this.toCollectHeading = new System.Windows.Forms.Label();
            this.handOverButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.outNowPanel = new System.Windows.Forms.Panel();
            this.outNowGrid = new System.Windows.Forms.DataGridView();
            this.outBorrowerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outItemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outDueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outNowTop = new System.Windows.Forms.Panel();
            this.outNowHeading = new System.Windows.Forms.Label();
            this.checkInButton = new LectroLend.Desktop.Views.UI.RoundButton();
            this.numbersTable.SuspendLayout();
            this.outNowBox.SuspendLayout();
            this.toCollectBox.SuspendLayout();
            this.lateBox.SuspendLayout();
            this.onShelfBox.SuspendLayout();
            this.listsTable.SuspendLayout();
            this.toCollectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toCollectGrid)).BeginInit();
            this.toCollectTop.SuspendLayout();
            this.outNowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outNowGrid)).BeginInit();
            this.outNowTop.SuspendLayout();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1000, 50);
            this.titleLabel.Text = "Today at the IT office";
            //
            // numbersTable  (four boxes side by side, each 25% wide)
            //
            this.numbersTable.ColumnCount = 4;
            this.numbersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.numbersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.numbersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.numbersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.numbersTable.Controls.Add(this.outNowBox, 0, 0);
            this.numbersTable.Controls.Add(this.toCollectBox, 1, 0);
            this.numbersTable.Controls.Add(this.lateBox, 2, 0);
            this.numbersTable.Controls.Add(this.onShelfBox, 3, 0);
            this.numbersTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.numbersTable.Name = "numbersTable";
            this.numbersTable.RowCount = 1;
            this.numbersTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.numbersTable.Size = new System.Drawing.Size(1000, 100);
            //
            // outNowBox  (yellow)
            //
            this.outNowBox.BackColor = System.Drawing.Color.FromArgb(255, 201, 60);
            this.outNowBox.Controls.Add(this.outNowCaption);
            this.outNowBox.Controls.Add(this.outNowNumber);
            this.outNowBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outNowBox.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.outNowBox.Name = "outNowBox";
            this.outNowCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.outNowCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.outNowCaption.Name = "outNowCaption";
            this.outNowCaption.Text = "Out now";
            this.outNowCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.outNowNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.outNowNumber.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.outNowNumber.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.outNowNumber.Name = "outNowNumber";
            this.outNowNumber.Size = new System.Drawing.Size(200, 55);
            this.outNowNumber.Text = "0";
            this.outNowNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // toCollectBox
            //
            this.toCollectBox.BackColor = System.Drawing.Color.White;
            this.toCollectBox.Controls.Add(this.toCollectCaption);
            this.toCollectBox.Controls.Add(this.toCollectNumber);
            this.toCollectBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toCollectBox.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.toCollectBox.Name = "toCollectBox";
            this.toCollectCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.toCollectCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.toCollectCaption.Name = "toCollectCaption";
            this.toCollectCaption.Text = "To collect";
            this.toCollectCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toCollectNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.toCollectNumber.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.toCollectNumber.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.toCollectNumber.Name = "toCollectNumber";
            this.toCollectNumber.Size = new System.Drawing.Size(200, 55);
            this.toCollectNumber.Text = "0";
            this.toCollectNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lateBox
            //
            this.lateBox.BackColor = System.Drawing.Color.White;
            this.lateBox.Controls.Add(this.lateCaption);
            this.lateBox.Controls.Add(this.lateNumber);
            this.lateBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lateBox.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lateBox.Name = "lateBox";
            this.lateCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lateCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lateCaption.Name = "lateCaption";
            this.lateCaption.Text = "Overdue";
            this.lateCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lateNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.lateNumber.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lateNumber.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.lateNumber.Name = "lateNumber";
            this.lateNumber.Size = new System.Drawing.Size(200, 55);
            this.lateNumber.Text = "0";
            this.lateNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // onShelfBox
            //
            this.onShelfBox.BackColor = System.Drawing.Color.White;
            this.onShelfBox.Controls.Add(this.onShelfCaption);
            this.onShelfBox.Controls.Add(this.onShelfNumber);
            this.onShelfBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onShelfBox.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.onShelfBox.Name = "onShelfBox";
            this.onShelfCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.onShelfCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.onShelfCaption.Name = "onShelfCaption";
            this.onShelfCaption.Text = "Items on the shelf";
            this.onShelfCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.onShelfNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.onShelfNumber.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.onShelfNumber.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.onShelfNumber.Name = "onShelfNumber";
            this.onShelfNumber.Size = new System.Drawing.Size(200, 55);
            this.onShelfNumber.Text = "0 / 0";
            this.onShelfNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // listsTable  (the two lists side by side, each 50% wide)
            //
            this.listsTable.ColumnCount = 2;
            this.listsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.listsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.listsTable.Controls.Add(this.toCollectPanel, 0, 0);
            this.listsTable.Controls.Add(this.outNowPanel, 1, 0);
            this.listsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listsTable.Name = "listsTable";
            this.listsTable.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.listsTable.RowCount = 1;
            this.listsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.listsTable.Size = new System.Drawing.Size(1000, 490);
            //
            // toCollectPanel  (white box: heading, "Hand over" button and the list)
            //
            this.toCollectPanel.BackColor = System.Drawing.Color.White;
            this.toCollectPanel.Controls.Add(this.toCollectGrid);
            this.toCollectPanel.Controls.Add(this.toCollectTop);
            this.toCollectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toCollectPanel.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.toCollectPanel.Name = "toCollectPanel";
            this.toCollectPanel.Padding = new System.Windows.Forms.Padding(12);
            this.toCollectTop.Controls.Add(this.toCollectHeading);
            this.toCollectTop.Controls.Add(this.handOverButton);
            this.toCollectTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.toCollectTop.Name = "toCollectTop";
            this.toCollectTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.toCollectTop.Size = new System.Drawing.Size(476, 50);
            this.toCollectHeading.AutoSize = true;
            this.toCollectHeading.Dock = System.Windows.Forms.DockStyle.Left;
            this.toCollectHeading.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.toCollectHeading.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.toCollectHeading.Name = "toCollectHeading";
            this.toCollectHeading.Text = "To collect";
            this.handOverButton.BackColor = System.Drawing.Color.FromArgb(44, 116, 179);
            this.handOverButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.handOverButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.handOverButton.ForeColor = System.Drawing.Color.White;
            this.handOverButton.Name = "handOverButton";
            this.handOverButton.Size = new System.Drawing.Size(140, 42);
            this.handOverButton.Text = "Hand over";
            this.handOverButton.Click += new System.EventHandler(this.HandOverButton_Click);
            this.toCollectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.collectBorrowerColumn, this.collectItemColumn, this.collectStartColumn });
            this.toCollectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toCollectGrid.Name = "toCollectGrid";
            this.collectBorrowerColumn.HeaderText = "Borrower";
            this.collectBorrowerColumn.Name = "Borrower";
            this.collectItemColumn.HeaderText = "Item";
            this.collectItemColumn.Name = "Item";
            this.collectStartColumn.HeaderText = "Starts";
            this.collectStartColumn.Name = "Start";
            //
            // outNowPanel  (white box: heading, "Check in" button and the list)
            //
            this.outNowPanel.BackColor = System.Drawing.Color.White;
            this.outNowPanel.Controls.Add(this.outNowGrid);
            this.outNowPanel.Controls.Add(this.outNowTop);
            this.outNowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outNowPanel.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.outNowPanel.Name = "outNowPanel";
            this.outNowPanel.Padding = new System.Windows.Forms.Padding(12);
            this.outNowTop.Controls.Add(this.outNowHeading);
            this.outNowTop.Controls.Add(this.checkInButton);
            this.outNowTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.outNowTop.Name = "outNowTop";
            this.outNowTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.outNowTop.Size = new System.Drawing.Size(476, 50);
            this.outNowHeading.AutoSize = true;
            this.outNowHeading.Dock = System.Windows.Forms.DockStyle.Left;
            this.outNowHeading.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.outNowHeading.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.outNowHeading.Name = "outNowHeading";
            this.outNowHeading.Text = "Out now";
            this.checkInButton.BackColor = System.Drawing.Color.FromArgb(255, 201, 60);
            this.checkInButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkInButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.checkInButton.ForeColor = System.Drawing.Color.FromArgb(3, 52, 110);
            this.checkInButton.Name = "checkInButton";
            this.checkInButton.Size = new System.Drawing.Size(140, 42);
            this.checkInButton.Text = "Check in";
            this.checkInButton.Click += new System.EventHandler(this.CheckInButton_Click);
            this.outNowGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.outBorrowerColumn, this.outItemColumn, this.outDueColumn });
            this.outNowGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outNowGrid.Name = "outNowGrid";
            this.outBorrowerColumn.HeaderText = "Borrower";
            this.outBorrowerColumn.Name = "Borrower";
            this.outItemColumn.HeaderText = "Item";
            this.outItemColumn.Name = "Item";
            this.outDueColumn.HeaderText = "Due back";
            this.outDueColumn.Name = "Due";
            //
            // DashboardPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 249);
            this.Controls.Add(this.listsTable);
            this.Controls.Add(this.numbersTable);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "DashboardPage";
            this.Size = new System.Drawing.Size(1000, 640);
            this.Load += new System.EventHandler(this.DashboardPage_Load);
            this.numbersTable.ResumeLayout(false);
            this.outNowBox.ResumeLayout(false);
            this.toCollectBox.ResumeLayout(false);
            this.lateBox.ResumeLayout(false);
            this.onShelfBox.ResumeLayout(false);
            this.listsTable.ResumeLayout(false);
            this.toCollectPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toCollectGrid)).EndInit();
            this.toCollectTop.ResumeLayout(false);
            this.toCollectTop.PerformLayout();
            this.outNowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outNowGrid)).EndInit();
            this.outNowTop.ResumeLayout(false);
            this.outNowTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TableLayoutPanel numbersTable;
        private System.Windows.Forms.Panel outNowBox;
        private System.Windows.Forms.Label outNowCaption;
        private System.Windows.Forms.Label outNowNumber;
        private System.Windows.Forms.Panel toCollectBox;
        private System.Windows.Forms.Label toCollectCaption;
        private System.Windows.Forms.Label toCollectNumber;
        private System.Windows.Forms.Panel lateBox;
        private System.Windows.Forms.Label lateCaption;
        private System.Windows.Forms.Label lateNumber;
        private System.Windows.Forms.Panel onShelfBox;
        private System.Windows.Forms.Label onShelfCaption;
        private System.Windows.Forms.Label onShelfNumber;
        private System.Windows.Forms.TableLayoutPanel listsTable;
        private System.Windows.Forms.Panel toCollectPanel;
        private System.Windows.Forms.DataGridView toCollectGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn collectBorrowerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn collectItemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn collectStartColumn;
        private System.Windows.Forms.Panel toCollectTop;
        private System.Windows.Forms.Label toCollectHeading;
        private LectroLend.Desktop.Views.UI.RoundButton handOverButton;
        private System.Windows.Forms.Panel outNowPanel;
        private System.Windows.Forms.DataGridView outNowGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn outBorrowerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn outItemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn outDueColumn;
        private System.Windows.Forms.Panel outNowTop;
        private System.Windows.Forms.Label outNowHeading;
        private LectroLend.Desktop.Views.UI.RoundButton checkInButton;
    }
}
