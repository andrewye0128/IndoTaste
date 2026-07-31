using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndoTaste.Models;
using IndoTaste.Helpers;
using IndoTaste.Services;   
using IndoTaste.Forms.Customer.Controls;

namespace IndoTaste.Forms.Customer
{
    public partial class FormCustomerOrder : Form
    {
        // 按鈕UI 元件變數
        private Button _selectedCategoryButton;
        private Dictionary<Button, Image> _buttonImages = new Dictionary<Button, Image>();

        // UI 元件變數
        // 商品列表區域
        private TableLayoutPanel tlpProductArea;
        private Panel pnlBanner;
        private Panel pnlSearchBar;
        private Panel pnlProductList;   // 之後放商品卡片的區域
        private TextBox txtSearch;
        private ComboBox cboSort;

        private float _bannerAspectRatio = 0.5f; // 預設值，載入圖片後會重新計算


        private FlowLayoutPanel flpProducts;     // 商品卡片容器
        private List<Product> _allProducts;      // 全部商品資料（篩選時的來源）

        private string _selectedCategoryKey = "all";   // 目前選取的分類

        private string _searchKeyword = "";        // 目前的搜尋關鍵字
        private bool _suppressSearchEvent = false; // 程式自行修改文字時，暫停觸發篩選


        // 購物車區域
        private ShoppingCart _cart = new ShoppingCart();
        private TableLayoutPanel tlpCart;
        private FlowLayoutPanel flpCartItems;
        private Label lblCartTitle;
        private Label lblTotalLabel;
        private Label lblTotalAmount;
        private Button btnClearCart;
        private Button btnCheckout;


        public FormCustomerOrder()
        {
            InitializeComponent();

            // 最先執行：修正主版面，避免後續建立的子控制項跟著錯位
            FixMainLayout();
            //PrintDebugLayout();   // 診斷用，確認沒問題後可移除

            // 設定 FormCustomerOrder 關掉視窗
            //this.FormBorderStyle = FormBorderStyle.None;

            // 設定 FormCustomerOrder 整個視窗的背景顏色
            this.BackColor = Color.FromArgb(248, 244, 236);

            // # UI視窗
            //this.BackColor = Color.FromArgb(249, 243, 233);

            // 設定 pnlProductArea 背景色
            pnlProductArea.BackColor = Color.FromArgb(248, 244, 236);

            SetCategoryPanelStyle();


            // 建立按鈕
            CreateCategoryButtons();

            // 調試：顯示載入資訊
            PrintDebugIconPaths();


            this.flpCategory.Resize += FlpCategory_Resize;


            // 改用 TableLayoutPanel 統一管理 Banner / 搜尋列 / 商品列表 三個區塊
            SetupProductAreaLayout();

            // header : 包含左上角 Logo
            SetupHeader(); 

            // 購物車
            SetupCartArea(); 



        }

        private void SetCategoryPanelStyle()
        {
            flpCategory.Dock = DockStyle.Fill;
            flpCategory.AutoSize = false;
            flpCategory.WrapContents = false;
            flpCategory.FlowDirection = FlowDirection.TopDown;
            flpCategory.AutoScroll = true;

            //flpCategory.Margin = Padding.Empty;  //chgpt修改後
            //this.flpCategory.Margin = new Padding(13, 18, 0, 0);   
            //flpCategory.Padding = new Padding(12);

            this.flpCategory.Margin = new Padding(13, 12, 0, 0);   //目前我修改後滿意 margin 上邊框有距離
            flpCategory.Padding = new Padding(12, 0, 12, 12);

            // 淺米色背景 (#FCF5EA)
            flpCategory.BackColor = Color.FromArgb(252, 245, 234);


        }

        private void FlpCategory_Resize(object sender, EventArgs e)
        {
            ResizeCategoryButtons();
        }

        private void ResizeCategoryButtons()
        {
            foreach (Button btn in flpCategory.Controls.OfType<Button>())
            {
                int availableWidth =
                    flpCategory.ClientSize.Width
                    - flpCategory.Padding.Horizontal
                    - btn.Margin.Horizontal;

                // 只擋掉初始化期間的負數，不再強制最小 160，避免超出容器產生水平捲軸
                btn.Width = Math.Max(1, availableWidth);

                // Button 寬度改變後，要重新計算圓角
                UpdateButtonRoundedCorners(btn);
            }
        }

        /// <summary>
        /// 調試方法：印出所有可能的 icon 路徑
        /// </summary>
        private void PrintDebugIconPaths()
        {
            System.Diagnostics.Debug.WriteLine("=== Icon 載入路徑調試 ===");
            System.Diagnostics.Debug.WriteLine($"應用程式啟動路徑: {Application.StartupPath}");
            System.Diagnostics.Debug.WriteLine($"應用程式基目錄: {AppDomain.CurrentDomain.BaseDirectory}");

            string path1 = Path.Combine(Application.StartupPath, "Properties");
            string path2 = Path.Combine(Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName,
                                        "IndoTaste", "Properties");

            System.Diagnostics.Debug.WriteLine($"嘗試路徑 1: {path1}");
            System.Diagnostics.Debug.WriteLine($"路徑 1 存在: {Directory.Exists(path1)}");

            System.Diagnostics.Debug.WriteLine($"嘗試路徑 2: {path2}");
            System.Diagnostics.Debug.WriteLine($"路徑 2 存在: {Directory.Exists(path2)}");

            System.Diagnostics.Debug.WriteLine("========================");
        }


        /// <summary>
        /// 取得專案 Assets 資料夾路徑（從 bin\Debug 往上跳兩層回到專案資料夾）
        /// </summary>
        private string GetAssetsFolder(string subFolder)
        {
            return Path.Combine(
                Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName,
                "Assets", subFolder);
        }


