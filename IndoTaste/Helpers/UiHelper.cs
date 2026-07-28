using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace IndoTaste.Helpers
{
    public static class UiHelper
    {
        /// <summary>
        /// 取得專案 Assets 底下的資料夾路徑（從 bin\Debug 往上跳兩層回到專案資料夾）
        /// 例：GetAssetsFolder("Images", "Products")
        /// </summary>
        public static string GetAssetsFolder(params string[] subFolders)
        {
            string projectRoot = Directory.GetParent(
                Directory.GetParent(Application.StartupPath).FullName).FullName;

            string path = Path.Combine(projectRoot, "Assets");

            foreach (string folder in subFolders)
                path = Path.Combine(path, folder);

            return path;
        }

        /// <summary>
        /// 建立圓角路徑，可個別指定四個角是否要圓角
        /// （商品圖片只需要上方兩角圓，下方要維持直角才能與卡片本體貼合）
        /// </summary>
        public static GraphicsPath CreateRoundedPath(
            int width, int height, int radius,
            bool topLeft = true, bool topRight = true,
            bool bottomRight = true, bool bottomLeft = true)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            int right = width - 1;
            int bottom = height - 1;

            // 左上
            if (topLeft) path.AddArc(0, 0, d, d, 180, 90);
            else path.AddLine(0, 0, 0, 0);

            // 右上
            if (topRight) path.AddArc(right - d, 0, d, d, 270, 90);
            else path.AddLine(right, 0, right, 0);

            // 右下
            if (bottomRight) path.AddArc(right - d, bottom - d, d, d, 0, 90);
            else path.AddLine(right, bottom, right, bottom);

            // 左下
            if (bottomLeft) path.AddArc(0, bottom - d, d, d, 90, 90);
            else path.AddLine(0, bottom, 0, bottom);

            path.CloseAllFigures();
            return path;
        }

        /// <summary>
        /// 將控制項裁切成圓角（可指定哪幾個角）
        /// </summary>
        public static void ApplyRoundedRegion(
            Control ctrl, int radius,
            bool topLeft = true, bool topRight = true,
            bool bottomRight = true, bool bottomLeft = true)
        {
            if (ctrl == null || ctrl.Width <= 0 || ctrl.Height <= 0) return;

            using (var path = CreateRoundedPath(
                ctrl.Width, ctrl.Height, radius,
                topLeft, topRight, bottomRight, bottomLeft))
            {
                ctrl.Region = new Region(path);
            }
        }

        /// <summary>
        /// 以「填滿並裁切」的方式繪製圖片（等同 CSS 的 background-size: cover）
        /// 圖片不會變形，超出範圍的部分置中裁掉
        /// </summary>
        public static void DrawImageCover(Graphics g, Image image, Rectangle bounds)
        {
            if (image == null || bounds.Width <= 0 || bounds.Height <= 0) return;

            float scale = Math.Max(
                (float)bounds.Width / image.Width,
                (float)bounds.Height / image.Height);

            int w = (int)(image.Width * scale);
            int h = (int)(image.Height * scale);
            int x = bounds.X + (bounds.Width - w) / 2;
            int y = bounds.Y + (bounds.Height - h) / 2;

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(image, new Rectangle(x, y, w, h));
        }
    }
}