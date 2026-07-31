namespace IndoTaste.Forms.Customer.Controls
{
    partial class CartItemRow
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
            this.picThumb = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picThumb)).BeginInit();
            this.SuspendLayout();
            // 
            // picThumb
            // 
            this.picThumb.Location = new System.Drawing.Point(12, 12);
            this.picThumb.Name = "picThumb";
            this.picThumb.Size = new System.Drawing.Size(66, 66);
            this.picThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picThumb.TabIndex = 0;
            this.picThumb.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblName.Location = new System.Drawing.Point(90, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(150, 24);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label1";
            // 
            // lblOptions
            // 
            this.lblOptions.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOptions.ForeColor = System.Drawing.Color.Gray;
            this.lblOptions.Location = new System.Drawing.Point(90, 38);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(150, 20);
            this.lblOptions.TabIndex = 2;
            this.lblOptions.Text = "label1";
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(29)))), ((int)(((byte)(37)))));
            this.lblPrice.Location = new System.Drawing.Point(90, 60);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(110, 22);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "label1";
            // 
            // lblQuantity
            // 
            this.lblQuantity.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblQuantity.Location = new System.Drawing.Point(297, 12);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(30, 24);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "label1";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(246, 47);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(38, 38);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "button1";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(290, 47);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(38, 38);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "button1";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // CartItemRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picThumb);
            this.Name = "CartItemRow";
            this.Size = new System.Drawing.Size(340, 90);
            this.Load += new System.EventHandler(this.CartItemRow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picThumb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picThumb;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}
