using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoTaste.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        // 從 FormAddToCart 帶回來的選項（該商品沒有此選項時為 null）
        public string SpiceLevel { get; set; }
        public string IceLevel { get; set; }
        public string Sweetness { get; set; }
        public string DiningType { get; set; }

        /// <summary>單筆小計</summary>
        public decimal Subtotal => Product.Price * Quantity;

        /// <summary>
        /// 選項摘要，顯示在購物車列上，例如「中辣・內用」
        /// </summary>
        public string OptionsSummary
        {
            get
            {
                var parts = new System.Collections.Generic.List<string>();

                if (!string.IsNullOrEmpty(SpiceLevel)) parts.Add(SpiceLevel);
                if (!string.IsNullOrEmpty(IceLevel)) parts.Add(IceLevel);
                if (!string.IsNullOrEmpty(Sweetness)) parts.Add(Sweetness);
                if (!string.IsNullOrEmpty(DiningType)) parts.Add(DiningType);

                return string.Join("・", parts);
            }
        }

        /// <summary>
        /// 判斷是否為「相同品項」：商品與所有選項都一致才算同一筆
        /// </summary>
        public bool IsSameAs(int productId, string spice, string ice, string sweet, string dining)
        {
            return Product.ProductId == productId
                && SpiceLevel == spice
                && IceLevel == ice
                && Sweetness == sweet
                && DiningType == dining;
        }
    }
}
