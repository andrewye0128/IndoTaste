using IndoTaste.Helpers;
using IndoTaste.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndoTaste.Forms.Customer.Controls
{
    public partial class CartItemRow : UserControl
    {
        private CartItem _item;
        private Image _thumbImage;

        /// <summary>此列對應的購物車項目</summary>
        public CartItem Item => _item;

        /// <summary>按下鉛筆：要求編輯這筆項目</summary>
        public event EventHandler<CartItem> EditRequested;

        /// <summary>按下垃圾桶：要求刪除這筆項目</summary>
        public event EventHandler<CartItem> RemoveRequested;

        /// <summary>設計工具用的無參數建構子</summary>
        public CartItemRow()
        {
            InitializeComponent();
            ApplyStyle();
        }

        public CartItemRow(CartItem item) : this()
        {
            BindItem(item);
        }

        #region 樣式

        private void ApplyStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint, true);

            this.BackColor = Color.White;

            // 整列圓角
            this.Resize += (s, e) =>
            {
                UiHelper.ApplyRoundedRegion(this, 12);
                LayoutRow();
            };
            UiHelper.ApplyRoundedRegion(this, 12);

            // 縮圖圓角 + 自繪（等比例填滿不變形）
            picThumb.Paint += PicThumb_Paint;
            picThumb.Resize += (s, e) => UiHelper.ApplyRoundedRegion(picThumb, 10);
            UiHelper.ApplyRoundedRegion(picThumb, 10);

            // 兩個圓形按鈕
            InitIconButton(btnEdit, "icon_edit", "✎", Color.FromArgb(120, 105, 95));
            InitIconButton(btnDelete, "icon_trash", "🗑", Color.FromArgb(183, 29, 37));

            btnEdit.Click += (s, e) => EditRequested?.Invoke(this, _item);
            btnDelete.Click += (s, e) => RemoveRequested?.Invoke(this, _item);

            this.Disposed += (s, e) => _thumbImage?.Dispose();
        }

        /// <summary>
        /// 設定圓形圖示按鈕；找不到圖檔時退回顯示文字符號
        /// </summary>
        private void InitIconButton(Button btn, string iconName, string fallbackText, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 238, 228);
            btn.BackColor = Color.White;
            btn.ForeColor = color;
            btn.Cursor = Cursors.Hand;
            btn.Text = "";

            Image icon = LoadIcon(iconName, 20);

            if (icon != null)
            {
                btn.Image = icon;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                btn.Text = fallbackText;
                btn.Font = new Font("微軟正黑體", 12);
            }

            btn.Resize += (s, e) => UiHelper.ApplyRoundedRegion(btn, btn.Height / 2);
            UiHelper.ApplyRoundedRegion(btn, btn.Height / 2);
        }

        private void PicThumb_Paint(object sender, PaintEventArgs e)
        {
            if (_thumbImage == null) return;
            UiHelper.DrawImageCover(e.Graphics, _thumbImage, picThumb.ClientRectangle);
        }

        /// <summary>
        /// 依實際寬度重新排版：左側文字自動延展，右側按鈕靠右對齊
        /// </summary>
        private void LayoutRow()
        {
            if (this.Width <= 0) return;

            const int edge = 12;   // 與左右邊界的距離
            const int gap = 6;     // 兩顆按鈕之間

            // 右側兩顆按鈕靠右
            btnDelete.Left = this.Width - edge - btnDelete.Width;
            btnEdit.Left = btnDelete.Left - gap - btnEdit.Width;

            // 數量顯示在按鈕上方、靠右
            lblQuantity.Left = this.Width - edge - lblQuantity.Width;

            // 左側文字區：從縮圖右緣開始，延伸到按鈕左緣前
            int textLeft = picThumb.Right + 12;
            int textWidth = Math.Max(60, btnEdit.Left - textLeft - 8);

            lblName.Left = textLeft;
            lblName.Width = textWidth;

            lblOptions.Left = textLeft;
            lblOptions.Width = textWidth;

            lblPrice.Left = textLeft;
            lblPrice.Width = textWidth;
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
                    return new Bitmap(original, new Size(size, size));
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 資料綁定

        public void BindItem(CartItem item)
        {
            _item = item;
            if (item == null) return;

            lblName.Text = item.Product.NameZh;
            lblOptions.Text = item.OptionsSummary;
            lblPrice.Text = $"NT$ {item.Product.Price:0}";
            lblQuantity.Text = item.Quantity.ToString();

            // 沒有選項時把該行收起來，讓名稱與價格看起來不會太空
            lblOptions.Visible = !string.IsNullOrEmpty(item.OptionsSummary);

            LoadThumb(item.Product.ImageFileName);
            LayoutRow();
        }

        private void LoadThumb(string fileName)
        {
            _thumbImage?.Dispose();
            _thumbImage = null;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                picThumb.Invalidate();
                return;
            }

            string path = Path.Combine(UiHelper.GetAssetsFolder("Images", "Products"), fileName);

            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine($"找不到商品縮圖：{path}");
                picThumb.BackColor = Color.FromArgb(235, 230, 222);
                picThumb.Invalidate();
                return;
            }

            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var temp = Image.FromStream(fs))
                {
                    _thumbImage = new Bitmap(temp);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"載入商品縮圖失敗：{ex.Message}");
            }

            picThumb.Invalidate();
        }

        #endregion

        private void CartItemRow_Load(object sender, EventArgs e) { }

    }
}
