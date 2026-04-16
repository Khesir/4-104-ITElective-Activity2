using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.modules.transaction;
using System.Drawing.Printing;

namespace _4_104_ITElective_Activity2.Reports
{
    public static class AllOrdersReportHelper
    {
        public static async Task ShowAsync()
        {
            var service      = ServiceLocator.Get<TransactionService>();
            var transactions = await service.GetAllTransactionsAsync();

            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);
            doc.DefaultPageSettings.Margins   = new Margins(50, 50, 50, 50);

            int pageNumber = 0;
            doc.PrintPage += (s, e) =>
            {
                pageNumber++;
                DrawReport(e, transactions, pageNumber);
            };

            using var preview = new PrintPreviewDialog
            {
                Document      = doc,
                Text          = "All Transactions Report",
                WindowState   = FormWindowState.Maximized,
                StartPosition = FormStartPosition.CenterScreen,
            };
            preview.ShowDialog();
        }

        private static void DrawReport(
            PrintPageEventArgs e,
            List<Transaction> transactions,
            int pageNumber)
        {
            var g      = e.Graphics!;
            var bounds = e.MarginBounds;
            float left  = bounds.Left;
            float width = bounds.Width;
            float y     = bounds.Top;

            var centerFmt = new StringFormat { Alignment = StringAlignment.Center };
            var rightFmt  = new StringFormat { Alignment = StringAlignment.Far };
            var leftFmt   = new StringFormat { Alignment = StringAlignment.Near };

            using var titleFont  = new Font("Courier New", 18f, FontStyle.Bold);
            using var subFont    = new Font("Courier New", 9f, FontStyle.Italic);
            using var hdrFont    = new Font("Courier New", 10f, FontStyle.Bold);
            using var rowFont    = new Font("Courier New", 9f);
            using var totalFont  = new Font("Courier New", 11f, FontStyle.Bold);
            using var pen        = new Pen(Color.Black, 0.5f);
            using var thickPen   = new Pen(Color.Black, 1.5f);
            using var grayBrush  = new SolidBrush(Color.FromArgb(7, 48, 43));
            using var whiteBrush = new SolidBrush(Color.White);

            // ── Title ──────────────────────────────────────────────────
            g.DrawString("ALL TRANSACTIONS REPORT", titleFont, Brushes.Black,
                new RectangleF(left, y, width, 30), centerFmt);
            y += 32;

            g.DrawString($"Generated: {DateTime.Now:MMMM dd, yyyy  hh:mm tt}", subFont, Brushes.Black,
                new RectangleF(left, y, width, 16), centerFmt);
            y += 22;

            g.DrawLine(thickPen, left, y, left + width, y); y += 10;

            // ── Column layout ──────────────────────────────────────────
            float idW   = width * 0.15f;
            float dtW   = width * 0.55f;
            float amtW  = width * 0.30f;

            float xId   = left;
            float xDt   = left + idW;
            float xAmt  = left + idW + dtW;

            float rowH  = 20f;
            float hdrH  = 24f;

            // ── Header Row (dark background) ───────────────────────────
            g.FillRectangle(grayBrush, left, y, width, hdrH);
            g.DrawString("Trans. ID", hdrFont, whiteBrush,
                new RectangleF(xId + 4, y + 3, idW, hdrH), leftFmt);
            g.DrawString("Date & Time", hdrFont, whiteBrush,
                new RectangleF(xDt + 4, y + 3, dtW, hdrH), leftFmt);
            g.DrawString("Total Amount", hdrFont, whiteBrush,
                new RectangleF(xAmt, y + 3, amtW - 4, hdrH), rightFmt);
            y += hdrH;

            // ── Data Rows ──────────────────────────────────────────────
            bool shaded = false;
            using var shadeBrush = new SolidBrush(Color.FromArgb(235, 245, 240));

            foreach (var tx in transactions)
            {
                if (shaded)
                    g.FillRectangle(shadeBrush, left, y, width, rowH);

                g.DrawString(tx.id?.ToString(), rowFont, Brushes.Black,
                    new RectangleF(xId + 4, y + 3, idW, rowH), leftFmt);
                g.DrawString(tx.createdAt.ToString("MM/dd/yyyy hh:mm tt"), rowFont, Brushes.Black,
                    new RectangleF(xDt + 4, y + 3, dtW, rowH), leftFmt);
                g.DrawString(tx.totalAmount.ToString("N2"), rowFont, Brushes.Black,
                    new RectangleF(xAmt, y + 3, amtW - 4, rowH), rightFmt);

                g.DrawLine(pen, left, y + rowH, left + width, y + rowH);
                y    += rowH;
                shaded = !shaded;
            }

            // ── Grand Total ────────────────────────────────────────────
            double grandTotal = transactions.Sum(t => t.totalAmount);
            y += 6;
            g.DrawLine(thickPen, left, y, left + width, y); y += 8;

            g.DrawString("GRAND TOTAL", totalFont, Brushes.Black,
                new RectangleF(left + 4, y, width * 0.6f, 22), leftFmt);
            g.DrawString($"PHP {grandTotal:N2}", totalFont, Brushes.Black,
                new RectangleF(xAmt, y, amtW - 4, 22), rightFmt);
            y += 22;

            g.DrawLine(thickPen, left, y, left + width, y);

            e.HasMorePages = false;
        }
    }
}
