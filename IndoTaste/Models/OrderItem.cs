using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoTaste.Models
{
    /// <summary>
    /// 訂單中的單一品項。
    /// 注意：這裡是「下單當時的快照」，不參照 Product 物件
    /// —— 之後商品改價，歷史訂單金額才不會跟著變動。
    /// </summary>
    public class OrderItem
    {
        // 商品資訊快照
        public int ProductId { get; set; }
        public string NameZh { get; set; }
        public string NameId { get; set; }
        public string ImageFileName { get; set; }
        public decimal UnitPrice { get; set; }      // 下單當時的單價
        public int Quantity { get; set; }

        // 客製選項（該商品沒有此選項時為 null）
        public string SpiceLevel { get; set; }
        public string IceLevel { get; set; }
        public string Sweetness { get; set; }
        public string DiningType { get; set; }

        /// <summary>小計 = 單價 × 數量</summary>
        public decimal Subtotal => UnitPrice * Quantity;

        /// <summary>選項摘要，例如「中辣 | 內用」</summary>
        public string OptionsSummary
        {
            get
            {
                var parts = new System.Collections.Generic.List<string>();

                if (!string.IsNullOrEmpty(SpiceLevel)) parts.Add(SpiceLevel);
                if (!string.IsNullOrEmpty(IceLevel)) parts.Add(IceLevel);
                if (!string.IsNullOrEmpty(Sweetness)) parts.Add(Sweetness);
                if (!string.IsNullOrEmpty(DiningType)) parts.Add(DiningType);

                return string.Join(" | ", parts);
            }
        }

        /// <summary>從購物車項目建立訂單品項（複製一份快照）</summary>
        public static OrderItem FromCartItem(CartItem cartItem)
        {
            return new OrderItem
            {
                ProductId = cartItem.Product.ProductId,
                NameZh = cartItem.Product.NameZh,
                NameId = cartItem.Product.NameId,
                ImageFileName = cartItem.Product.ImageFileName,
                UnitPrice = cartItem.Product.Price,
                Quantity = cartItem.Quantity,
                SpiceLevel = cartItem.SpiceLevel,
                IceLevel = cartItem.IceLevel,
                Sweetness = cartItem.Sweetness,
                DiningType = cartItem.DiningType
            };
        }
    }
}
