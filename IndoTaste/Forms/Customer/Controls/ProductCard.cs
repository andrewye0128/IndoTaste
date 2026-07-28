using IndoTaste.Helpers;
using IndoTaste.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IndoTaste.Forms.Customer.Controls
{
    public partial class ProductCard : UserControl
    {
        private Product _product;
        private Image _productImage;

        /// <summary>此卡片對應的商品資料</summary>
        public Product Product => _product;

        /// <summary>按下「加入購物車」時觸發，由 Form 接手處理購物車邏輯</summary>
        public event EventHandler<Product> AddToCartClicked;

        /// <summary>無參數建構子：設計工具必須保留</summary>
        public ProductCard()
        {
            InitializeComponent();
            ApplyStyle();
        }

        public ProductCard(Product product) : this()
        {
            BindProduct(product);
        }

        /// <summary>
        /// 設定外觀樣式（圓角、去閃爍、hover 效果）
        /// </summary>
        private void ApplyStyle()
        {
            // 減少重繪閃爍，卡片數量多時效果明顯
            SetStyle(ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint, true);

            this.BackColor = Color.White;

            // 卡片本體：四角圓角
            this.Resize += (s, e) => UiHelper.ApplyRoundedRegion(this, 14);
            UiHelper.ApplyRoundedRegion(this, 14);

            // 商品圖片：只有上方兩角圓角，下方維持直角
            pnlImage.Resize += (s, e) =>
            {
                UiHelper.ApplyRoundedRegion(pnlImage, 14,
                    topLeft: true, topRight: true,
                    bottomRight: false, bottomLeft: false);
                pnlImage.Invalidate();
            };
            UiHelper.ApplyRoundedRegion(pnlImage, 14,
                topLeft: true, topRight: true,
                bottomRight: false, bottomLeft: false);

            // 圖片改用自繪，確保等比例不變形
            pnlImage.Paint += PnlImage_Paint;

            // 熱門標籤：小圓角
            lblBadge.Resize += (s, e) => UiHelper.ApplyRoundedRegion(lblBadge, 6);
            UiHelper.ApplyRoundedRegion(lblBadge, 6);

            // 加入購物車按鈕：圓角 + hover 變色
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.Resize += (s, e) => UiHelper.ApplyRoundedRegion(btnAddToCart, 8);
            UiHelper.ApplyRoundedRegion(btnAddToCart, 8);

            btnAddToCart.MouseEnter += (s, e) =>
                btnAddToCart.BackColor = Color.FromArgb(150, 22, 30);
            btnAddToCart.MouseLeave += (s, e) =>
                btnAddToCart.BackColor = Color.FromArgb(183, 29, 37);

            btnAddToCart.Click += (s, e) =>
                AddToCartClicked?.Invoke(this, _product);

            // 卡片釋放時一併釋放圖片，避免記憶體累積
            this.Disposed += (s, e) => _productImage?.Dispose();
        }

        private void PnlImage_Paint(object sender, PaintEventArgs e)
        {
            if (_productImage == null) return;
            UiHelper.DrawImageCover(e.Graphics, _productImage, pnlImage.ClientRectangle);
        }

        /// <summary>
        /// 將商品資料套用到卡片上的各個控制項
        /// </summary>
        public void BindProduct(Product product)
        {
            _product = product;
            if (product == null) return;

            lblName.Text = product.DisplayName;
            lblDesc.Text = product.Description;
            lblPrice.Text = $"NT$ {product.Price:0}";
            lblRating.Text = $"★ {product.Rating:0.0}";

            // 非熱門商品不顯示標籤
            lblBadge.Visible = product.IsPopular;

            LoadProductImage(product.ImageFileName);
        }

        private void LoadProductImage(string fileName)
        {
            _productImage?.Dispose();
            _productImage = null;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                pnlImage.Invalidate();
                return;
            }

            string imagePath = Path.Combine(
                UiHelper.GetAssetsFolder("Images", "Products"), fileName);

            if (!File.Exists(imagePath))
            {
                System.Diagnostics.Debug.WriteLine($"找不到商品圖片：{imagePath}");
                pnlImage.BackColor = Color.FromArgb(235, 230, 222); // 沒有圖片時的底色
                pnlImage.Invalidate();
                return;
            }

            try
            {
                // 讀進記憶體後立刻釋放檔案控制代碼，避免檔案被鎖住
                using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (var temp = Image.FromStream(fs))
                {
                    _productImage = new Bitmap(temp);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"載入商品圖片失敗：{imagePath}");
                System.Diagnostics.Debug.WriteLine($"錯誤訊息：{ex.Message}");
            }

            pnlImage.Invalidate();
        }
    }
}