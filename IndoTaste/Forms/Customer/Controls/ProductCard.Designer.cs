namespace IndoTaste.Forms.Customer.Controls
{
    partial class ProductCard
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpCard = new System.Windows.Forms.TableLayoutPanel();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.lblBadge = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.pnlPriceRow = new System.Windows.Forms.Panel();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.tlpCard.SuspendLayout();
            this.pnlImage.SuspendLayout();
            this.pnlPriceRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpCard
            // 
            this.tlpCard.ColumnCount = 1;
            this.tlpCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCard.Controls.Add(this.pnlImage, 0, 0);
            this.tlpCard.Controls.Add(this.lblName, 0, 1);
            this.tlpCard.Controls.Add(this.lblDesc, 0, 2);
            this.tlpCard.Controls.Add(this.pnlPriceRow, 0, 3);
            this.tlpCard.Controls.Add(this.btnAddToCart, 0, 4);
            this.tlpCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCard.Location = new System.Drawing.Point(0, 0);
            this.tlpCard.Name = "tlpCard";
            this.tlpCard.RowCount = 5;
            this.tlpCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tlpCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpCard.Size = new System.Drawing.Size(365, 330);
            this.tlpCard.TabIndex = 0;
            // 
            // pnlImage
            // 
            this.pnlImage.Controls.Add(this.lblBadge);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(3, 3);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(359, 164);
            this.pnlImage.TabIndex = 0;
            // 
            // lblBadge
            // 
            this.lblBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(168)))), ((int)(((byte)(42)))));
            this.lblBadge.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblBadge.ForeColor = System.Drawing.Color.Black;
            this.lblBadge.Location = new System.Drawing.Point(12, 12);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Size = new System.Drawing.Size(56, 22);
            this.lblBadge.TabIndex = 0;
            this.lblBadge.Text = "熱門";
            this.lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblName.Location = new System.Drawing.Point(3, 170);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblName.Size = new System.Drawing.Size(359, 34);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "商品名稱";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDesc.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblDesc.Location = new System.Drawing.Point(3, 204);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblDesc.Size = new System.Drawing.Size(359, 28);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "商品簡短描述";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPriceRow
            // 
            this.pnlPriceRow.Controls.Add(this.lblRating);
            this.pnlPriceRow.Controls.Add(this.lblPrice);
            this.pnlPriceRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPriceRow.Location = new System.Drawing.Point(3, 235);
            this.pnlPriceRow.Name = "pnlPriceRow";
            this.pnlPriceRow.Size = new System.Drawing.Size(359, 28);
            this.pnlPriceRow.TabIndex = 3;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRating.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRating.ForeColor = System.Drawing.Color.Orange;
            this.lblRating.Location = new System.Drawing.Point(273, 0);
            this.lblRating.Name = "lblRating";
            this.lblRating.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblRating.Size = new System.Drawing.Size(86, 22);
            this.lblRating.TabIndex = 1;
            this.lblRating.Text = "商品評分";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPrice.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(29)))), ((int)(((byte)(37)))));
            this.lblPrice.Location = new System.Drawing.Point(0, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblPrice.Size = new System.Drawing.Size(100, 25);
            this.lblPrice.TabIndex = 0;
            this.lblPrice.Text = "商品價錢";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(29)))), ((int)(((byte)(37)))));
            this.btnAddToCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(203, 278);
            this.btnAddToCart.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(150, 40);
            this.btnAddToCart.TabIndex = 4;
            this.btnAddToCart.Text = "+ 加入購物車";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            // 
            // ProductCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpCard);
            this.Name = "ProductCard";
            this.Size = new System.Drawing.Size(365, 330);
            this.tlpCard.ResumeLayout(false);
            this.tlpCard.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            this.pnlPriceRow.ResumeLayout(false);
            this.pnlPriceRow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpCard;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Label lblBadge;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Panel pnlPriceRow;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnAddToCart;
    }
}
