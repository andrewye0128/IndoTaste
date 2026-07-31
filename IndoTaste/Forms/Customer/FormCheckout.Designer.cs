namespace IndoTaste.Forms.Customer
{
    partial class FormCheckout
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnConfirmOrder = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlDetailTitle = new System.Windows.Forms.Panel();
            this.picReceiptIcon = new System.Windows.Forms.PictureBox();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.flpOrderItems = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.pnlCountRow = new System.Windows.Forms.Panel();
            this.pnlTotalRow = new System.Windows.Forms.Panel();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.pnlPayment = new System.Windows.Forms.Panel();
            this.picPayIcon = new System.Windows.Forms.PictureBox();
            this.lblPayTitle = new System.Windows.Forms.Label();
            this.flpPayment = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFooter.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlDetailTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReceiptIcon)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.pnlCountRow.SuspendLayout();
            this.pnlTotalRow.SuspendLayout();
            this.pnlPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(226)))));
            this.pnlFooter.Controls.Add(this.btnConfirmOrder);
            this.pnlFooter.Controls.Add(this.btnBack);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 670);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(560, 90);
            this.pnlFooter.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBack.Location = new System.Drawing.Point(20, 18);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(200, 54);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "返回修改";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnConfirmOrder
            // 
            this.btnConfirmOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmOrder.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfirmOrder.Location = new System.Drawing.Point(236, 18);
            this.btnConfirmOrder.Name = "btnConfirmOrder";
            this.btnConfirmOrder.Size = new System.Drawing.Size(300, 54);
            this.btnConfirmOrder.TabIndex = 1;
            this.btnConfirmOrder.Text = "立即結帳";
            this.btnConfirmOrder.UseVisualStyleBackColor = true;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlTitle, 0, 0);
            this.tlpMain.Controls.Add(this.pnlDetailTitle, 0, 1);
            this.tlpMain.Controls.Add(this.flpOrderItems, 0, 2);
            this.tlpMain.Controls.Add(this.pnlSummary, 0, 3);
            this.tlpMain.Controls.Add(this.pnlPayment, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.Size = new System.Drawing.Size(560, 670);
            this.tlpMain.TabIndex = 1;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(554, 54);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(210, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(114, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "訂單結帳";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.Location = new System.Drawing.Point(486, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(38, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlDetailTitle
            // 
            this.pnlDetailTitle.Controls.Add(this.lblDetailTitle);
            this.pnlDetailTitle.Controls.Add(this.picReceiptIcon);
            this.pnlDetailTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailTitle.Location = new System.Drawing.Point(3, 63);
            this.pnlDetailTitle.Name = "pnlDetailTitle";
            this.pnlDetailTitle.Size = new System.Drawing.Size(554, 34);
            this.pnlDetailTitle.TabIndex = 1;
            // 
            // picReceiptIcon
            // 
            this.picReceiptIcon.Location = new System.Drawing.Point(20, 6);
            this.picReceiptIcon.Name = "picReceiptIcon";
            this.picReceiptIcon.Size = new System.Drawing.Size(24, 24);
            this.picReceiptIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReceiptIcon.TabIndex = 0;
            this.picReceiptIcon.TabStop = false;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDetailTitle.Location = new System.Drawing.Point(52, 8);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(82, 23);
            this.lblDetailTitle.TabIndex = 1;
            this.lblDetailTitle.Text = "訂單明細";
            // 
            // flpOrderItems
            // 
            this.flpOrderItems.AutoScroll = true;
            this.flpOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOrderItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpOrderItems.Location = new System.Drawing.Point(16, 100);
            this.flpOrderItems.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.flpOrderItems.Name = "flpOrderItems";
            this.flpOrderItems.Size = new System.Drawing.Size(528, 340);
            this.flpOrderItems.TabIndex = 2;
            this.flpOrderItems.WrapContents = false;
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.pnlTotalRow);
            this.pnlSummary.Controls.Add(this.pnlCountRow);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummary.Location = new System.Drawing.Point(3, 443);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(554, 114);
            this.pnlSummary.TabIndex = 3;
            // 
            // pnlCountRow
            // 
            this.pnlCountRow.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.pnlCountRow.BackColor = System.Drawing.Color.White;
            this.pnlCountRow.Controls.Add(this.lblTotalQty);
            this.pnlCountRow.Controls.Add(this.lblItemCount);
            this.pnlCountRow.Location = new System.Drawing.Point(20, 2);
            this.pnlCountRow.Name = "pnlCountRow";
            this.pnlCountRow.Size = new System.Drawing.Size(500, 44);
            this.pnlCountRow.TabIndex = 0;
            // 
            // pnlTotalRow
            // 
            this.pnlTotalRow.BackColor = System.Drawing.Color.White;
            this.pnlTotalRow.Controls.Add(this.lblTotalAmount);
            this.pnlTotalRow.Controls.Add(this.lblTotalTitle);
            this.pnlTotalRow.Location = new System.Drawing.Point(20, 54);
            this.pnlTotalRow.Name = "pnlTotalRow";
            this.pnlTotalRow.Size = new System.Drawing.Size(500, 62);
            this.pnlTotalRow.TabIndex = 1;
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblItemCount.Location = new System.Drawing.Point(100, 12);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(96, 22);
            this.lblItemCount.TabIndex = 0;
            this.lblItemCount.Text = "共 0 項商品";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTotalQty.Location = new System.Drawing.Point(300, 12);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(96, 22);
            this.lblTotalQty.TabIndex = 1;
            this.lblTotalQty.Text = "總數量 0 件";
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTotalTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(112, 25);
            this.lblTotalTitle.TabIndex = 0;
            this.lblTotalTitle.Text = "訂單總金額";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(29)))), ((int)(((byte)(37)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(280, 14);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(200, 34);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "NT$ 0";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPayment
            // 
            this.pnlPayment.Controls.Add(this.flpPayment);
            this.pnlPayment.Controls.Add(this.lblPayTitle);
            this.pnlPayment.Controls.Add(this.picPayIcon);
            this.pnlPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPayment.Location = new System.Drawing.Point(3, 563);
            this.pnlPayment.Name = "pnlPayment";
            this.pnlPayment.Size = new System.Drawing.Size(554, 104);
            this.pnlPayment.TabIndex = 4;
            // 
            // picPayIcon
            // 
            this.picPayIcon.Location = new System.Drawing.Point(20, 4);
            this.picPayIcon.Name = "picPayIcon";
            this.picPayIcon.Size = new System.Drawing.Size(24, 24);
            this.picPayIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPayIcon.TabIndex = 0;
            this.picPayIcon.TabStop = false;
            // 
            // lblPayTitle
            // 
            this.lblPayTitle.AutoSize = true;
            this.lblPayTitle.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPayTitle.Location = new System.Drawing.Point(52, 6);
            this.lblPayTitle.Name = "lblPayTitle";
            this.lblPayTitle.Size = new System.Drawing.Size(118, 23);
            this.lblPayTitle.TabIndex = 1;
            this.lblPayTitle.Text = "選擇付款方式";
            // 
            // flpPayment
            // 
            this.flpPayment.Location = new System.Drawing.Point(20, 36);
            this.flpPayment.Name = "flpPayment";
            this.flpPayment.Size = new System.Drawing.Size(500, 62);
            this.flpPayment.TabIndex = 2;
            this.flpPayment.WrapContents = false;
            // 
            // FormCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(560, 760);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCheckout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormCheckout";
            this.pnlFooter.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlDetailTitle.ResumeLayout(false);
            this.pnlDetailTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReceiptIcon)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlCountRow.ResumeLayout(false);
            this.pnlCountRow.PerformLayout();
            this.pnlTotalRow.ResumeLayout(false);
            this.pnlTotalRow.PerformLayout();
            this.pnlPayment.ResumeLayout(false);
            this.pnlPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPayIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnConfirmOrder;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlDetailTitle;
        private System.Windows.Forms.PictureBox picReceiptIcon;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.FlowLayoutPanel flpOrderItems;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Panel pnlTotalRow;
        private System.Windows.Forms.Panel pnlCountRow;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel pnlPayment;
        private System.Windows.Forms.Label lblPayTitle;
        private System.Windows.Forms.PictureBox picPayIcon;
        private System.Windows.Forms.FlowLayoutPanel flpPayment;
    }
}