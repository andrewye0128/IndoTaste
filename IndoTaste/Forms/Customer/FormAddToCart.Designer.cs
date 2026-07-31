namespace IndoTaste.Forms.Customer
{
    partial class FormAddToCart
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProductInfo = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblProductDesc = new System.Windows.Forms.Label();
            this.lblProductPrice = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.pnlQuantity = new System.Windows.Forms.Panel();
            this.pnlStepper = new System.Windows.Forms.Panel();
            this.btnPlus = new System.Windows.Forms.Button();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.lblQtyTitle = new System.Windows.Forms.Label();
            this.picQtyIcon = new System.Windows.Forms.PictureBox();
            this.pnlSpice = new System.Windows.Forms.Panel();
            this.flpSpice = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSpiceTitle = new System.Windows.Forms.Label();
            this.picSpiceIcon = new System.Windows.Forms.PictureBox();
            this.pnlDining = new System.Windows.Forms.Panel();
            this.btnDineIn = new System.Windows.Forms.Button();
            this.btnTakeout = new System.Windows.Forms.Button();
            this.lblDiningTitle = new System.Windows.Forms.Label();
            this.picDiningIcon = new System.Windows.Forms.PictureBox();
            this.pnlIce = new System.Windows.Forms.Panel();
            this.pnlSweet = new System.Windows.Forms.Panel();
            this.picIceIcon = new System.Windows.Forms.PictureBox();
            this.picSweetIcon = new System.Windows.Forms.PictureBox();
            this.lblIceTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flpIce = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSweet = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnConfirmAdd = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlProductInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            this.pnlQuantity.SuspendLayout();
            this.pnlStepper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQtyIcon)).BeginInit();
            this.pnlSpice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpiceIcon)).BeginInit();
            this.pnlDining.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiningIcon)).BeginInit();
            this.pnlIce.SuspendLayout();
            this.pnlSweet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIceIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSweetIcon)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlProductInfo, 0, 0);
            this.tlpMain.Controls.Add(this.pnlQuantity, 0, 1);
            this.tlpMain.Controls.Add(this.pnlSpice, 0, 2);
            this.tlpMain.Controls.Add(this.pnlDining, 0, 5);
            this.tlpMain.Controls.Add(this.pnlIce, 0, 3);
            this.tlpMain.Controls.Add(this.pnlSweet, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpMain.Size = new System.Drawing.Size(660, 970);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlProductInfo
            // 
            this.pnlProductInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlProductInfo.Controls.Add(this.btnClose);
            this.pnlProductInfo.Controls.Add(this.lblProductDesc);
            this.pnlProductInfo.Controls.Add(this.lblProductPrice);
            this.pnlProductInfo.Controls.Add(this.lblProductName);
            this.pnlProductInfo.Controls.Add(this.picProduct);
            this.pnlProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlProductInfo.Name = "pnlProductInfo";
            this.pnlProductInfo.Size = new System.Drawing.Size(654, 204);
            this.pnlProductInfo.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.Location = new System.Drawing.Point(594, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblProductDesc
            // 
            this.lblProductDesc.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblProductDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblProductDesc.Location = new System.Drawing.Point(296, 128);
            this.lblProductDesc.Name = "lblProductDesc";
            this.lblProductDesc.Size = new System.Drawing.Size(290, 70);
            this.lblProductDesc.TabIndex = 3;
            this.lblProductDesc.Text = "label1";
            // 
            // lblProductPrice
            // 
            this.lblProductPrice.AutoSize = true;
            this.lblProductPrice.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblProductPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(29)))), ((int)(((byte)(37)))));
            this.lblProductPrice.Location = new System.Drawing.Point(296, 78);
            this.lblProductPrice.Name = "lblProductPrice";
            this.lblProductPrice.Size = new System.Drawing.Size(80, 29);
            this.lblProductPrice.TabIndex = 2;
            this.lblProductPrice.Text = "label1";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblProductName.Location = new System.Drawing.Point(296, 34);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(114, 32);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "商品名稱";
            // 
            // picProduct
            // 
            this.picProduct.Location = new System.Drawing.Point(20, 24);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(250, 190);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 0;
            this.picProduct.TabStop = false;
            // 
            // pnlQuantity
            // 
            this.pnlQuantity.BackColor = System.Drawing.Color.Transparent;
            this.pnlQuantity.Controls.Add(this.pnlStepper);
            this.pnlQuantity.Controls.Add(this.lblQtyTitle);
            this.pnlQuantity.Controls.Add(this.picQtyIcon);
            this.pnlQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuantity.Location = new System.Drawing.Point(3, 213);
            this.pnlQuantity.Name = "pnlQuantity";
            this.pnlQuantity.Size = new System.Drawing.Size(654, 79);
            this.pnlQuantity.TabIndex = 1;
            // 
            // pnlStepper
            // 
            this.pnlStepper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.pnlStepper.Controls.Add(this.btnPlus);
            this.pnlStepper.Controls.Add(this.lblQuantity);
            this.pnlStepper.Controls.Add(this.btnMinus);
            this.pnlStepper.Location = new System.Drawing.Point(240, 12);
            this.pnlStepper.Name = "pnlStepper";
            this.pnlStepper.Size = new System.Drawing.Size(340, 56);
            this.pnlStepper.TabIndex = 2;
            // 
            // btnPlus
            // 
            this.btnPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPlus.Location = new System.Drawing.Point(284, 8);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(40, 40);
            this.btnPlus.TabIndex = 2;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblQuantity.Location = new System.Drawing.Point(140, 8);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(60, 40);
            this.lblQuantity.TabIndex = 1;
            this.lblQuantity.Text = "1";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMinus
            // 
            this.btnMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMinus.Location = new System.Drawing.Point(16, 8);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(40, 40);
            this.btnMinus.TabIndex = 0;
            this.btnMinus.Text = "−";
            this.btnMinus.UseVisualStyleBackColor = true;
            // 
            // lblQtyTitle
            // 
            this.lblQtyTitle.AutoSize = true;
            this.lblQtyTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblQtyTitle.Location = new System.Drawing.Point(60, 16);
            this.lblQtyTitle.Name = "lblQtyTitle";
            this.lblQtyTitle.Size = new System.Drawing.Size(92, 25);
            this.lblQtyTitle.TabIndex = 1;
            this.lblQtyTitle.Text = "調整數量";
            // 
            // picQtyIcon
            // 
            this.picQtyIcon.Location = new System.Drawing.Point(24, 14);
            this.picQtyIcon.Name = "picQtyIcon";
            this.picQtyIcon.Size = new System.Drawing.Size(28, 28);
            this.picQtyIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQtyIcon.TabIndex = 0;
            this.picQtyIcon.TabStop = false;
            // 
            // pnlSpice
            // 
            this.pnlSpice.BackColor = System.Drawing.Color.Transparent;
            this.pnlSpice.Controls.Add(this.flpSpice);
            this.pnlSpice.Controls.Add(this.lblSpiceTitle);
            this.pnlSpice.Controls.Add(this.picSpiceIcon);
            this.pnlSpice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSpice.Location = new System.Drawing.Point(3, 298);
            this.pnlSpice.Name = "pnlSpice";
            this.pnlSpice.Size = new System.Drawing.Size(654, 124);
            this.pnlSpice.TabIndex = 2;
            // 
            // flpSpice
            // 
            this.flpSpice.Location = new System.Drawing.Point(24, 52);
            this.flpSpice.Name = "flpSpice";
            this.flpSpice.Size = new System.Drawing.Size(572, 62);
            this.flpSpice.TabIndex = 2;
            this.flpSpice.WrapContents = false;
            // 
            // lblSpiceTitle
            // 
            this.lblSpiceTitle.AutoSize = true;
            this.lblSpiceTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSpiceTitle.Location = new System.Drawing.Point(60, 16);
            this.lblSpiceTitle.Name = "lblSpiceTitle";
            this.lblSpiceTitle.Size = new System.Drawing.Size(92, 25);
            this.lblSpiceTitle.TabIndex = 1;
            this.lblSpiceTitle.Text = "調整辣度";
            // 
            // picSpiceIcon
            // 
            this.picSpiceIcon.Location = new System.Drawing.Point(24, 14);
            this.picSpiceIcon.Name = "picSpiceIcon";
            this.picSpiceIcon.Size = new System.Drawing.Size(28, 28);
            this.picSpiceIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSpiceIcon.TabIndex = 0;
            this.picSpiceIcon.TabStop = false;
            // 
            // pnlDining
            // 
            this.pnlDining.BackColor = System.Drawing.Color.Transparent;
            this.pnlDining.Controls.Add(this.pnlFooter);
            this.pnlDining.Controls.Add(this.btnDineIn);
            this.pnlDining.Controls.Add(this.btnTakeout);
            this.pnlDining.Controls.Add(this.lblDiningTitle);
            this.pnlDining.Controls.Add(this.picDiningIcon);
            this.pnlDining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDining.Location = new System.Drawing.Point(3, 688);
            this.pnlDining.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.pnlDining.Name = "pnlDining";
            this.pnlDining.Size = new System.Drawing.Size(654, 274);
            this.pnlDining.TabIndex = 3;
            // 
            // btnDineIn
            // 
            this.btnDineIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDineIn.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDineIn.Location = new System.Drawing.Point(24, 62);
            this.btnDineIn.Name = "btnDineIn";
            this.btnDineIn.Size = new System.Drawing.Size(200, 52);
            this.btnDineIn.TabIndex = 4;
            this.btnDineIn.Text = "內用";
            this.btnDineIn.UseVisualStyleBackColor = true;
            // 
            // btnTakeout
            // 
            this.btnTakeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTakeout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeout.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTakeout.Location = new System.Drawing.Point(240, 62);
            this.btnTakeout.Name = "btnTakeout";
            this.btnTakeout.Size = new System.Drawing.Size(200, 52);
            this.btnTakeout.TabIndex = 3;
            this.btnTakeout.Text = "外帶";
            this.btnTakeout.UseVisualStyleBackColor = true;
            // 
            // lblDiningTitle
            // 
            this.lblDiningTitle.AutoSize = true;
            this.lblDiningTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDiningTitle.Location = new System.Drawing.Point(60, 16);
            this.lblDiningTitle.Name = "lblDiningTitle";
            this.lblDiningTitle.Size = new System.Drawing.Size(52, 25);
            this.lblDiningTitle.TabIndex = 1;
            this.lblDiningTitle.Text = "餐點";
            // 
            // picDiningIcon
            // 
            this.picDiningIcon.Location = new System.Drawing.Point(24, 14);
            this.picDiningIcon.Name = "picDiningIcon";
            this.picDiningIcon.Size = new System.Drawing.Size(28, 28);
            this.picDiningIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDiningIcon.TabIndex = 0;
            this.picDiningIcon.TabStop = false;
            // 
            // pnlIce
            // 
            this.pnlIce.BackColor = System.Drawing.Color.Transparent;
            this.pnlIce.Controls.Add(this.flpIce);
            this.pnlIce.Controls.Add(this.lblIceTitle);
            this.pnlIce.Controls.Add(this.picIceIcon);
            this.pnlIce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIce.Location = new System.Drawing.Point(3, 428);
            this.pnlIce.Name = "pnlIce";
            this.pnlIce.Size = new System.Drawing.Size(654, 124);
            this.pnlIce.TabIndex = 5;
            // 
            // pnlSweet
            // 
            this.pnlSweet.BackColor = System.Drawing.Color.Transparent;
            this.pnlSweet.Controls.Add(this.flpSweet);
            this.pnlSweet.Controls.Add(this.label1);
            this.pnlSweet.Controls.Add(this.picSweetIcon);
            this.pnlSweet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSweet.Location = new System.Drawing.Point(3, 558);
            this.pnlSweet.Name = "pnlSweet";
            this.pnlSweet.Size = new System.Drawing.Size(654, 124);
            this.pnlSweet.TabIndex = 6;
            // 
            // picIceIcon
            // 
            this.picIceIcon.Location = new System.Drawing.Point(24, 14);
            this.picIceIcon.Name = "picIceIcon";
            this.picIceIcon.Size = new System.Drawing.Size(28, 28);
            this.picIceIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIceIcon.TabIndex = 0;
            this.picIceIcon.TabStop = false;
            // 
            // picSweetIcon
            // 
            this.picSweetIcon.Location = new System.Drawing.Point(24, 14);
            this.picSweetIcon.Name = "picSweetIcon";
            this.picSweetIcon.Size = new System.Drawing.Size(26, 26);
            this.picSweetIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSweetIcon.TabIndex = 0;
            this.picSweetIcon.TabStop = false;
            // 
            // lblIceTitle
            // 
            this.lblIceTitle.AutoSize = true;
            this.lblIceTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIceTitle.Location = new System.Drawing.Point(60, 16);
            this.lblIceTitle.Name = "lblIceTitle";
            this.lblIceTitle.Size = new System.Drawing.Size(52, 25);
            this.lblIceTitle.TabIndex = 1;
            this.lblIceTitle.Text = "冰塊";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(60, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "甜度";
            // 
            // flpIce
            // 
            this.flpIce.Location = new System.Drawing.Point(24, 52);
            this.flpIce.Name = "flpIce";
            this.flpIce.Size = new System.Drawing.Size(572, 62);
            this.flpIce.TabIndex = 2;
            this.flpIce.WrapContents = false;
            // 
            // flpSweet
            // 
            this.flpSweet.Location = new System.Drawing.Point(24, 52);
            this.flpSweet.Name = "flpSweet";
            this.flpSweet.Size = new System.Drawing.Size(572, 62);
            this.flpSweet.TabIndex = 2;
            this.flpSweet.WrapContents = false;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(226)))));
            this.pnlFooter.Controls.Add(this.btnConfirmAdd);
            this.pnlFooter.Controls.Add(this.btnContinue);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 184);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(654, 90);
            this.pnlFooter.TabIndex = 10;
            // 
            // btnConfirmAdd
            // 
            this.btnConfirmAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(29)))), ((int)(((byte)(27)))));
            this.btnConfirmAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmAdd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfirmAdd.ForeColor = System.Drawing.Color.White;
            this.btnConfirmAdd.Location = new System.Drawing.Point(296, 20);
            this.btnConfirmAdd.Name = "btnConfirmAdd";
            this.btnConfirmAdd.Size = new System.Drawing.Size(300, 60);
            this.btnConfirmAdd.TabIndex = 1;
            this.btnConfirmAdd.Text = "加入購物車";
            this.btnConfirmAdd.UseVisualStyleBackColor = false;
            // 
            // btnContinue
            // 
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnContinue.ForeColor = System.Drawing.Color.Black;
            this.btnContinue.Location = new System.Drawing.Point(24, 20);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(250, 60);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "繼續選購";
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // FormAddToCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(243)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(660, 970);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAddToCart";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormAddToCart";
            this.tlpMain.ResumeLayout(false);
            this.pnlProductInfo.ResumeLayout(false);
            this.pnlProductInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            this.pnlQuantity.ResumeLayout(false);
            this.pnlQuantity.PerformLayout();
            this.pnlStepper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQtyIcon)).EndInit();
            this.pnlSpice.ResumeLayout(false);
            this.pnlSpice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpiceIcon)).EndInit();
            this.pnlDining.ResumeLayout(false);
            this.pnlDining.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiningIcon)).EndInit();
            this.pnlIce.ResumeLayout(false);
            this.pnlIce.PerformLayout();
            this.pnlSweet.ResumeLayout(false);
            this.pnlSweet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIceIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSweetIcon)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlProductInfo;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label lblProductDesc;
        private System.Windows.Forms.Label lblProductPrice;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlQuantity;
        private System.Windows.Forms.PictureBox picQtyIcon;
        private System.Windows.Forms.Panel pnlStepper;
        private System.Windows.Forms.Label lblQtyTitle;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Panel pnlSpice;
        private System.Windows.Forms.Label lblSpiceTitle;
        private System.Windows.Forms.PictureBox picSpiceIcon;
        private System.Windows.Forms.FlowLayoutPanel flpSpice;
        private System.Windows.Forms.Panel pnlDining;
        private System.Windows.Forms.PictureBox picDiningIcon;
        private System.Windows.Forms.Label lblDiningTitle;
        private System.Windows.Forms.Button btnTakeout;
        private System.Windows.Forms.Button btnDineIn;
        private System.Windows.Forms.Panel pnlIce;
        private System.Windows.Forms.PictureBox picIceIcon;
        private System.Windows.Forms.Panel pnlSweet;
        private System.Windows.Forms.PictureBox picSweetIcon;
        private System.Windows.Forms.FlowLayoutPanel flpIce;
        private System.Windows.Forms.Label lblIceTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpSweet;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnConfirmAdd;
        private System.Windows.Forms.Button btnContinue;
    }
}