        // 用來儲存每個按鈕對應的紅色/白色圖示，方便選取切換時取用
        private Dictionary<Button, (Image Red, Image White)> _buttonIconPairs
            = new Dictionary<Button, (Image, Image)>();


        /// <summary>
        /// 載入分類按鈕 icon
        /// </summary>
        private Image LoadCategoryIcon(string iconName)
        {
            string iconPath = Path.Combine(GetAssetsFolder("Icons"), iconName + ".png");


            // 從 bin\Debug 往上跳兩層，回到專案資料夾 IndoTaste\，再進入 Assets\Icons
            //string projectIconsFolder = Path.Combine(
            //    Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName,
            //    "Assets", "Icons");

            //string iconPath = Path.Combine(projectIconsFolder, iconName + ".png");

            if (!File.Exists(iconPath))
            {
                System.Diagnostics.Debug.WriteLine($"找不到分類 Icon：{iconPath}");
                return null;
            }

            try
            {
                using (Image originalImage = Image.FromFile(iconPath))
                {
                    return new Bitmap(originalImage, new Size(28, 28));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"載入分類 Icon 失敗：{iconPath}");
                System.Diagnostics.Debug.WriteLine($"錯誤訊息：{ex.Message}");
                return null;
            }
        }


        /// <summary>
        /// 載入搜尋放大鏡 icon；若檔案不存在則改用程式繪製，確保畫面一定有圖示
        /// </summary>
        private Image LoadSearchIcon(int size)
        {
            string iconPath = Path.Combine(GetAssetsFolder("Icons"), "icon_search.png");

            if (File.Exists(iconPath))
            {
                try
                {
                    using (var original = new Bitmap(iconPath))
                    {
                        var bmp = new Bitmap(size, size);

                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.SmoothingMode = SmoothingMode.AntiAlias;

                            g.DrawImage(original, new Rectangle(0, 0, size, size));
                        }

                        return bmp;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"載入搜尋 Icon 失敗：{ex.Message}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"找不到搜尋 Icon：{iconPath}，改用程式繪製");
            }

            return DrawSearchIcon(size, Color.FromArgb(150, 150, 150));
        }

        /// <summary>
        /// 用 GDI+ 繪製放大鏡圖示（圓形 + 斜線握把）
        /// </summary>
        private Image DrawSearchIcon(int size, Color color)
        {
            var bmp = new Bitmap(size, size);

            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                float penWidth = Math.Max(1.6f, size / 11f);

                using (var pen = new Pen(color, penWidth))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    // 鏡片（圓形）
                    float diameter = size * 0.62f;
                    g.DrawEllipse(pen, penWidth / 2, penWidth / 2, diameter, diameter);

                    // 握把（右下斜線）
                    float start = diameter * 0.87f;
                    g.DrawLine(pen, start, start, size - penWidth, size - penWidth);
                }
            }

            return bmp;
        }


        private void CreateCategoryButtons()
        {
            // 加入第三個欄位 CategoryKey，對應 Product.CategoryKey
            var categories = new (string Text, string IconName, string CategoryKey)[]
            {
                ("全部", "icon_all",       "all"),
                ("熱門", "icon_popular",   "popular"),
                ("主食", "icon_rice",      "rice"),
                ("主菜", "icon_plate",     "plate"),
                ("蔬食", "icon_vegetable", "vegetable"),
                ("炸物", "icon_fries",     "fries"),
                ("甜點", "icon_cake",      "cake"),
                ("飲料", "icon_drink",     "drink"),
            };

            this.flpCategory.Controls.Clear();
            foreach (var (text, iconName, categoryKey) in categories)
            {
                Image redIcon = LoadCategoryIcon(iconName + "_red");
                Image whiteIcon = LoadCategoryIcon(iconName + "_white");

                var btn = new Button
                {
                    Text = "  " + text,
                    Image = redIcon,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextAlign = ContentAlignment.MiddleLeft,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(60, 60, 60),
                    Font = new Font("微軟正黑體", 16, FontStyle.Bold),
                    Height = 56,
                    AutoSize = false,
                    Margin = new Padding(8, 0, 8, 8),
                    Padding = new Padding(12, 0, 0, 0),
                    Cursor = Cursors.Hand,
                    Tag = categoryKey          // 改存分類 Key
                };

                UpdateButtonRoundedCorners(btn);
                _buttonIconPairs[btn] = (redIcon, whiteIcon);

                if (this.flpCategory.Controls.Count == 0)
                {
                    ApplySelectedStyle(btn);
                    _selectedCategoryButton = btn;
                }

                btn.Click += (s, e) =>
                {
                    if (_selectedCategoryButton != null && _selectedCategoryButton != btn)
                        ApplyNormalStyle(_selectedCategoryButton);

                    ApplySelectedStyle(btn);
                    _selectedCategoryButton = btn;

                    // 記錄選取的分類，並重新篩選商品
                    _selectedCategoryKey = btn.Tag as string;
                    ApplyFilters();
                };

                btn.Width = this.flpCategory.ClientSize.Width
                            - this.flpCategory.Padding.Horizontal
                            - btn.Margin.Horizontal;

                this.flpCategory.Controls.Add(btn);
            }
        }

