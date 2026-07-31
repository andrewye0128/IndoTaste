using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndoTaste.Helpers;
using IndoTaste.Models;
using System.IO;

namespace IndoTaste.Forms.Customer
{
    public partial class FormAddToCart : Form
    {
        private readonly Product _product;
        private readonly CartItem _editingItem;   // 編輯模式時帶入的項目，新增模式為 null

        /// <summary>目前是否為編輯模式</summary>
        public bool IsEditMode => _editingItem != null;
        private Image _productImage;

        private int _quantity = 1;
        private Button _selectedSpiceButton;
        private Button _selectedDiningButton;

        // 辣度選項（無辣為預設）
        //private static readonly string[] SpiceLevels =
        //    { "無辣", "微辣", "小辣", "中辣", "大辣" };

        // --- 三組選項的定義 ---
        private static readonly string[] SpiceLevels = { "無辣", "微辣", "小辣", "中辣", "大辣" };
        private static readonly string[] IceLevels = { "去冰", "微冰", "正常冰", "熱飲" };
        private static readonly string[] SweetLevels = { "無糖", "微糖", "半糖", "正常糖" };

        private const string DefaultSpice = "無辣";
        private const string DefaultIce = "正常冰";
        private const string DefaultSweet = "正常糖";

        // --- 三組選項的狀態 ---
        private OptionGroup _spiceGroup;
        private OptionGroup _iceGroup;
        private OptionGroup _sweetGroup;


        /// <summary>
        /// 一組單選按鈕的狀態（辣度 / 冰塊 / 甜度共用同一套邏輯）
        /// </summary>
        private class OptionGroup
        {
            public FlowLayoutPanel Container;
            public Button Selected;
            public string SelectedValue;
        }

        // --- 對外回傳的結果（沒有該選項的商品回傳 null）---
        public int SelectedQuantity => _quantity;
        public string SelectedSpiceLevel => _spiceGroup?.SelectedValue;
        public string SelectedIceLevel => _iceGroup?.SelectedValue;
        public string SelectedSweetness => _sweetGroup?.SelectedValue;
        public string SelectedDiningType { get; private set; } = "內用";

        // --- 對外回傳的結果 ---
        //public int SelectedQuantity => _quantity;
        //public string SelectedSpiceLevel { get; private set; } = "無辣";
        //public string SelectedDiningType { get; private set; } = "內用";



        //public FormAddToCart(Product product)
        //{
        //    InitializeComponent();

        //    _product = product;

        //    ApplyStyle();
        //    //CreateSpiceButtons();
        //    CreateAllOptionGroups();
        //    SetupEvents();
        //    BindProduct();
        //}


        /// <summary>新增模式：從商品卡片加入購物車</summary>
        public FormAddToCart(Product product) : this(product, null) { }

        /// <summary>編輯模式：修改購物車中已存在的項目</summary>
        public FormAddToCart(CartItem editingItem) : this(editingItem?.Product, editingItem) { }

        private FormAddToCart(Product product, CartItem editingItem)
        {
            InitializeComponent();

            _product = product;
            _editingItem = editingItem;

            ApplyStyle();
            CreateAllOptionGroups();
            SetupEvents();
            BindProduct();

            // 編輯模式：把原本的選項預先選好
            ApplyEditingValues();
        }



        //#region 樣式設定

        private void ApplyStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint, true);


            // 視窗、圖片、按鈕圓角（改用會自動重算的版本）
            AttachRoundedRegion(this, 18);
            AttachRoundedRegion(picProduct, 12);
            //AttachRoundedRegion(btnClose, btnClose.Height / 2);
            //AttachRoundedRegion(pnlStepper, pnlStepper.Height / 2);
            //AttachRoundedRegion(btnMinus, btnMinus.Height / 2);
            //AttachRoundedRegion(btnPlus, btnPlus.Height / 2);

            AttachRoundedRegion(btnClose, -1);      // -1 = 圓形
            AttachRoundedRegion(pnlStepper, -1);    // -1 = 膠囊
            AttachRoundedRegion(btnMinus, -1);
            AttachRoundedRegion(btnPlus, -1);
            AttachRoundedRegion(btnContinue, 12);
            AttachRoundedRegion(btnConfirmAdd, 12);

