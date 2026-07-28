namespace IndoTaste.Forms.Customer
{
    partial class FormCustomerOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}

            // 清理所有載入的 Image 資源
            if (disposing)
            {
                foreach (var image in _buttonImages.Values)
                {
                    image?.Dispose();
                }
                _buttonImages.Clear();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProductArea = new System.Windows.Forms.Panel();
            this.flpCategory = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCart = new System.Windows.Forms.Panel();
            this.tblMain.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.pnlHeader, 0, 0);
            this.tblMain.Controls.Add(this.tblContent, 0, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(1465, 741);
            this.tblMain.TabIndex = 0;
            this.tblMain.Paint += new System.Windows.Forms.PaintEventHandler(this.tblMain_Paint);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1465, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 3;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblContent.Controls.Add(this.pnlProductArea, 1, 0);
            this.tblContent.Controls.Add(this.flpCategory, 0, 0);
            this.tblContent.Controls.Add(this.pnlCart, 2, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 100);
            this.tblContent.Margin = new System.Windows.Forms.Padding(0);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(1465, 641);
            this.tblContent.TabIndex = 1;
            // 
            // pnlProductArea
            // 
            this.pnlProductArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductArea.Location = new System.Drawing.Point(230, 0);
            this.pnlProductArea.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlProductArea.Name = "pnlProductArea";
            this.pnlProductArea.Size = new System.Drawing.Size(852, 641);
            this.pnlProductArea.TabIndex = 1;
            // 
            // flpCategory
            // 
            this.flpCategory.BackColor = System.Drawing.Color.White;
            this.flpCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCategory.Location = new System.Drawing.Point(0, 0);
            this.flpCategory.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.flpCategory.Name = "flpCategory";
            this.flpCategory.Size = new System.Drawing.Size(230, 629);
            this.flpCategory.TabIndex = 3;
            // 
            // pnlCart
            // 
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCart.Location = new System.Drawing.Point(1097, 3);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Size = new System.Drawing.Size(365, 635);
            this.pnlCart.TabIndex = 4;
            // 
            // FormCustomerOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1465, 741);
            this.Controls.Add(this.tblMain);
            this.Name = "FormCustomerOrder";
            this.Text = "FormCustomerOrder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tblMain.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.Panel pnlProductArea;
        private System.Windows.Forms.FlowLayoutPanel flpCategory;
        private System.Windows.Forms.Panel pnlCart;
    }
}