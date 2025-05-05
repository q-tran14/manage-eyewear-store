using System.Data;
using DAL.Utils;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using DAL.Object;

namespace eyewear_store_management_system.Utils
{
    public static class OtherUtilities
    {
        public static DataTable Search(string query, Form form)
        {

            DataTable dt = null;
            try
            {
                dt = UtilityDatabase.Instance.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification("Search Fail", ex.Message, "error", form);
            }
            return dt;
        }

        public static void SetUpComboBox<T, T1>(CustomComponents.CustomComboBox cb, List<KeyValuePair<T, T1>> keyPairValue)
        {
            cb.Items.Clear();

            foreach (var item in keyPairValue) cb.Items.Add(item);

            // Set display member and value member
            cb.DisplayMember = "Key";
            cb.ValueMember = "Value";
            cb.SelectedIndex = 0;
        }

        public static List<KeyValuePair<string, string>> ConvertToKeyValueList(JArray array, string displayField = "name", string valueField = "code")
        {
            List<KeyValuePair<string, string>> result = new();
            result.Add(new KeyValuePair<string, string>("Choose Option", ""));
            foreach (var item in array)
            {
                string key = item[displayField]?.ToString();
                string value = item[valueField]?.ToString();
                if (key != null && value != null)
                {
                    result.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return result;
        }

        public static async Task LoadCities(CustomComponents.CustomComboBox cb)
        {
            var citiesArray = await LocationByAPI.GetCities();

            if (citiesArray != null)
            {
                var cityList = ConvertToKeyValueList(citiesArray);
                SetUpComboBox(cb, cityList);
            }
        }
        public static async Task LoadDistricts(CustomComponents.CustomComboBox cb, string cityCode)
        {
            var districtsArray = await LocationByAPI.GetDistricts(cityCode);

            if (districtsArray != null)
            {
                var districtList = ConvertToKeyValueList(districtsArray);
                SetUpComboBox(cb, districtList);
            }
        }
        public static async Task LoadWards(CustomComponents.CustomComboBox cb, string districtCode)
        {
            var wardsArray = await LocationByAPI.GetWards(districtCode);

            if (wardsArray != null)
            {
                var wardList = ConvertToKeyValueList(wardsArray);
                SetUpComboBox(cb, wardList);
            }
        }

        public static void ButtonEnable(CustomComponents.CustomButton btn)
        {
            btn.Enabled = true;
            btn.BackColor = Color.FromArgb(51, 51, 51);

        }
        public static void ButtonDisable(CustomComponents.CustomButton btn)
        {
            btn.Enabled = false;
            btn.BackColor = Color.DarkGray;
        }
        public static DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible) // chỉ lấy cột hiển thị
                    dt.Columns.Add(column.HeaderText);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (dgv.Columns[cell.ColumnIndex].Visible)
                            dr[i++] = cell.Value?.ToString() ?? "";
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        public static void Print(string title, Invoice _invoice, DataTable dt)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                Font titleFont = new Font("Arial", 16, FontStyle.Bold);
                Font detailFont = new Font("Arial", 10);
                Font headerFont = new Font("Arial", 10, FontStyle.Bold);

                float left = e.MarginBounds.Left;
                float top = e.MarginBounds.Top;
                float right = e.MarginBounds.Right;
                float y = top;

                // Tiêu đề
                string header = title;
                SizeF headerSize = g.MeasureString(header, titleFont);
                g.DrawString(header, titleFont, Brushes.Black, left + (e.MarginBounds.Width - headerSize.Width) / 2, y);
                y += headerSize.Height + 10;

                // Parse detail lines thành từng dòng
                string[] leftColumnLines = new[]
                {
                    $"Invoice ID: {_invoice.ID}",
                    $"Create Date: {_invoice.CreateDate:dd/MM/yyyy}",
                    $"Customer Name: {_invoice.Customer.Name}",
                    $"Staff ID: {_invoice.Employee.ID}"
                };

                string[] rightColumnLines = new[]
                {
                            _invoice.Voucher != null ? $"Voucher: {_invoice.Voucher.ID}" : "Voucher: No use voucher",
                            _invoice.Voucher != null ? $"Description: {_invoice.Voucher.Description}" : "Description: ",
                            $"Customer Phone: {_invoice.Customer.Phone}",
                    $"Staff Name: {_invoice.Employee.Name}"
                };

                // Cột trái – phải
                float columnWidth = (e.MarginBounds.Width - 40) / 2;
                float spacing = 20;

                for (int i = 0; i < leftColumnLines.Length; i++)
                {
                    g.DrawString(leftColumnLines[i], detailFont, Brushes.Black, left, y);
                    g.DrawString(rightColumnLines[i], detailFont, Brushes.Black, left + columnWidth + spacing, y);
                    y += detailFont.GetHeight(g) + 5;
                }

                y += 10; // khoảng cách trước bảng


                // Vẽ bảng sản phẩm
                float rowHeight = detailFont.GetHeight(g) + 5;
                float[] columnWidths = { 50, 200, 80, 100, 100 };
                float totalTableWidth = columnWidths.Sum();
                float tableLeft = left + (e.MarginBounds.Width - totalTableWidth) / 2;
                string[] columnNames = { "ID", "Product Name", "Quantity", "Unit Price", "Total Price" };

                // Vẽ header
                float x = tableLeft;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    g.DrawString(columnNames[i], headerFont, Brushes.Black, x, y);
                    x += columnWidths[i];
                }
                y += rowHeight;

                // Vẽ nội dung bảng
                foreach (DataRow row in dt.Rows)
                {
                    x = tableLeft;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        g.DrawString(row[i].ToString(), detailFont, Brushes.Black, x, y);
                        x += columnWidths[i];
                    }
                    y += rowHeight;
                }


                // In Total Bill căn phải
                string totalBill = $"Total Bill: {_invoice.TotalAmount}";
                SizeF totalBillSize = g.MeasureString(totalBill, detailFont);
                g.DrawString(totalBill, detailFont, Brushes.Black, right - totalBillSize.Width, y + 10);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument
            };
            preview.ShowDialog();
        }

        public static void ExportToExcel(DataTable dt)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Save Excel File";
                sfd.FileName = "export.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    // Set the LicenseContext for EPPlus (NonCommercial for free use)
                    ExcelPackage.License.SetNonCommercialPersonal("Trần Quang Quân");
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                            // Header
                            for (int col = 0; col < dt.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col + 1].Value = dt.Columns[col].ColumnName;
                                worksheet.Cells[1, col + 1].Style.Font.Bold = true;
                                worksheet.Cells[1, col + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, col + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            }

                            // Data
                            for (int row = 0; row < dt.Rows.Count; row++)
                            {
                                for (int col = 0; col < dt.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dt.Rows[row][col];
                                }
                            }

                            // Auto-fit columns
                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                            // Save to file
                            File.WriteAllBytes(filePath, package.GetAsByteArray());
                            MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Export failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