            StyleGhostButton(btnClose);
            StyleGhostButton(btnMinus);
            StyleGhostButton(btnPlus);
            StyleGhostButton(btnContinue);
            btnContinue.Font = new Font("微軟正黑體", 12, FontStyle.Bold);

            // 餐點按鈕：先掛上自繪外框，再套用預設選取狀態
            InitDiningButton(btnDineIn);
            InitDiningButton(btnTakeout);
            _selectedDiningButton = btnDineIn;
            ApplyDiningStyle(btnDineIn, true);
            ApplyDiningStyle(btnTakeout, false);

            btnConfirmAdd.FlatAppearance.BorderSize = 0;
            btnConfirmAdd.MouseEnter += (s, e) => btnConfirmAdd.BackColor = Color.FromArgb(150, 22, 30);
            btnConfirmAdd.MouseLeave += (s, e) => btnConfirmAdd.BackColor = Color.FromArgb(183, 29, 37);

            // 標題列 icon
            picQtyIcon.Image = LoadIcon("icon_cart_red", 28);
            picSpiceIcon.Image = LoadIcon("icon_chili", 28);
            picIceIcon.Image = LoadIcon("icon_ice", 28);       // 新增
            picSweetIcon.Image = LoadIcon("icon_sugar", 28);   // 新增
            picDiningIcon.Image = LoadIcon("icon_dish", 28);

