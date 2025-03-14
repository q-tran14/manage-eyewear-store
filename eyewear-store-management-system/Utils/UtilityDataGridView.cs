using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eyewear_store_management_system.Utils
{
    public static class UtilityDataGridView
    {
        public static void SetupDataGridView(DataGridView dgv)
        {
            dgv.VirtualMode = true; // Bật chế độ ảo
            dgv.RowHeadersVisible = false;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;

            // Đặt viền ngoài màu đen
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.GridColor = Color.Black;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Đặt nền màu đen cho phần trống
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.BackColor = Color.White; // Giữ ô có dữ liệu màu trắng

            // Bật thanh cuộn khi cần
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.ScrollBars = ScrollBars.Vertical;

            // Giảm flickering
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi?.SetValue(dgv, true, null);

            dgv.DataBindingComplete += (s, e) =>
            {
                int gridWidth = dgv.ClientSize.Width;
                Dictionary<string, float> columnWeights = new Dictionary<string, float>();

                // Tính toán trọng số
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    float weight = 1.0f; // Mặc định

                    // Xác định trọng số theo kiểu dữ liệu
                    if (column.ValueType == typeof(int) || column.ValueType == typeof(decimal) || column.ValueType == typeof(double))
                        weight = 1.5f;
                    else if (column.ValueType == typeof(DateTime))
                        weight = 2.0f;
                    else if (column.ValueType == typeof(string))
                        weight = 2.5f;

                    // Tăng trọng số cho Salary để rộng hơn
                    if (column.ValueType == typeof(int) || column.ValueType == typeof(decimal) || column.ValueType == typeof(double))
                        weight *= 2.0f;

                    // Tính trọng số dựa trên độ dài nội dung
                    int totalLength = 0, rowCount = dgv.Rows.Count;
                    if (rowCount > 0)
                    {
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            var cellValue = row.Cells[column.Index].Value;
                            if (cellValue != null)
                                totalLength += cellValue.ToString().Length;
                        }
                        float avgLength = (float)totalLength / rowCount;
                        weight *= (avgLength / 10.0f);
                    }


                    if (column.ValueType == typeof(int) || column.ValueType == typeof(decimal) || column.ValueType == typeof(double))
                        weight = Math.Max(weight, 1.5f);

                    columnWeights[column.Name] = weight;
                }

                // Tính tổng trọng số
                float totalWeight = columnWeights.Values.Sum();

                // Gán lại độ rộng cho các cột
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    int minWidth = 40;
                    if (column.ValueType == typeof(int)) minWidth = 50;
                    if (column.ValueType == typeof(decimal) || column.ValueType == typeof(double)) minWidth = 55;
                    column.Width = Math.Max(minWidth, (int)(gridWidth * (columnWeights[column.Name] / totalWeight)));
                }

                // Căn giữa tiêu đề và nội dung
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            };
        }


        // Kích hoạt Double Buffering để giảm lag khi cuộn
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            System.Reflection.PropertyInfo dgvType = dgv.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvType.SetValue(dgv, setting, null);
        }
    }
}
