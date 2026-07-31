using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoTaste.Models
{
    /// <summary>訂單狀態（管理者頁面會用到）</summary>
    public enum OrderStatus
    {
        Pending,      // 待處理
        Preparing,    // 製作中
        Completed,    // 已完成
        Cancelled     // 已取消
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }     // 例：20260730-001
        public DateTime CreatedAt { get; set; }
        public string PaymentMethod { get; set; }   // 現金 / LINE Pay / 信用卡
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>訂單總金額</summary>
        public decimal TotalAmount => Items.Sum(i => i.Subtotal);

        /// <summary>共幾項商品（不含數量）</summary>
        public int ItemCount => Items.Count;

        /// <summary>總數量幾件（含數量）</summary>
        public int TotalQuantity => Items.Sum(i => i.Quantity);

        /// <summary>狀態的中文顯示，供管理者頁面使用</summary>
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case OrderStatus.Pending: return "待處理";
                    case OrderStatus.Preparing: return "製作中";
                    case OrderStatus.Completed: return "已完成";
                    case OrderStatus.Cancelled: return "已取消";
                    default: return "未知";
                }
            }
        }
    }
}
