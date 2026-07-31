using IndoTaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoTaste.Services
{
    /// <summary>
    /// 購物車：管理項目的增刪改與金額計算
    /// 只處理資料邏輯，不碰任何 UI
    /// </summary>
    public class ShoppingCart
    {
        private readonly List<CartItem> _items = new List<CartItem>();

        /// <summary>目前所有項目（唯讀，外部不能直接改）</summary>
        public IReadOnlyList<CartItem> Items => _items;

        /// <summary>總金額</summary>
        public decimal TotalAmount => _items.Sum(i => i.Subtotal);

        /// <summary>總件數（含數量，右上角徽章用）</summary>
        public int TotalCount => _items.Sum(i => i.Quantity);

        /// <summary>品項數（不含數量，「N 項商品」用）</summary>
        public int ItemCount => _items.Count;

        public bool IsEmpty => _items.Count == 0;

        /// <summary>購物車內容有任何變動時觸發，UI 收到後重畫</summary>
        public event EventHandler Changed;

        /// <summary>
        /// 加入商品；若已存在完全相同的品項（商品 + 所有選項），則累加數量
        /// </summary>
        public void Add(Product product, int quantity,
                        string spice, string ice, string sweet, string dining)
        {
            if (product == null || quantity <= 0) return;

            var existing = FindSame(product.ProductId, spice, ice, sweet, dining);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                    SpiceLevel = spice,
                    IceLevel = ice,
                    Sweetness = sweet,
                    DiningType = dining
                });
            }

            OnChanged();
        }

        /// <summary>
        /// 編輯現有項目（鉛筆按鈕用）
        /// 若改完後跟其他項目變成相同組合，會自動合併成一筆
        /// </summary>
        public void UpdateItem(CartItem item, int quantity,
                               string spice, string ice, string sweet, string dining)
        {
            if (item == null || !_items.Contains(item)) return;

            if (quantity <= 0)
            {
                Remove(item);
                return;
            }

            // 找找看有沒有「別筆」跟改完後的組合一樣
            var duplicate = _items.FirstOrDefault(i =>
                i != item && i.IsSameAs(item.Product.ProductId, spice, ice, sweet, dining));

            if (duplicate != null)
            {
                // 合併：把數量加到那筆，刪掉目前這筆
                duplicate.Quantity += quantity;
                _items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
                item.SpiceLevel = spice;
                item.IceLevel = ice;
                item.Sweetness = sweet;
                item.DiningType = dining;
            }

            OnChanged();
        }

        /// <summary>只調整數量；減到 0 以下自動移除該筆</summary>
        public void UpdateQuantity(CartItem item, int quantity)
        {
            if (item == null || !_items.Contains(item)) return;

            if (quantity <= 0)
            {
                Remove(item);
                return;
            }

            item.Quantity = quantity;
            OnChanged();
        }

        public void Remove(CartItem item)
        {
            if (item == null) return;

            if (_items.Remove(item))
                OnChanged();
        }

        public void Clear()
        {
            if (_items.Count == 0) return;

            _items.Clear();
            OnChanged();
        }

        private CartItem FindSame(int productId, string spice, string ice, string sweet, string dining)
        {
            return _items.FirstOrDefault(i => i.IsSameAs(productId, spice, ice, sweet, dining));
        }

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
