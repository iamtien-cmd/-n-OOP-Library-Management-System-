using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
namespace XuatFile
{
    public static class XuatFile
    {
        public static void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            try
            {
                // Tạo đối tượng saveFileDialog1, SaveFileDialog là hộp thoại trong winform để lưu dữ liệu
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                // Set the title and filter for the dialog
                saveFileDialog1.Title = "Save Excel File";
                saveFileDialog1.Filter = "Excel Files|*.xlsx";

                // If the user clicks OK, continue with the export
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Use the selected file path from the SaveFileDialog
                    filePath = saveFileDialog1.FileName;

                    // Sử dụng thư viện ClosedXML để tạo một đối tượng 'XLWorkbook'
                    using (var workbook = new XLWorkbook())
                    {
                        // Thêm 1 trang tính mới có tên là Sheet1
                        var worksheet = workbook.Worksheets.Add("Sheet1");


                        // Write column headers
                        for (int i = 1; i <= dataGridView.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i).Value = dataGridView.Columns[i - 1].HeaderText;
                        }

                        // Write data
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView.Columns.Count; j++)
                            {
                                // đảm bảo rằng chuỗi sẽ được viết ở excel, nếu null thì k viết
                                // Check for null values before converting to string
                                worksheet.Cell(i + 2, j + 1).Value = dataGridView.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                            }
                        }

                        // Save Excel file
                        workbook.SaveAs(filePath);
                    }

                    MessageBox.Show("Export to Excel successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message);
            }
        }
    }
}
