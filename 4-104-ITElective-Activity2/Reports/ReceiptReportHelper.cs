using _4_104_ITElective_Activity2.modules.transactionItem;
using System.Drawing.Printing;

namespace _4_104_ITElective_Activity2.Reports
{
    public static class ReceiptReportHelper
    {
        public static void Show(
            int transactionId,
            DateTime dateTime,
            string cashierName,
            List<TransactionItem> items,
            double totalAmount)
        {
            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 350, 1100);
            doc.DefaultPageSettings.Margins   = new Margins(15, 15, 20, 20);

            doc.PrintPage += (s, e) =>
                DrawReceipt(e, transactionId, dateTime, cashierName, items, totalAmount);

            using var preview = new PrintPreviewDialog
            {
                Document      = doc,
                Text          = $"Receipt — Transaction #{transactionId}",
                WindowState   = FormWindowState.Normal,
                Width         = 520,
                Height        = 700,
                StartPosition = FormStartPosition.CenterScreen,
            };
            preview.ShowDialog();
        }

        private static void DrawReceipt(
            PrintPageEventArgs e,
            int transactionId,
            DateTime dateTime,
            string cashierName,
            List<TransactionItem> items,
            double totalAmount)
        {
            var g      = e.Graphics!;
            var bounds = e.MarginBounds;
            float left  = bounds.Left;
            float width = bounds.Width;
            float y     = bounds.Top;

            var centerFmt = new StringFormat { Alignment = StringAlignment.Center };
            var rightFmt  = new StringFormat { Alignment = StringAlignment.Far };
            var leftFmt   = new StringFormat { Alignment = StringAlignment.Near };

            using var titleFont  = new Font("Courier New", 13f, FontStyle.Bold);
            using var subFont    = new Font("Courier New", 7f,  FontStyle.Italic);
            using var infoFont   = new Font("Courier New", 8f);
            using var infoFontB  = new Font("Courier New", 8f,  FontStyle.Bold);
            using var itemFont   = new Font("Courier New", 7.5f);
            using var totalFont  = new Font("Courier New", 10f, FontStyle.Bold);
            using var thanksFont = new Font("Courier New", 8f,  FontStyle.Italic);
            using var pen        = new Pen(Color.Black, 0.5f);
            using var thickPen   = new Pen(Color.Black, 1f);

            // ── Shop Name ──────────────────────────────────────────────
            g.DrawString("STARBUKO COFFEE", titleFont, Brushes.Black,
                new RectangleF(left, y, width, 22), centerFmt);
            y += 22;

            g.DrawString("Your Coffee, Our Craft", subFont, Brushes.Black,
                new RectangleF(left, y, width, 14), centerFmt);
            y += 16;

            DrawDashedLine(g, pen, left, left + width, y); y += 8;

            // ── Transaction Info ───────────────────────────────────────
            g.DrawString($"Transaction # : {transactionId}", infoFont, Brushes.Black,
                new RectangleF(left, y, width, 14), leftFmt);
            y += 14;

            g.DrawString($"Date/Time     : {dateTime:MM/dd/yyyy hh:mm tt}", infoFont, Brushes.Black,
                new RectangleF(left, y, width, 14), leftFmt);
            y += 14;

            g.DrawString($"Cashier       : {cashierName}", infoFont, Brushes.Black,
                new RectangleF(left, y, width, 14), leftFmt);
            y += 16;

            DrawDashedLine(g, pen, left, left + width, y); y += 8;

            // ── Column boundaries (6 points = 5 columns) ──────────────
            //   Item  | Size  | Qty  | Price | Total
            //   0%      38%    56%    67%     83%    100%
            float xItem  = left;
            float xSize  = left + width * 0.38f;
            float xQty   = left + width * 0.56f;
            float xPrice = left + width * 0.67f;
            float xTotal = left + width * 0.83f;
            float xRight = left + width;

            float rowH = 13f;

            // ── Column Headers ─────────────────────────────────────────
            DrawRow(g, infoFontB,
                "Item", "Size", "Qty", "Price", "Total",
                xItem, xSize, xQty, xPrice, xTotal, xRight, y, rowH);
            y += rowH;

            g.DrawLine(thickPen, left, y, left + width, y); y += 5;

            // ── Item Rows ──────────────────────────────────────────────
            foreach (var item in items)
            {
                DrawRow(g, itemFont,
                    TruncateName(item.name, 14),
                    item.cupSize.ToString(),
                    item.quantity.ToString(),
                    item.price.ToString("F2"),
                    item.totalPrice.ToString("F2"),
                    xItem, xSize, xQty, xPrice, xTotal, xRight, y, rowH);
                y += rowH;
            }

            g.DrawLine(thickPen, left, y, left + width, y); y += 6;

            // ── Total Row ──────────────────────────────────────────────
            // "TOTAL" label on the left, amount right-aligned in the Total column
            g.DrawString("TOTAL", totalFont, Brushes.Black,
                new RectangleF(xItem, y, xPrice - xItem, 18), leftFmt);
            g.DrawString($"PHP {totalAmount:F2}", totalFont, Brushes.Black,
                new RectangleF(xPrice, y, xRight - xPrice, 18), rightFmt);
            y += 22;

            DrawDashedLine(g, pen, left, left + width, y); y += 10;

            // ── Thank You ──────────────────────────────────────────────
            g.DrawString("Thank you for visiting!", thanksFont, Brushes.Black,
                new RectangleF(left, y, width, 14), centerFmt);
        }

        // Draws one row across all 5 columns using correct boundaries
        private static void DrawRow(
            Graphics g, Font font,
            string c1, string c2, string c3, string c4, string c5,
            float xItem, float xSize, float xQty, float xPrice, float xTotal, float xRight,
            float y, float h)
        {
            var leftFmt   = new StringFormat { Alignment = StringAlignment.Near };
            var centerFmt = new StringFormat { Alignment = StringAlignment.Center };
            var rightFmt  = new StringFormat { Alignment = StringAlignment.Far };

            // Item  — left-aligned, from xItem to xSize
            g.DrawString(c1, font, Brushes.Black,
                new RectangleF(xItem,  y, xSize  - xItem  - 2, h), leftFmt);
            // Size  — left-aligned, from xSize to xQty
            g.DrawString(c2, font, Brushes.Black,
                new RectangleF(xSize,  y, xQty   - xSize  - 2, h), leftFmt);
            // Qty   — center-aligned, from xQty to xPrice
            g.DrawString(c3, font, Brushes.Black,
                new RectangleF(xQty,   y, xPrice - xQty   - 2, h), centerFmt);
            // Price — right-aligned, from xPrice to xTotal
            g.DrawString(c4, font, Brushes.Black,
                new RectangleF(xPrice, y, xTotal - xPrice - 2, h), rightFmt);
            // Total — right-aligned, from xTotal to xRight
            g.DrawString(c5, font, Brushes.Black,
                new RectangleF(xTotal, y, xRight - xTotal,     h), rightFmt);
        }

        private static void DrawDashedLine(Graphics g, Pen pen, float x1, float x2, float y)
        {
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(pen, x1, y, x2, y);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }

        private static string TruncateName(string name, int max) =>
            name.Length <= max ? name : name[..max] + "..";
    }
}
