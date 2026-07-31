using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndoTaste.Models;

namespace IndoTaste.Services
{
    /// <summary>
    /// 全域訂單服務（Singleton）
    /// 顧客點餐頁與管理者訂單管理頁共用同一份資料
    /// </summary>
    public class OrderService
    {
        // --- Singleton ---
        private static OrderService _instance;
        public static OrderService Instance =>
            _instance ?? (_instance = new OrderService());

        private OrderService() { }   // 私有建構子，禁止外部 new

        private readonly List<Order> _orders = new List<Order>();
        private int _nextOrderId = 1;

        /// <summary>所有訂單（唯讀，外部不能直接改）</summary>
        public IReadOnlyList<Order> Orders => _orders;

        /// <summary>訂單清單有變動時觸發，UI 收到後重畫</summary>
        public event EventHandler OrdersChanged;

        /// <summary>
        /// 把購物車內容轉成訂單並存入
        /// </summary>
        public Order CreateOrder(ShoppingCart cart, string paymentMethod)
        {
            if (cart == null || cart.IsEmpty) return null;

            var order = new Order
            {
                OrderId = _nextOrderId++,
                OrderNumber = GenerateOrderNumber(),
                CreatedAt = DateTime.Now,
                PaymentMethod = paymentMethod,
                Status = OrderStatus.Pending,
                // 逐筆複製快照
                Items = cart.Items.Select(OrderItem.FromCartItem).ToList()
            };

            _orders.Add(order);
            OnOrdersChanged();

            return order;
        }

        /// <summary>
        /// 產生訂單編號：日期 + 當日流水號（20260730-001）
        /// </summary>
        private string GenerateOrderNumber()
        {
            DateTime today = DateTime.Today;

            int todayCount = _orders.Count(o => o.CreatedAt.Date == today) + 1;

            return $"{today:yyyyMMdd}-{todayCount:000}";
        }

        // --- 以下供管理者訂單管理頁面使用 ---

        public Order GetById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetByDate(DateTime date)
        {
            return _orders.Where(o => o.CreatedAt.Date == date.Date);
        }

        public IEnumerable<Order> GetByStatus(OrderStatus status)
        {
            return _orders.Where(o => o.Status == status);
        }

        public void UpdateStatus(int orderId, OrderStatus status)
        {
            var order = GetById(orderId);
            if (order == null) return;

            order.Status = status;
            OnOrdersChanged();
        }

        private void OnOrdersChanged()
        {
            OrdersChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
