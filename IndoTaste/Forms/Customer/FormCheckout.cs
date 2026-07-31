using IndoTaste.Helpers;
using IndoTaste.Models;
using IndoTaste.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace IndoTaste.Forms.Customer
{
    public partial class FormCheckout : Form
    {
        private readonly ShoppingCart _cart;
        private readonly List<Image> _loadedImages = new List<Image>();

        // 付款方式
        private static readonly string[] PaymentMethods = { "現金", "LINE Pay", "信用卡" };
        private const string DefaultPayment = "現金";

        private Button _selectedPaymentButton;

        /// <summary>使用者選擇的付款方式</summary>
        public string SelectedPaymentMethod { get; private set; } = DefaultPayment;

        public FormCheckout(ShoppingCart cart)
        {
            InitializeComponent();

            _cart = cart;

            ApplyStyle();
            CreatePaymentButtons();
            SetupEvents();
            BindCart();
        }

        //#region 樣式

        private void ApplyStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint, true);

            // 視窗圓角
            AttachRoundedRegion(this, 18);

            // 關閉按鈕：圓形
            StyleGhostButton(btnClose, circle: true);

            // 統計列與總金額列：白底圓角
            AttachRoundedRegion(pnlCountRow, 10);
            AttachRoundedRegion(pnlTotalRow, 12);

            // 底部按鈕
            StyleGhostButton(btnBack, circle: false);

            btnConfirmOrder.FlatStyle = FlatStyle.Flat;
            btnConfirmOrder.FlatAppearance.BorderSize = 0;
            btnConfirmOrder.BackColor = Color.FromArgb(183, 29, 37);
            btnConfirmOrder.ForeColor = Color.White;
            btnConfirmOrder.Cursor = Cursors.Hand;
            AttachRoundedRegion(btnConfirmOrder, 12);

            btnConfirmOrder.MouseEnter += (s, e) =>
                btnConfirmOrder.BackColor = Color.FromArgb(150, 22, 30);
            btnConfirmOrder.MouseLeave += (s, e) =>
                btnConfirmOrder.BackColor = Color.FromArgb(183, 29, 37);

            // 標題列 icon
            picReceiptIcon.Image = LoadIcon("icon_receipt", 24);
            picPayIcon.Image = LoadIcon("icon_wallet", 24);

            EnableDragByTitle();

            this.FormClosed += (s, e) =>
            {
                foreach (var img in _loadedImages)
                    img?.Dispose();
                _loadedImages.Clear();
            };
        }

        /// <summary>白底 + 淺色圓角外框的按鈕樣式</summary>
        private void StyleGhostButton(Button btn, bool circle)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(90, 78, 70);
            btn.Cursor = Cursors.Hand;

            btn.Paint += (s, e) =>
            {
                var b = (Button)s;
                int r = circle ? b.Height / 2 : 12;
                UiHelper.DrawRoundedBorder(e.Graphics, b.ClientRectangle, r,
                    Color.FromArgb(226, 214, 196), 1);
            };

            AttachRoundedRegion(btn, circle ? -1 : 12);
        }

        /// <summary>套用圓角並在尺寸改變時自動重算（radius 傳負值代表圓形／膠囊）</summary>
        private void AttachRoundedRegion(Control ctrl, int radius)
        {
            void Apply()
            {
                int r = radius < 0 ? ctrl.Height / 2 : radius;
                UiHelper.ApplyRoundedRegion(ctrl, r);
                ctrl.Invalidate();
            }

            ctrl.Resize += (s, e) => Apply();
            Apply();
        }

        //#endregion

        //#region 付款方式按鈕

        private void CreatePaymentButtons()
        {
            flpPayment.Controls.Clear();
            flpPayment.WrapContents = false;
            flpPayment.AutoScroll = false;

            foreach (string method in PaymentMethods)
            {
                var btn = new Button
                {
                    Text = method,
                    Font = new Font("微軟正黑體", 10),
                    AutoSize = false,
                    Cursor = Cursors.Hand,
                    Tag = method
                };

                ApplyPaymentStyle(btn, false);
                AttachRoundedRegion(btn, 10);

                btn.Paint += (s, e) =>
                {
                    var b = (Button)s;
                    bool selected = (b == _selectedPaymentButton);
                    
                    UiHelper.DrawRoundedBorder(
                        e.Graphics, b.ClientRectangle, 10,
                        selected ? Color.FromArgb(183, 29, 37) : Color.FromArgb(226, 214, 196),

                        selected ? 2 : 1);
                };

                btn.Click += (s, e) => SelectPayment((Button)s);

                flpPayment.Controls.Add(btn);
            }

            // 預設選取
            var defaultBtn = flpPayment.Controls.OfType<Button>()
                                .FirstOrDefault(b => (b.Tag as string) == DefaultPayment)
                             ?? flpPayment.Controls.OfType<Button>().FirstOrDefault();

            if (defaultBtn != null)
                SelectPayment(defaultBtn);

            flpPayment.Resize += (s, e) => LayoutPaymentButtons();
            LayoutPaymentButtons();
        }

        private void SelectPayment(Button btn)
        {
            Button previous = _selectedPaymentButton;

            _selectedPaymentButton = btn;
            SelectedPaymentMethod = btn.Tag as string;

            if (previous != null && previous != btn)
                ApplyPaymentStyle(previous, false);

            ApplyPaymentStyle(btn, true);
        }

        private void ApplyPaymentStyle(Button btn, bool selected)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            //btn.BackColor = selected ? Color.FromArgb(253, 243, 243) : Color.White;
            btn.BackColor = selected ? Color.FromArgb(183, 29, 37) : Color.White;

            //btn.ForeColor = selected ? Color.FromArgb(183, 29, 37) : Color.FromArgb(100, 88, 80);
            btn.ForeColor = selected ? Color.White : Color.FromArgb(100, 88, 80);


            btn.Invalidate();
        }

        /// <summary>三顆按鈕平均分配寬度</summary>
        private void LayoutPaymentButtons()
        {
            var buttons = flpPayment.Controls.OfType<Button>().ToList();
            if (buttons.Count == 0) return;

            const int gap = 8;
            int count = buttons.Count;

            int available = flpPayment.ClientSize.Width - flpPayment.Padding.Horizontal;
            int btnWidth = (available - gap * (count - 1)) / count;
            int btnHeight = flpPayment.ClientSize.Height - flpPayment.Padding.Vertical;

            for (int i = 0; i < count; i++)
            {
                buttons[i].Margin = new Padding(0, 0, i == count - 1 ? 0 : gap, 0);
                buttons[i].Size = new Size(Math.Max(60, btnWidth), Math.Max(30, btnHeight));
            }
        }

        //#endregion

        //#region 訂單明細

        private void BindCart()
        {
            flpOrderItems.SuspendLayout();
            flpOrderItems.Controls.Clear();

            if (_cart != null)
            {
                foreach (var item in _cart.Items)
                    flpOrderItems.Controls.Add(CreateOrderItemRow(item));
            }

            flpOrderItems.ResumeLayout(true);

            // 統計與總金額
            lblItemCount.Text = $"共 {_cart?.ItemCount ?? 0} 項商品";
            lblTotalQty.Text = $"總數量 {_cart?.TotalCount ?? 0} 件";
            lblTotalAmount.Text = $"NT$ {_cart?.TotalAmount ?? 0:0}";
        }

        /// <summary>
        /// 產生一列訂單明細：縮圖 / 名稱 / 選項 / 數量 / 小計
        /// </summary>
        private Control CreateOrderItemRow(CartItem item)
        {
            var row = new Panel
            {
                Height = 76,
                Width = Math.Max(300, flpOrderItems.ClientSize.Width - 8),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8)
            };

            var picThumb = new PictureBox
            {
                Size = new Size(58, 58),
                Location = new Point(9, 9),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(238, 232, 224)
            };

            Image thumb = LoadProductImage(item.Product.ImageFileName);
            if (thumb != null)
            {
                picThumb.Image = thumb;
                _loadedImages.Add(thumb);
            }

            var lblName = new Label
            {
                Text = item.Product.NameZh,
                Font = new Font("微軟正黑體", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 45, 40),
                AutoSize = false,
                Size = new Size(180, 24),
                Location = new Point(78, 12),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblOptions = new Label
            {
                Text = item.OptionsSummary,
                Font = new Font("微軟正黑體", 9),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(180, 20),
                Location = new Point(78, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblQty = new Label
            {
                Text = $"× {item.Quantity}",
                Font = new Font("微軟正黑體", 10),
                ForeColor = Color.FromArgb(90, 78, 70),
                AutoSize = false,
                Size = new Size(90, 22),
                Location = new Point(0, 12),
                TextAlign = ContentAlignment.MiddleRight
            };

            // 顯示小計（單價 × 數量）
            var lblSubtotal = new Label
            {
                Text = $"NT$ {item.Subtotal:0}",
                Font = new Font("微軟正黑體", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(183, 29, 37),
                AutoSize = false,
                Size = new Size(110, 24),
                Location = new Point(0, 40),
                TextAlign = ContentAlignment.MiddleRight
            };

            row.Controls.Add(picThumb);
            row.Controls.Add(lblName);
            row.Controls.Add(lblOptions);
            row.Controls.Add(lblQty);
            row.Controls.Add(lblSubtotal);

            // 依實際寬度排版：右側金額靠右，左側文字自動延展
            void LayoutRow()
            {
                int w = row.ClientSize.Width;
                if (w <= 0) return;

                const int edge = 12;

                lblQty.Left = w - edge - lblQty.Width;
                lblSubtotal.Left = w - edge - lblSubtotal.Width;

                int textWidth = Math.Max(60, lblSubtotal.Left - 78 - 8);
                lblName.Width = textWidth;
                lblOptions.Width = textWidth;

                UiHelper.ApplyRoundedRegion(row, 10);
            }

            row.Resize += (s, e) => LayoutRow();
            LayoutRow();

            return row;
        }

        private Image LoadProductImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;

            string path = Path.Combine(UiHelper.GetAssetsFolder("Images", "Products"), fileName);

            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine($"找不到商品縮圖：{path}");
                return null;
            }

            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var temp = Image.FromStream(fs))
                {
                    return new Bitmap(temp);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"載入商品縮圖失敗：{ex.Message}");
                return null;
            }
        }

        private Image LoadIcon(string iconName, int size)
        {
            string path = Path.Combine(UiHelper.GetAssetsFolder("Icons"), iconName + ".png");

            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine($"找不到 Icon：{path}");
                return null;
            }

            try
            {
                using (var original = new Bitmap(path))
                {
                    var img = new Bitmap(original, new Size(size, size));
                    _loadedImages.Add(img);
                    return img;
                }
            }
            catch
            {
                return null;
            }
        }

        //#endregion

        //#region 事件

        private void SetupEvents()
        {
            btnClose.Click += (s, e) => CloseWithCancel();
            btnBack.Click += (s, e) => CloseWithCancel();

            btnConfirmOrder.Click += (s, e) =>
            {
                if (_cart == null || _cart.IsEmpty)
                {
                    MessageBox.Show("購物車是空的，無法結帳。", "提示");
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            };
        }

        private void CloseWithCancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>無邊框視窗，改用拖曳標題列移動</summary>
        private void EnableDragByTitle()
        {
            bool dragging = false;
            Point dragStart = Point.Empty;

            void OnMouseDown(object s, MouseEventArgs e)
            {
                if (e.Button != MouseButtons.Left) return;
                dragging = true;
                dragStart = e.Location;
            }

            void OnMouseMove(object s, MouseEventArgs e)
            {
                if (!dragging) return;
                this.Location = new Point(
                    this.Left + e.X - dragStart.X,
                    this.Top + e.Y - dragStart.Y);
            }

            void OnMouseUp(object s, MouseEventArgs e) => dragging = false;

            pnlTitle.MouseDown += OnMouseDown;
            pnlTitle.MouseMove += OnMouseMove;
            pnlTitle.MouseUp += OnMouseUp;

            lblTitle.MouseDown += OnMouseDown;
            lblTitle.MouseMove += OnMouseMove;
            lblTitle.MouseUp += OnMouseUp;
        }

        //#endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // 顯示後才有最終尺寸，重算按鈕與明細列寬度
            LayoutPaymentButtons();

            foreach (Control row in flpOrderItems.Controls)
                row.Width = Math.Max(300, flpOrderItems.ClientSize.Width - 8);

            this.ActiveControl = null;
        }
    }
}