            EnableDragByHeader();
        }


        private void StyleGhostButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(120, 100, 90);
            btn.FlatAppearance.BorderSize = 0;    // 原本是 1，改成 0
            btn.Cursor = Cursors.Hand;

            // 圓角外框自繪
            btn.Paint += (s, e) =>
            {
                //var b = (Button)s;
                //int r = (b == btnClose || b == btnMinus || b == btnPlus) ? b.Height / 2 : 12;
                //UiHelper.DrawRoundedBorder(e.Graphics, b.ClientRectangle, r,
                //    Color.FromArgb(228, 216, 198), 1);

                var b = (Button)s;
                int r = (b == btnClose || b == btnMinus || b == btnPlus)
                    ? b.Height / 2      // 改成即時計算
                    : 12;
                UiHelper.DrawRoundedBorder(e.Graphics, b.ClientRectangle, r,
                    Color.FromArgb(228, 216, 198), 1);
            };
        }


        /// <summary>
        /// 餐點按鈕樣式：選中為紅底 + 白字，未選為白底 + 紅字
        /// 外框改用 Paint 自繪，才能貼合圓角
        /// </summary>
        private void ApplyDiningStyle(Button btn, bool selected)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;          // 關掉內建的矩形外框
            btn.BackColor = selected ? Color.FromArgb(183, 29, 37) : Color.White;
            btn.ForeColor = selected ? Color.White : Color.FromArgb(183, 29, 37);
            btn.Cursor = Cursors.Hand;

            btn.Invalidate();   // 觸發重繪，讓 Paint 依新狀態畫外框
        }

        /// <summary>
        /// 只在初始化時呼叫一次，掛上圓角與自繪外框
        /// </summary>
        private void InitDiningButton(Button btn)
        {
            AttachRoundedRegion(btn, 12);

            btn.Paint += (s, e) =>
            {
                var b = (Button)s;
                bool selected = (b == _selectedDiningButton);

                UiHelper.DrawRoundedBorder(
                    e.Graphics,
                    b.ClientRectangle,
                    12,
                    selected ? Color.FromArgb(183, 29, 37) : Color.FromArgb(228, 216, 198),
                    selected ? 2 : 1);
            };
        }


        //#region 選項按鈕（辣度 / 冰塊 / 甜度共用）

        private void CreateAllOptionGroups()
        {
            _spiceGroup = CreateOptionButtons(flpSpice, SpiceLevels, DefaultSpice);
            _iceGroup = CreateOptionButtons(flpIce, IceLevels, DefaultIce);
            _sweetGroup = CreateOptionButtons(flpSweet, SweetLevels, DefaultSweet);
        }

        /// <summary>
        /// 在指定容器中產生一組單選按鈕
        /// </summary>
        private OptionGroup CreateOptionButtons(FlowLayoutPanel container, string[] options, string defaultValue)
        {
            var group = new OptionGroup { Container = container };

            container.Controls.Clear();
            container.WrapContents = false;
            container.AutoScroll = false;

            foreach (string option in options)
            {
                var btn = new Button
                {
                    Text = option,
                    Font = new Font("微軟正黑體", 11),
                    AutoSize = false,
                    Cursor = Cursors.Hand,
                    Tag = option
                };

                ApplyOptionStyle(btn, false);
                AttachRoundedRegion(btn, 12);

                // 未選中才畫淺色外框，選中時是純紅底不需要框
                btn.Paint += (s, e) =>
                {
                    var b = (Button)s;
                    if (b != group.Selected)
                    {
                        UiHelper.DrawRoundedBorder(
                            e.Graphics, b.ClientRectangle, 12,
                            Color.FromArgb(228, 216, 198), 1);
                    }
                };

                btn.Click += (s, e) => SelectOption(group, (Button)s);

                container.Controls.Add(btn);
            }

            // 套用預設選取（找不到指定值時退回第一顆）
            var defaultBtn = container.Controls.OfType<Button>()
                                .FirstOrDefault(b => (b.Tag as string) == defaultValue)
                             ?? container.Controls.OfType<Button>().FirstOrDefault();

            if (defaultBtn != null)
                SelectOption(group, defaultBtn);

            container.Resize += (s, e) => LayoutOptionButtons(container);
            LayoutOptionButtons(container);

            return group;
        }

        /// <summary>
        /// 切換某一組選項的選取項目（先更新狀態，再觸發重繪）
        /// </summary>
        private void SelectOption(OptionGroup group, Button btn)
        {
            Button previous = group.Selected;

            group.Selected = btn;
            group.SelectedValue = btn.Tag as string;

            if (previous != null && previous != btn)
                ApplyOptionStyle(previous, false);

            ApplyOptionStyle(btn, true);
        }

        /// <summary>
        /// 選項按鈕樣式：選中為紅底白字，未選為白底
        /// </summary>
        private void ApplyOptionStyle(Button btn, bool selected)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = selected ? Color.FromArgb(160, 22, 30) : Color.White;
            btn.ForeColor = selected ? Color.White : Color.FromArgb(110, 95, 85);

            btn.Invalidate();
        }

        /// <summary>
        /// 依容器實際大小，平均分配該組按鈕的寬高（4 顆或 5 顆都適用）
        /// </summary>
        private void LayoutOptionButtons(FlowLayoutPanel container)
        {
            var buttons = container.Controls.OfType<Button>().ToList();
            if (buttons.Count == 0) return;

            const int gap = 8;
            int count = buttons.Count;

            int available = container.ClientSize.Width - container.Padding.Horizontal;
            int btnWidth = (available - gap * (count - 1)) / count;
            int btnHeight = container.ClientSize.Height - container.Padding.Vertical;

            for (int i = 0; i < count; i++)
            {
                buttons[i].Margin = new Padding(0, 0, i == count - 1 ? 0 : gap, 0);
                buttons[i].Size = new Size(Math.Max(50, btnWidth), Math.Max(30, btnHeight));
            }
        }

        //#region 資料綁定

        private void BindProduct()
        {
            if (_product == null) return;

            lblProductName.Text = _product.DisplayName;
            lblProductPrice.Text = $"NT$ {_product.Price:0}";

            // 沒有長描述時退回使用卡片上的短描述
            lblProductDesc.Text = string.IsNullOrWhiteSpace(_product.LongDescription)
                ? _product.Description
                : _product.LongDescription;

            lblQuantity.Text = _quantity.ToString();

            LoadProductImage();

            // 依商品類型收起不需要的選項區塊
            ApplySectionVisibility();
        }


        /// <summary>
        /// 依商品類型決定顯示哪些選項區塊，再依實際內容重算視窗高度
        /// </summary>
        private void ApplySectionVisibility()
        {
            HideSectionIfNeeded(2, pnlSpice, _spiceGroup, _product.HasSpiceOption);
            HideSectionIfNeeded(3, pnlIce, _iceGroup, _product.HasIceOption);
            HideSectionIfNeeded(4, pnlSweet, _sweetGroup, _product.HasSweetnessOption);

            //ResizeToContent();
        }

        /// <summary>
        /// 隱藏不需要的區塊（不再自行計算高度，交給 ResizeToContent 統一處理）
        /// </summary>
        private void HideSectionIfNeeded(int rowIndex, Panel panel, OptionGroup group, bool visible)
        {
            if (visible) return;

            panel.Visible = false;
            tlpMain.RowStyles[rowIndex].Height = 0;

            if (group != null)
                group.SelectedValue = null;   // 此商品沒有這個選項
        }

        /// <summary>
        /// 加總所有可見列的高度，設定為視窗高度；超出螢幕時自動夾限並開啟捲動
        /// </summary>
        //private void ResizeToContent()
        //{
        //    float rowsTotal = 0;

        //    foreach (RowStyle row in tlpMain.RowStyles)
        //        rowsTotal += row.Height;

        //    int needed = (int)Math.Ceiling(rowsTotal) + pnlFooter.Height;

        //    // 不超過螢幕工作區，避免視窗底部跑到畫面外
        //    int maxHeight = Screen.FromControl(this).WorkingArea.Height - 40;
        //    int finalHeight = Math.Min(needed, maxHeight);

        //    this.ClientSize = new Size(this.ClientSize.Width, finalHeight);

        //    // 內容放不下時允許捲動（底部按鈕因為是 Dock=Bottom，不會被捲走）
        //    tlpMain.AutoScroll = (needed > maxHeight);

        //    System.Diagnostics.Debug.WriteLine(
        //        $"[彈窗高度] 需要={needed}, 螢幕上限={maxHeight}, 實際={finalHeight}, 捲動={tlpMain.AutoScroll}");
        //}

        /// <summary>
        /// 加總「實際可見」區塊的渲染高度，設定為視窗高度
        /// 必須在 Form 顯示後呼叫，此時 DPI 縮放與版面計算才完成
        /// </summary>
        private void ResizeToContent()
        {
            int contentHeight = tlpMain.Padding.Vertical;

            // 用控制項的實際高度，而非設計時的 RowStyle 數值
            foreach (Control ctrl in tlpMain.Controls)
            {
                if (!ctrl.Visible) continue;
                contentHeight += ctrl.Height + ctrl.Margin.Vertical;
            }

            int needed = contentHeight + pnlFooter.Height;

            // 不超過螢幕工作區，避免視窗底部跑到畫面外
            int maxHeight = Screen.FromControl(this).WorkingArea.Height - 40;
            int finalHeight = Math.Min(needed, maxHeight);

            this.ClientSize = new Size(this.ClientSize.Width, finalHeight);

            // 內容放不下時允許捲動（底部按鈕是 Dock=Bottom，不會被捲走）
            tlpMain.AutoScroll = (needed > maxHeight);

            // 高度改變後要重新置中，否則會偏下
            RecenterToOwner();

            System.Diagnostics.Debug.WriteLine(
                $"[彈窗高度] 需要={needed}, 螢幕上限={maxHeight}, 實際={finalHeight}, 捲動={tlpMain.AutoScroll}");
        }

        /// <summary>
        /// 依 Owner（遮罩視窗）重新置中
        /// </summary>
        private void RecenterToOwner()
        {
            if (this.Owner == null) return;

            Rectangle ownerBounds = this.Owner.Bounds;

            this.Location = new Point(
                ownerBounds.Left + (ownerBounds.Width - this.Width) / 2,
                ownerBounds.Top + (ownerBounds.Height - this.Height) / 2);
        }


        private void LoadProductImage()
        {
            if (string.IsNullOrWhiteSpace(_product.ImageFileName)) return;

            string path = Path.Combine(
                UiHelper.GetAssetsFolder("Images", "Products"), _product.ImageFileName);

            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine($"找不到商品圖片：{path}");
                picProduct.BackColor = Color.FromArgb(235, 230, 222);
                return;
            }

            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var temp = Image.FromStream(fs))
                {
                    _productImage = new Bitmap(temp);
                }

                picProduct.Image = _productImage;
                picProduct.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"載入商品圖片失敗：{ex.Message}");
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
                    return new Bitmap(original, new Size(size, size));
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
            btnMinus.Click += (s, e) => ChangeQuantity(-1);
            btnPlus.Click += (s, e) => ChangeQuantity(+1);

            btnDineIn.Click += (s, e) => SelectDining(btnDineIn, "內用");
            btnTakeout.Click += (s, e) => SelectDining(btnTakeout, "外帶");

            btnClose.Click += (s, e) => CloseWithCancel();
            btnContinue.Click += (s, e) => CloseWithCancel();

            btnConfirmAdd.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
        }

        private void ChangeQuantity(int delta)
        {
            int newValue = _quantity + delta;

            // 數量限制在 1 ~ 99
            if (newValue < 1 || newValue > 99) return;

            _quantity = newValue;
            lblQuantity.Text = _quantity.ToString();
        }

        private void SelectDining(Button btn, string type)
        {
            Button previous = _selectedDiningButton;
            _selectedDiningButton = btn;      // 先更新狀態
            SelectedDiningType = type;

            if (previous != null && previous != btn)
                ApplyDiningStyle(previous, false);

            ApplyDiningStyle(btn, true);
        }

        private void CloseWithCancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 因為 FormBorderStyle = None 沒有標題列，改用拖曳商品資訊區來移動視窗
        /// </summary>
        private void EnableDragByHeader()
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

            pnlProductInfo.MouseDown += OnMouseDown;
            pnlProductInfo.MouseMove += OnMouseMove;
            pnlProductInfo.MouseUp += OnMouseUp;
        }

        /// <summary>
        /// 套用圓角，並在尺寸改變時自動重算
        /// （避免 DPI 縮放後 Region 尺寸對不上而被裁切）
        /// </summary>
        //private void AttachRoundedRegion(Control ctrl, int radius)
        //{
        //    ctrl.Resize += (s, e) =>
        //    {
        //        UiHelper.ApplyRoundedRegion(ctrl, radius);
        //        ctrl.Invalidate();
        //    };
        //    UiHelper.ApplyRoundedRegion(ctrl, radius);
        //}

        /// <summary>
        /// 套用圓角並在尺寸改變時自動重算
        /// </summary>
        /// <param name="radius">圓角半徑；傳入負值代表「膠囊/圓形」，半徑會依當下高度自動計算</param>
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

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);

        //    // Form 完全顯示後，各控制項才是最終尺寸（DPI 縮放已套用）
        //    LayoutSpiceButtons();
        //    this.ActiveControl = null;   // 避免按鈕帶著預設焦點虛線框
        //}

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);

        //    LayoutOptionButtons(flpSpice);
        //    LayoutOptionButtons(flpIce);
        //    LayoutOptionButtons(flpSweet);

        //    this.ActiveControl = null;
        //}

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // 順序很重要：先定高度，再依最終寬度分配按鈕
            ResizeToContent();

            LayoutOptionButtons(flpSpice);
            LayoutOptionButtons(flpIce);
            LayoutOptionButtons(flpSweet);

            this.ActiveControl = null;
        }


        /// <summary>
        /// 編輯模式時，把該筆項目原本的數量與選項還原到畫面上
        /// </summary>
        private void ApplyEditingValues()
        {
            if (!IsEditMode) return;

            // 數量
            _quantity = Math.Max(1, _editingItem.Quantity);
            lblQuantity.Text = _quantity.ToString();

            // 三組選項
            SelectOptionByValue(_spiceGroup, _editingItem.SpiceLevel);
            SelectOptionByValue(_iceGroup, _editingItem.IceLevel);
            SelectOptionByValue(_sweetGroup, _editingItem.Sweetness);

            // 內用 / 外帶
            if (_editingItem.DiningType == "外帶")
                SelectDining(btnTakeout, "外帶");
            else
                SelectDining(btnDineIn, "內用");

            // 按鈕文字改成編輯情境的說法
            btnConfirmAdd.Text = "更新項目";
            btnContinue.Text = "取消";
        }

        /// <summary>
        /// 依文字值選取某一組選項中對應的按鈕
        /// </summary>
        private void SelectOptionByValue(OptionGroup group, string value)
        {
            if (group == null || string.IsNullOrEmpty(value)) return;

            var target = group.Container.Controls.OfType<Button>()
                            .FirstOrDefault(b => (b.Tag as string) == value);

            if (target != null)
                SelectOption(group, target);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _productImage?.Dispose();
            base.OnFormClosed(e);
        }



    }
}
