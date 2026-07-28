using IndoTaste.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndoTaste.Models;
using IndoTaste.Forms.Customer.Controls;

namespace IndoTaste.Forms.Customer
{
    public partial class FormCustomerOrder : Form
    {
        // 按鈕UI 元件變數
        private Button _selectedCategoryButton;
        private Dictionary<Button, Image> _buttonImages = new Dictionary<Button, Image>();

        // UI 元件變數
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
            //pnlHeader.BackColor = Color.FromArgb(77, 18, 8);
            pnlHeader.BackColor = AppColors.HeaderBgColor;

            // 設定 pnlProductArea 背景色
            pnlProductArea.BackColor = Color.FromArgb(248, 244, 236);

            SetCategoryPanelStyle();


            // 新增：設定 FlowLayoutPanel 屬性
            //this.flpCategory.AutoSize = false;
            //this.flpCategory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //this.flpCategory.WrapContents = false;

            //// 讓 flpCategory 與視窗左側有距離 (可改數值)
            //this.flpCategory.Margin = new Padding(12, 12, 0, 0);   // 在 table cell 中產生左側空白
            //this.flpCategory.Padding = new Padding(8);               // 控制欄內按鈕與邊界的內距
            //this.flpCategory.FlowDirection = FlowDirection.TopDown;
            //this.flpCategory.WrapContents = false;


            // 建立按鈕
            CreateCategoryButtons();

            // 調試：顯示載入資訊
            PrintDebugIconPaths();


            this.flpCategory.Resize += FlpCategory_Resize;


            // 改用 TableLayoutPanel 統一管理 Banner / 搜尋列 / 商品列表 三個區塊
            SetupProductAreaLayout();



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

        private readonly string[] categoryNames =
        {
            "全部",
            "熱門排行榜",
            "主食",
            "主菜",
            "蔬食",
            "炸物",
            "甜點",
            "飲料"
        };


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
            pnlProductArea.Margin = Padding.Empty;
            pnlProductArea.Padding = new Padding(12, 12, 12, 12); // 中間區塊與左右欄的內距

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
                      Description = "印尼經典炒飯，香氣濃郁", Price = 120, Rating = 5.0,
                      IsPopular = true, CategoryKey = "rice",
                      ImageFileName = "product_nasi_goreng.png" },

        new Product { ProductId = 2, NameZh = "沙嗲串燒", NameId = "Sate",
                      Description = "炭烤雞肉串，搭配花生醬", Price = 150, Rating = 5.0,
                      IsPopular = true, CategoryKey = "plate",
                      ImageFileName = "product_sate.png" },

        new Product { ProductId = 3, NameZh = "仁當牛肉", NameId = "Rendang",
                      Description = "印尼傳統燉牛肉，香辣濃郁", Price = 180, Rating = 5.0,
                      IsPopular = true, CategoryKey = "plate",
                      ImageFileName = "product_rendang.png" },

        new Product { ProductId = 4, NameZh = "雞肉湯", NameId = "Soto Ayam",
                      Description = "香料雞湯，清爽開胃", Price = 120, Rating = 5.0,
                      IsPopular = false, CategoryKey = "plate",
                      ImageFileName = "product_soto_ayam.png" },

        new Product { ProductId = 5, NameZh = "天貝炸物", NameId = "Tempe Goreng",
                      Description = "印尼傳統發酵豆餅", Price = 80, Rating = 5.0,
                      IsPopular = false, CategoryKey = "fries",
                      ImageFileName = "product_tempe_goreng.png" },

        new Product { ProductId = 6, NameZh = "千層糕", NameId = "Kue Lapis",
                      Description = "印尼傳統千層糕", Price = 60, Rating = 5.0,
                      IsPopular = false, CategoryKey = "cake",
                      ImageFileName = "product_kue_lapis.png" },
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

        /// <summary>
        /// 任何一張卡片按下「加入購物車」都會走到這裡
        /// </summary>
        private void Card_AddToCartClicked(object sender, Product product)
        {
            // TODO: 之後改成實際加入購物車的邏輯（右側購物車區塊）
            MessageBox.Show($"加入購物車：{product.DisplayName}　NT$ {product.Price:0}");
        }

        private void tblMain_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
