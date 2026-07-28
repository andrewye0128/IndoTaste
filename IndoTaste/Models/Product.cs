using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoTaste.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string NameZh { get; set; }          // 印尼炒飯
        public string NameId { get; set; }          // Nasi Goreng
        public string Description { get; set; }     // 印尼經典炒飯，香氣濃郁
        public decimal Price { get; set; }          // 120
        public double Rating { get; set; }          // 5.0
        public bool IsPopular { get; set; }         // 是否顯示「熱門」標籤
        public string CategoryKey { get; set; }     // rice / plate / vegetable ...
        public string ImageFileName { get; set; }   // product_nasi_goreng.png

        /// <summary>卡片上顯示的完整名稱：印尼炒飯 (Nasi Goreng)</summary>
        public string DisplayName => $"{NameZh} ({NameId})";
    }
}