        private void UpdateButtonRoundedCorners(Button btn)
        {
            int radius = 12;
            var path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius - 1, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius - 1, btn.Height - radius - 1, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius - 1, radius, radius, 90, 90);
            path.CloseAllFigures();
            btn.Region = new Region(path);
        }

        private void ApplySelectedStyle(Button btn)
        {
            btn.BackColor = Color.FromArgb(183, 29, 37); // 紅色選取
            btn.ForeColor = Color.White;

            // 切換成白色版圖示
            if (_buttonIconPairs.TryGetValue(btn, out var icons) && icons.White != null)
            {
                btn.Image = icons.White;
            }
        }

        private void ApplyNormalStyle(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(60, 60, 60);

            // 切換回紅色版圖示
            if (_buttonIconPairs.TryGetValue(btn, out var icons) && icons.Red != null)
            {
                btn.Image = icons.Red;
            }
        }


        /// <summary>
        /// 依分類 + 搜尋關鍵字 + 排序方式，篩選商品並重新產生卡片
        /// </summary>
        private void ApplyFilters()
        {
            if (_allProducts == null) return;

            IEnumerable<Product> result = _allProducts;

            // --- 1. 分類篩選 ---
            if (_selectedCategoryKey == "popular")
            {
                result = result.Where(p => p.IsPopular);
            }
            else if (_selectedCategoryKey != "all")
            {
                result = result.Where(p => p.CategoryKey == _selectedCategoryKey);
            }

            // --- 2. 搜尋關鍵字（中文名、印尼文名、描述都比對）---
            if (!string.IsNullOrWhiteSpace(_searchKeyword))
            {
                string keyword = _searchKeyword;

                result = result.Where(p =>
                    (p.NameZh != null && p.NameZh.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.NameId != null && p.NameId.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.Description != null && p.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // --- 3. 排序 ---
            switch (cboSort?.SelectedIndex ?? 0)
            {
                case 1:  // 價格由低到高
                    result = result.OrderBy(p => p.Price).ThenBy(p => p.ProductId);
                    break;

                case 2:  // 價格由高到低
                    result = result.OrderByDescending(p => p.Price).ThenBy(p => p.ProductId);
                    break;

                case 3:  // 評分最高
                    result = result.OrderByDescending(p => p.Rating).ThenBy(p => p.ProductId);
                    break;

                default: // 熱門排序：熱門商品優先，其餘維持原順序
                    result = result.OrderByDescending(p => p.IsPopular).ThenBy(p => p.ProductId);
                    break;
            }

            RenderProducts(result.ToList());
        }



        /// <summary>
        /// 建立 pnlProductArea 內部的三列版面：Banner / 搜尋列 / 商品列表
        /// 用 TableLayoutPanel 取代原本 Dock=Top 的做法，避免 Docking 順序造成重疊
        /// </summary>
        private void SetupProductAreaLayout()
        {
            tlpProductArea = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };

            // 第一列：Banner（高度依圖片比例動態計算，先給預設值）
            tlpProductArea.RowStyles.Add(new RowStyle(SizeType.Absolute, 180));
            // 第二列：搜尋列（固定高度）
            tlpProductArea.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            // 第三列：商品列表（佔滿剩餘空間）
            tlpProductArea.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tlpProductArea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            this.pnlProductArea.Controls.Add(tlpProductArea);

            SetupBanner();
            SetupSearchBar();
            SetupProductList();

            // 新增：先把三個 Panel 加入 TableLayoutPanel，SetRow 才會生效
            tlpProductArea.Controls.Add(pnlBanner);
            tlpProductArea.Controls.Add(pnlSearchBar);
            tlpProductArea.Controls.Add(pnlProductList);

            tlpProductArea.SetRow(pnlBanner, 0);
            tlpProductArea.SetRow(pnlSearchBar, 1);
            tlpProductArea.SetRow(pnlProductList, 2);

            // Banner 高度要依實際寬度動態調整，維持圖片原始比例，不變形
            tlpProductArea.Resize += (s, e) => UpdateBannerRowHeight();
        }

        private void UpdateBannerRowHeight()
        {
            if (tlpProductArea.RowStyles.Count == 0) return;

            int newHeight = (int)(tlpProductArea.Width * _bannerAspectRatio);

            // 限制最大/最小高度，避免視窗過寬或過窄時 Banner 比例失控
            newHeight = Math.Max(120, Math.Min(newHeight, 260));

            tlpProductArea.RowStyles[0].Height = newHeight;
        }



        private void SetupBanner()
        {
            pnlBanner = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 16),
                BackColor = Color.FromArgb(90, 40, 20) // 圖片載入失敗時的備用底色
            };

            string projectImagesFolder = Path.Combine(
                Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName,
                "Assets", "Images");

            string bannerImagePath = Path.Combine(projectImagesFolder, "banner_nasi_goreng.png");

            if (File.Exists(bannerImagePath))
            {
                var img = Image.FromFile(bannerImagePath);
                pnlBanner.BackgroundImage = img;
                pnlBanner.BackgroundImageLayout = ImageLayout.Stretch;

                // 依實際圖片尺寸計算比例（高 ÷ 寬），讓 Banner 高度自動貼合圖片比例
                _bannerAspectRatio = (float)img.Height / img.Width;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"找不到 Banner 圖片：{bannerImagePath}");
            }

            pnlBanner.Resize += (s, e) => UpdatePanelRoundedCorners(pnlBanner, 16);
            UpdatePanelRoundedCorners(pnlBanner, 16);

            // 圖片載入完成後，立刻依照比例重新計算一次 Banner 高度
            UpdateBannerRowHeight();
        }


        private bool _isSearchPlaceholder = true;   // 目前顯示的是否為提示文字
        private const string SearchPlaceholder = "搜尋菜色...";

        private void SetupSearchBar()
        {
            pnlSearchBar = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 12, 0, 12),
                BackColor = Color.Transparent
            };

            // --- 左側：搜尋輸入框 ---
            var pnlSearchBox = new Panel
            {
                BackColor = Color.White,
                Location = new Point(0, 0)
            };

            const int iconSize = 24;   // 搜尋框高 48，icon 取 24 視覺上最協調

            var picSearchIcon = new PictureBox
            {
                Size = new Size(iconSize, iconSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Image = LoadSearchIcon(iconSize)
            };

            txtSearch = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("微軟正黑體", 12),
                ForeColor = Color.Gray,
                Text = SearchPlaceholder
            };


            txtSearch.GotFocus += (s, e) =>
            {
                if (_isSearchPlaceholder)
                {
                    _suppressSearchEvent = true;        // 開始程式改動
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.FromArgb(60, 60, 60);
                    _isSearchPlaceholder = false;
                    _suppressSearchEvent = false;       // 結束
                }
            };

            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    _suppressSearchEvent = true;
                    txtSearch.Text = SearchPlaceholder;
                    txtSearch.ForeColor = Color.Gray;
                    _isSearchPlaceholder = true;
                    _suppressSearchEvent = false;
                }
            };

            // 邊打字邊即時篩選
            txtSearch.TextChanged += (s, e) =>
            {
                if (_suppressSearchEvent) return;       // 程式自己改的，不處理
                if (_isSearchPlaceholder) return;       // 目前顯示的是提示文字，不是真的輸入

                _searchKeyword = txtSearch.Text.Trim();
                ApplyFilters();
            };

            pnlSearchBox.Controls.Add(picSearchIcon);
            pnlSearchBox.Controls.Add(txtSearch);

            // 確保輸入框在 icon 之上，避免文字或游標被 icon 遮住
            txtSearch.BringToFront();

            // --- 右側：排序下拉選單 ---
            cboSort = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("微軟正黑體", 11),
                Width = 180,
                FlatStyle = FlatStyle.Flat
            };
            cboSort.Items.AddRange(new[] { "熱門排序", "價格由低到高", "價格由高到低", "評分最高" });
            cboSort.SelectedIndex = 0;

            // 註冊在設定 SelectedIndex 之後，避免初始化時就觸發一次篩選
            cboSort.SelectedIndexChanged += (s, e) => ApplyFilters();

            // --- 排版：搜尋框佔 65%，排序選單靠右對齊 ---
            void LayoutSearchBar()
            {
                if (pnlSearchBar.Width <= 0 || pnlSearchBar.Height <= 0) return;

                int barWidth = pnlSearchBar.Width;
                int barHeight = pnlSearchBar.Height;

                int boxWidth = (int)(barWidth * 0.65f);
                boxWidth = Math.Max(240, Math.Min(boxWidth, 720));

                pnlSearchBox.Size = new Size(boxWidth, barHeight);
                pnlSearchBox.Location = new Point(0, 0);

                // icon 靠左 16px、垂直置中
                int iconLeft = 16;
                picSearchIcon.Location = new Point(
                    iconLeft,
                    Math.Max(0, (barHeight - picSearchIcon.Height) / 2));

                // 文字接在 icon 右緣後面，中間留 12px，不再寫死座標
                int textLeft = picSearchIcon.Right + 12;
                txtSearch.Location = new Point(
                    textLeft,
                    Math.Max(0, (barHeight - txtSearch.Height) / 2));

                txtSearch.Width = Math.Max(50, boxWidth - textLeft - 16);

                cboSort.Location = new Point(
                    barWidth - cboSort.Width,
                    Math.Max(0, (barHeight - cboSort.Height) / 2));

                UpdatePanelRoundedCorners(pnlSearchBox, 12);
            }

            pnlSearchBar.Resize += (s, e) => LayoutSearchBar();

            pnlSearchBar.Controls.Add(pnlSearchBox);
            pnlSearchBar.Controls.Add(cboSort);

            LayoutSearchBar();
        }


        /// <summary>
        /// 通用版圓角設定，適用於任何 Control（Panel、Button 等）
        /// </summary>
        private void UpdatePanelRoundedCorners(Control ctrl, int radius)
        {
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;

            var path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(ctrl.Width - radius - 1, 0, radius, radius, 270, 90);
            path.AddArc(ctrl.Width - radius - 1, ctrl.Height - radius - 1, radius, radius, 0, 90);
            path.AddArc(0, ctrl.Height - radius - 1, radius, radius, 90, 90);
            path.CloseAllFigures();
            ctrl.Region = new Region(path);
        }


        /// <summary>
        /// 診斷版面：印出各容器的父層與實際座標，用來確認 pnlProductArea 是否真的在 tblMain 的儲存格內
        /// </summary>
        private void PrintDebugLayout()
        {
            System.Diagnostics.Debug.WriteLine("=== 版面診斷 ===");
            System.Diagnostics.Debug.WriteLine($"Form 大小: {this.ClientSize}");
            System.Diagnostics.Debug.WriteLine($"tblMain        父層={tblMain.Parent?.Name}, Dock={tblMain.Dock}, Bounds={tblMain.Bounds}");
            System.Diagnostics.Debug.WriteLine($"pnlHeader      父層={pnlHeader.Parent?.Name}, Dock={pnlHeader.Dock}, Bounds={pnlHeader.Bounds}");
            System.Diagnostics.Debug.WriteLine($"flpCategory    父層={flpCategory.Parent?.Name}, Dock={flpCategory.Dock}, Bounds={flpCategory.Bounds}");
            System.Diagnostics.Debug.WriteLine($"pnlProductArea 父層={pnlProductArea.Parent?.Name}, Dock={pnlProductArea.Dock}, Bounds={pnlProductArea.Bounds}");

            if (pnlProductArea.Parent == tblMain)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"pnlProductArea 在 tblMain 的 欄={tblMain.GetColumn(pnlProductArea)}, 列={tblMain.GetRow(pnlProductArea)}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("⚠️ pnlProductArea 不在 tblMain 內！這就是重疊的原因");
            }
            System.Diagnostics.Debug.WriteLine("================");
        }

        /// <summary>
        /// 強制修正主版面：確保 pnlHeader / flpCategory / pnlProductArea 各自待在 tblMain 的儲存格內、且 Dock=Fill
        /// 避免任何一個區塊超出邊界去跟其他區塊重疊
        /// </summary>
        private void FixMainLayout()
        {
            this.SuspendLayout();
            tblMain.SuspendLayout();

            // 主表格填滿整個 Form
            tblMain.Dock = DockStyle.Fill;
            tblMain.Margin = Padding.Empty;
            tblMain.Padding = Padding.Empty;

            // 每個區塊都必須 Dock=Fill，讓 TableLayoutPanel 完全掌控位置，不保留設計時的絕對座標
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = Padding.Empty;

            flpCategory.Dock = DockStyle.Fill;

            pnlProductArea.Dock = DockStyle.Fill;
            pnlProductArea.Margin = new Padding(0, 0, 12, 0);   // 保留與購物車的間距
            pnlProductArea.Padding = new Padding(12, 12, 12, 12); // 中間區塊與左右欄的內距

            // 順便把 pnlCart 也納入管理，避免之後出現同樣的錯位問題
            pnlCart.Dock = DockStyle.Fill;
            pnlCart.Margin = new Padding(0, 12, 13, 12);

            tblMain.ResumeLayout(true);
            this.ResumeLayout(true);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // 移除預設焦點，避免啟動時 txtSearch 的提示文字被 GotFocus 清空
            this.ActiveControl = null;
            UpdateBannerRowHeight();
        }

        private List<Product> GetSampleProducts()
        {
            return new List<Product>
    {
        new Product { ProductId = 1, NameZh = "印尼炒飯", NameId = "Nasi Goreng",
                      Description = "印尼經典炒飯，香氣濃郁",
                      LongDescription = "印尼經典炒飯，香氣濃郁\r\n搭配蝦餅，荷包蛋與炸蔥，風味十足。",
                      Price = 120, Rating = 5.0, IsPopular = true, CategoryKey = "rice",
                      HasSpiceOption = true,
                      ImageFileName = "product_nasi_goreng.png" },

        new Product { ProductId = 2, NameZh = "沙嗲串燒", NameId = "Sate",
                      Description = "炭烤雞肉串，搭配花生醬",
                      LongDescription = "炭烤雞肉串，搭配花生醬\r\n炙烤香氣濃郁，沾醬香甜微辛。",
                      Price = 150, Rating = 5.0, IsPopular = true, CategoryKey = "plate",
                      HasSpiceOption = true,
                      ImageFileName = "product_sate.png" },

        new Product { ProductId = 3, NameZh = "仁當牛肉", NameId = "Rendang",
                      Description = "印尼傳統燉牛肉，香辣濃郁",
                      LongDescription = "印尼傳統燉牛肉，香辣濃郁\r\n慢燉數小時，肉質軟嫩入味。",
                      Price = 180, Rating = 5.0, IsPopular = true, CategoryKey = "plate",
                      HasSpiceOption = true,
                      ImageFileName = "product_rendang.png" },

        new Product { ProductId = 4, NameZh = "雞肉湯", NameId = "Soto Ayam",
                      Description = "香料雞湯，清爽開胃",
                      LongDescription = "香料雞湯，清爽開胃\r\n以薑黃與香茅慢熬，湯頭清甜。",
                      Price = 120, Rating = 5.0, IsPopular = false, CategoryKey = "plate",
                      HasSpiceOption = false,
                      ImageFileName = "product_soto_ayam.png" },

        new Product { ProductId = 5, NameZh = "天貝炸物", NameId = "Tempe Goreng",
                      Description = "印尼傳統發酵豆餅",
                      LongDescription = "印尼傳統發酵豆餅\r\n外酥內軟，香氣質樸有嚼勁。",
                      Price = 80, Rating = 5.0, IsPopular = false, CategoryKey = "fries",
                      HasSpiceOption = false,
                      ImageFileName = "product_tempe_goreng.png" },

        new Product { ProductId = 6, NameZh = "千層糕", NameId = "Kue Lapis",
                      Description = "印尼傳統千層糕",
                      LongDescription = "印尼傳統千層糕\r\n層層分明，口感軟糯帶椰香。",
                      Price = 60, Rating = 5.0, IsPopular = false, CategoryKey = "cake",
                      HasSpiceOption = false,
                      ImageFileName = "product_kue_lapis.png" },
         new Product { ProductId = 7, NameZh = "珍多冰", NameId = "Es Cendol",
                        Description = "椰奶椰糖，消暑冰品",
                        LongDescription = "椰奶椰糖，消暑冰品\r\n加入綠色米粉條，口感 Q 彈冰涼。",
                        Price = 70, Rating = 5.0, IsPopular = false, CategoryKey = "drink",
                        HasSpiceOption = false,
                        HasIceOption = true,
                        HasSweetnessOption = true,
                        ImageFileName = "product_es_cendol.png" },

        new Product { ProductId = 8, NameZh = "酪梨牛奶", NameId = "Jus Alpukat",
                      Description = "濃醇酪梨，香甜滑順",
                       LongDescription = "濃醇酪梨，香甜滑順\r\n淋上巧克力醬，是印尼經典飲品。",
                       Price = 90, Rating = 5.0, IsPopular = true, CategoryKey = "drink",
                       HasSpiceOption = false,
                       HasIceOption = true,
                       HasSweetnessOption = true,
                       ImageFileName = "product_jus_alpukat.png" },
            };
        }


        /// <summary>
        /// 建立商品列表區域：外層 Panel + 內部 FlowLayoutPanel（自動換行、可捲動）
        /// </summary>
        private void SetupProductList()
        {
            pnlProductList = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            flpProducts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,              // 卡片超出高度時出現垂直捲軸
                WrapContents = true,            // 一列排不下時自動換到下一列
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 0, 8, 0),   // 右側留空給捲軸，避免卡片被壓到
                BackColor = Color.Transparent
            };

            pnlProductList.Controls.Add(flpProducts);

            // 載入假資料並產生卡片
            _allProducts = GetSampleProducts();
            //RenderProducts(_allProducts);
            ApplyFilters(); 
        }

        /// <summary>
        /// 依傳入的商品清單重新產生卡片
        /// 之後分類篩選、搜尋、排序都是「整理好清單後呼叫這個方法」即可
        /// </summary>

        private void RenderProducts(List<Product> products)
        {
            if (flpProducts == null) return;

            flpProducts.SuspendLayout();

            foreach (Control ctrl in flpProducts.Controls.OfType<Control>().ToList())
            {
                flpProducts.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            if (products == null || products.Count == 0)
            {
                // 查無商品時顯示提示文字
                //var lblEmpty = new Label
                //{
                //    Text = "此分類目前沒有商品",
                //    Font = new Font("微軟正黑體", 14),
                //    ForeColor = Color.FromArgb(150, 140, 130),
                //    AutoSize = true,
                //    Margin = new Padding(24, 40, 0, 0)
                //};
                //flpProducts.Controls.Add(lblEmpty);


                // 依照是「搜尋沒結果」還是「分類沒商品」顯示不同訊息
                string message = string.IsNullOrWhiteSpace(_searchKeyword)
                    ? "此分類目前沒有商品"
                    : $"找不到符合「{_searchKeyword}」的菜色";

                var lblEmpty = new Label
                {
                    Text = message,
                    Font = new Font("微軟正黑體", 14),
                    ForeColor = Color.FromArgb(150, 140, 130),
                    AutoSize = true,
                    Margin = new Padding(24, 40, 0, 0)
                };
                flpProducts.Controls.Add(lblEmpty);
            }
            else
            {
                foreach (var product in products)
                {
                    var card = new ProductCard(product)
                    {
                        Margin = new Padding(0, 0, 16, 16)
                    };

                    card.AddToCartClicked += Card_AddToCartClicked;
                    flpProducts.Controls.Add(card);
                }
            }

            flpProducts.ResumeLayout(true);
        }


        private void Card_AddToCartClicked(object sender, Product product)
        {
            ShowAddToCartDialog(product, null);
        }


        /// <summary>
        /// 自動裁掉圖片四周的透明邊距，只保留實際有內容的區域
        /// </summary>
        private Image TrimTransparentBorder(Bitmap source, int alphaThreshold = 8)
        {
            var full = new Rectangle(0, 0, source.Width, source.Height);

            BitmapData data = source.LockBits(full, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int stride = data.Stride;
            byte[] buffer = new byte[stride * source.Height];
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            source.UnlockBits(data);

            int minX = source.Width, minY = source.Height, maxX = -1, maxY = -1;

            for (int y = 0; y < source.Height; y++)
            {
                int rowStart = y * stride;

                for (int x = 0; x < source.Width; x++)
                {
                    byte alpha = buffer[rowStart + x * 4 + 3];   // BGRA 的 A

                    if (alpha > alphaThreshold)
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // 整張都是透明（或判斷失敗）時，回傳原圖
            if (maxX < minX || maxY < minY)
                return new Bitmap(source);

            var content = Rectangle.FromLTRB(minX, minY, maxX + 1, maxY + 1);

            System.Diagnostics.Debug.WriteLine(
                $"[Logo 裁邊] 原始 {source.Width}×{source.Height} → 實際內容 {content.Width}×{content.Height}");

            return source.Clone(content, PixelFormat.Format32bppArgb);
        }


        /// <summary>
        /// 建立上方 Header：左側 Logo（之後再加使用者資訊與購物車徽章）
        /// </summary>
        private void SetupHeader()
        {
            pnlHeader.BackColor = AppColors.HeaderBgColor;

            var picLogo = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            string logoPath = Path.Combine(GetAssetsFolder("Images"), "logo_indotaste.png");

            if (File.Exists(logoPath))
            {
                //讀進記憶體後立刻釋放檔案，避免檔案被鎖住

                using (var fs = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                using (var temp = Image.FromStream(fs))
                using (var bmp = new Bitmap(temp))
                {
                    // 裁掉四周透明邊距，讓 Logo 能填滿顯示區域
                    picLogo.Image = TrimTransparentBorder(bmp);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"找不到 Logo 圖片：{logoPath}");

                // 找不到圖片時用文字替代，方便先確認版面
                picLogo.Dispose();

                var lblLogo = new Label
                {
                    Text = "IndoTaste",
                    Font = new Font("微軟正黑體", 25, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Location = new Point(28, 20)
                };

                pnlHeader.Controls.Add(lblLogo);
                return;
            }

            pnlHeader.Controls.Add(picLogo);

            // 依 Header 實際高度與圖片比例，計算 Logo 尺寸並垂直置中
            void LayoutHeader()
            {
                if (pnlHeader.ClientSize.Height <= 0 || picLogo.Image == null) return;

                const int leftPadding = 28;   // 與左邊界的距離
                //const int verticalPadding = 16; // 上下各留多少空白
                const int verticalPadding = 10;   // 原本 16，改小讓 Logo 更大

                int logoHeight = pnlHeader.ClientSize.Height - verticalPadding * 2;
                if (logoHeight <= 0) return;

                // 依圖片原始比例算出對應寬度，不會變形
                float ratio = (float)picLogo.Image.Width / picLogo.Image.Height;
                int logoWidth = (int)(logoHeight * ratio);

                picLogo.Size = new Size(logoWidth, logoHeight);
                picLogo.Location = new Point(leftPadding, verticalPadding);
            }

            pnlHeader.Resize += (s, e) => LayoutHeader();
            LayoutHeader();
        }



        //#region 購物車區塊
        // SetupCartArea、CreateCartHeader、RefreshCart... 全部放這裡
        /// <summary>
        /// 建立右側購物車區塊：標題列 / 項目清單 / 結算區
        /// </summary>
        private void SetupCartArea()
        {
            pnlCart.BackColor = Color.FromArgb(252, 245, 234);
            UpdatePanelRoundedCorners(pnlCart, 16);
            pnlCart.Resize += (s, e) => UpdatePanelRoundedCorners(pnlCart, 16);

            tlpCart = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent,
                Padding = new Padding(16, 16, 16, 16)
            };

            tlpCart.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));    // 標題
            tlpCart.RowStyles.Add(new RowStyle(SizeType.Percent, 100));    // 項目清單
            tlpCart.RowStyles.Add(new RowStyle(SizeType.Absolute, 170));   // 結算區
            tlpCart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            tlpCart.Controls.Add(CreateCartHeader(), 0, 0);
            tlpCart.Controls.Add(CreateCartItemList(), 0, 1);
            tlpCart.Controls.Add(CreateCartFooter(), 0, 2);

            pnlCart.Controls.Add(tlpCart);

            // 購物車一有變動就自動重畫
            _cart.Changed += (s, e) => RefreshCart();

            RefreshCart();
        }

        private Control CreateCartHeader()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var picIcon = new PictureBox
            {
                Size = new Size(28, 28),
                Location = new Point(0, 12),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Image = LoadCartIcon("icon_cart_black", 28)
            };

            lblCartTitle = new Label
            {
                Text = "購物車",
                Font = new Font("微軟正黑體", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 50, 45),
                AutoSize = true,
                Location = new Point(38, 12)
            };

            panel.Controls.Add(picIcon);
            panel.Controls.Add(lblCartTitle);

            return panel;
        }

        private Control CreateCartItemList()
        {
            flpCartItems = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 8, 0, 8)
            };

            return flpCartItems;
        }

        private Control CreateCartFooter()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            // --- 總金額區塊（白底圓角）---
            var pnlTotal = new Panel
            {
                BackColor = Color.White,
                Location = new Point(0, 0),
                Height = 64
            };
            pnlTotal.Resize += (s, e) => UpdatePanelRoundedCorners(pnlTotal, 12);

            lblTotalLabel = new Label
            {
                Text = "總金額 (0 項商品)",
                Font = new Font("微軟正黑體", 11),
                ForeColor = Color.FromArgb(90, 80, 70),
                AutoSize = true,
                Location = new Point(16, 20)
            };

            lblTotalAmount = new Label
            {
                Text = "NT$ 0",
                Font = new Font("微軟正黑體", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 50, 45),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Size = new Size(120, 26),
                Location = new Point(0, 20)
            };

            pnlTotal.Controls.Add(lblTotalLabel);
            pnlTotal.Controls.Add(lblTotalAmount);


            // --- 兩顆按鈕 ---
            // 清空購物車按鈕
            btnClearCart = new Button
            {
                Text = "清空購物車",
                Font = new Font("微軟正黑體", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,        // 預設黑字
                Height = 52,
                Cursor = Cursors.Hand
            };
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.Resize += (s, e) => UiHelper.ApplyRoundedRegion(btnClearCart, 10);

            // hover：紅底白字
            btnClearCart.MouseEnter += (s, e) =>
            {
                btnClearCart.BackColor = Color.FromArgb(183, 29, 37);
                btnClearCart.ForeColor = Color.White;
            };
            btnClearCart.MouseLeave += (s, e) =>
            {
                btnClearCart.BackColor = Color.White;
                btnClearCart.ForeColor = Color.Black;
            };

            // 前往結帳按鈕
            btnCheckout = new Button
            {
                Text = "前往結帳",
                Font = new Font("微軟正黑體", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(183, 29, 37),   // 預設紅底
                ForeColor = Color.White,                    // 預設白字
                Height = 52,
                Cursor = Cursors.Hand
            };
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Resize += (s, e) => UiHelper.ApplyRoundedRegion(btnCheckout, 10);

            // hover：白底黑字（跟預設狀態對調）
            btnCheckout.MouseEnter += (s, e) =>
            {
                btnCheckout.BackColor = Color.White;
                btnCheckout.ForeColor = Color.Black;
            };
            btnCheckout.MouseLeave += (s, e) =>
            {
                btnCheckout.BackColor = Color.FromArgb(183, 29, 37);
                btnCheckout.ForeColor = Color.White;
            };

            btnClearCart.Click += BtnClearCart_Click;
            btnCheckout.Click += BtnCheckout_Click;

            panel.Controls.Add(pnlTotal);
            panel.Controls.Add(btnClearCart);
            panel.Controls.Add(btnCheckout);

            // 依實際寬度排版
            void LayoutFooter()
            {
                int w = panel.ClientSize.Width;
                if (w <= 0) return;

                pnlTotal.Size = new Size(w, 64);
                lblTotalAmount.Left = w - lblTotalAmount.Width - 16;
                UpdatePanelRoundedCorners(pnlTotal, 12);

                int gap = 10;
                int btnWidth = (w - gap) / 2;

                btnClearCart.Location = new Point(0, 84);
                btnClearCart.Width = btnWidth;

                btnCheckout.Location = new Point(btnWidth + gap, 84);
                btnCheckout.Width = w - btnWidth - gap;
            }

            panel.Resize += (s, e) => LayoutFooter();
            LayoutFooter();

            return panel;
        }

        private Image LoadCartIcon(string iconName, int size)
        {
            string path = Path.Combine(GetAssetsFolder("Icons"), iconName + ".png");
            if (!File.Exists(path)) return null;

            try
            {
                using (var original = new Bitmap(path))
                    return new Bitmap(original, new Size(size, size));
            }
            catch { return null; }
        }


        /// <summary>
        /// 依購物車目前內容重畫項目列表與總金額
        /// </summary>
        private void RefreshCart()
        {
            if (flpCartItems == null) return;

            flpCartItems.SuspendLayout();

            foreach (Control ctrl in flpCartItems.Controls.OfType<Control>().ToList())
            {
                flpCartItems.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            if (_cart.IsEmpty)
            {
                flpCartItems.Controls.Add(new Label
                {
                    Text = "購物車是空的",
                    Font = new Font("微軟正黑體", 11),
                    ForeColor = Color.FromArgb(160, 150, 140),
                    AutoSize = true,
                    Margin = new Padding(8, 24, 0, 0)
                });
            }
            else
            {
                int rowWidth = flpCartItems.ClientSize.Width - 8;

                foreach (var item in _cart.Items)
                {
                    var row = new CartItemRow(item)
                    {
                        Width = Math.Max(280, rowWidth),
                        Margin = new Padding(0, 0, 0, 10)
                    };

                    row.EditRequested += CartRow_EditRequested;
                    row.RemoveRequested += CartRow_RemoveRequested;

                    flpCartItems.Controls.Add(row);
                }
            }

            flpCartItems.ResumeLayout(true);

            // 更新結算區
            lblTotalLabel.Text = $"總金額 ({_cart.ItemCount} 項商品)";
            lblTotalAmount.Text = $"NT$ {_cart.TotalAmount:0}";

            // 按鈕啟用/停用
            btnClearCart.Enabled = !_cart.IsEmpty;
            btnCheckout.Enabled = !_cart.IsEmpty;
            btnCheckout.BackColor = _cart.IsEmpty
                ? Color.FromArgb(200, 190, 180)     // 停用時的灰色
                : Color.FromArgb(183, 29, 37);      // 可用時的紅色
        }

        /// <summary>鉛筆：重新開啟彈窗並帶入原本的選項</summary>
        private void CartRow_EditRequested(object sender, CartItem item)
        {
            if (item == null) return;

            ShowAddToCartDialog(item.Product, item);
        }

        /// <summary>垃圾桶：確認後移除</summary>
        private void CartRow_RemoveRequested(object sender, CartItem item)
        {
            if (item == null) return;

            var result = MessageBox.Show(
                $"確定要移除「{item.Product.NameZh}」嗎？",
                "移除商品",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                _cart.Remove(item);
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.IsEmpty) return;

            var result = MessageBox.Show(
                "確定要清空購物車嗎？",
                "清空購物車",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                _cart.Clear();
        }

        //private void BtnCheckout_Click(object sender, EventArgs e)
        //{
        //    if (_cart.IsEmpty) return;

        //    // TODO: 之後接結帳流程
        //    MessageBox.Show(
        //        $"共 {_cart.ItemCount} 項商品，總金額 NT$ {_cart.TotalAmount:0}",
        //        "前往結帳");
        //}

        // / <summary>
        /// 前往結帳：開啟 FormCheckout 彈窗，選擇付款方式後建立訂單
        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.IsEmpty) return;

            // 半透明遮罩
            using (var overlay = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                Opacity = 0.45,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Bounds = this.Bounds
            })
            {
                overlay.Show(this);

                using (var dialog = new FormCheckout(_cart))
                {
                    var result = dialog.ShowDialog(overlay);
                    overlay.Close();

                    if (result != DialogResult.OK) return;   // 返回修改：購物車保持原狀

                    // 建立訂單（存入全域 OrderService）
                    var order = OrderService.Instance.CreateOrder(_cart, dialog.SelectedPaymentMethod);

                    if (order == null)
                    {
                        MessageBox.Show("訂單建立失敗，請重試。", "錯誤");
                        return;
                    }

                    // 訂單成立後清空購物車（Changed 事件會自動重畫右側區塊）
                    _cart.Clear();

                    MessageBox.Show(
                        $"訂單成立！\r\n\r\n" +
                        $"訂單編號：{order.OrderNumber}\r\n" +
                        $"付款方式：{order.PaymentMethod}\r\n" +
                        $"共 {order.ItemCount} 項商品、{order.TotalQuantity} 件\r\n" +
                        $"總金額：NT$ {order.TotalAmount:0}",
                        "結帳完成",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    System.Diagnostics.Debug.WriteLine(
                        $"[訂單] {order.OrderNumber} 已建立，目前共 {OrderService.Instance.Orders.Count} 筆訂單");
                }
            }
        }

        /// <summary>
        /// 開啟加入購物車彈窗（editingItem 為 null 代表新增，否則為編輯）
        /// </summary>
        private void ShowAddToCartDialog(Product product, CartItem editingItem)
        {
            using (var overlay = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                Opacity = 0.45,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Bounds = this.Bounds
            })
            {
                overlay.Show(this);

                var dialog = editingItem != null
                    ? new FormAddToCart(editingItem)
                    : new FormAddToCart(product);

                using (dialog)
                {
                    var result = dialog.ShowDialog(overlay);
                    overlay.Close();

                    if (result != DialogResult.OK) return;

                    if (editingItem != null)
                    {
                        _cart.UpdateItem(editingItem,
                            dialog.SelectedQuantity,
                            dialog.SelectedSpiceLevel,
                            dialog.SelectedIceLevel,
                            dialog.SelectedSweetness,
                            dialog.SelectedDiningType);
                    }
                    else
                    {
                        _cart.Add(product,
                            dialog.SelectedQuantity,
                            dialog.SelectedSpiceLevel,
                            dialog.SelectedIceLevel,
                            dialog.SelectedSweetness,
                            dialog.SelectedDiningType);
                    }
                }
            }
        }

        //#endregion


        private void tblMain_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
