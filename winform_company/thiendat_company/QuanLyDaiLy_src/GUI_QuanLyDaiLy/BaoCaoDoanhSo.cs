using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using BUS_QuanLyDaiLy;
using DTO_QuanLyDaiLy;
using DevExpress.XtraPrinting.Native;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Size = System.Drawing.Size;

namespace QuanLyDaiLy
{
    public partial class BaoCaoDoanhSo : DevExpress.XtraEditors.XtraUserControl
    {
        private ArrayList doanhSo;
        private DataTable tb=new DataTable();
        int id = -1;
        public BaoCaoDoanhSo()
        {
            InitializeComponent();
            tb.Columns.Add("ID", typeof(int));
            tb.Columns.Add("Đại lý", typeof(string));
            tb.Columns.Add("Số phiếu xuất", typeof(int));
            tb.Columns.Add("Tổng giá trị", typeof(float));
            tb.Columns.Add("tỷ lệ", typeof(string));
            Load();
        }

        private void Load()
        {
            //doanhSo= BUS_BaoCaoDoanhSo.GetDoanhSo(1);

            int[] dsThang= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            cbThang.DataSource = dsThang;
            gvDoanhSo.DataSource = tb;
            gvDoanhSo.Columns["Id"].Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb.Clear();
         
            if (cbThang.SelectedIndex!=-1){
                int thang = int.Parse(cbThang.SelectedItem.ToString());
                doanhSo = BUS_BaoCaoDoanhSo.GetDoanhSo(thang);
                foreach (DTO_DoanhSo dso in doanhSo)
                {
                    string tendl = BUS_DaiLy.GetTenById(dso.IdDaiLy);
                    tb.Rows.Add(dso.IdDaiLy, tendl, dso.SoPhieuXuat, dso.TongDoanhSo, dso.TiLe+"%");
                }
                gvDoanhSo.DataSource = tb;

            }
            
        }

        private void cbThang_SelectedValueChanged(object sender, EventArgs e)
        {
           // Console.WriteLine(cbThang.SelectedItem);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
 
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.DefaultExt = "*.docx";
                savefile.Filter = "DOCX files(*.docx)|*.docx";

                if (savefile.ShowDialog() == DialogResult.OK && savefile.FileName.Length > 0)
                {
                    Export_Data_To_Word(gvDoanhSo, savefile.FileName);
                    MessageBox.Show("File saved!", "Message Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }


            public void Export_Data_To_Word(DataGridView DGV, string filename)
            {
                if (DGV.Rows.Count != 0)
                {
                    int RowCount = DGV.Rows.Count;
                    int ColumnCount = DGV.Columns.Count;



                    Object[,] dataArray = new object[RowCount + 1, ColumnCount + 1];
                    //add rows
                    int r = 0;
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        for (r = 0; r <= RowCount - 1; r++)
                        {
                            dataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                        }
                    }
                    Word.Document oDoc = new Word.Document();
                    oDoc.Application.Visible = true;
                    //page origiantion
                    oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                    dynamic oRange = oDoc.Content.Application.Selection.Range;
                    string oTemp = "";
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        for (int c = 0; c <= ColumnCount - 1; c++)
                        {
                            oTemp = oTemp + dataArray[r, c] + "\t";
                        }
                    }
                    //table format
                    oRange.Text = oTemp;
                    object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                    object ApplyBorders = true;
                    object AutoFit = true;
                    object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                    oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                           Type.Missing, Type.Missing, ref ApplyBorders,
                                           Type.Missing, Type.Missing, Type.Missing,
                                           Type.Missing, Type.Missing, Type.Missing,
                                           Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);
                    oRange.Select();
                    oDoc.Application.Selection.Tables[1].Select();
                    oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                    oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                    oDoc.Application.Selection.Tables[1].Rows[1].Select();
                    oDoc.Application.Selection.InsertRowsAbove(1);
                    oDoc.Application.Selection.Tables[1].Rows[1].Select();
                    //header row style
                    oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                    oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                    oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 20;
                    string ngay = "";
                    ngay = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + DateTime.Now + "";
                    Object oMissing2 = System.Reflection.Missing.Value;
                    var oPara2 = oDoc.Content.Paragraphs.Add(ref oMissing2);
                    oPara2.Range.Text = ngay;
                    oPara2.Range.InsertParagraphAfter();
                    //add header row manually
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                    }
                    //table sytle
                    oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                    oDoc.Application.Selection.Tables[1].Rows[1].Select();
                    oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //header text
                    foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                    {
                        Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                        headerRange.Text = "BÁO CÁO DOANH SỐ\n";
                        headerRange.Font.Size = 20;
                        headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                    //save image
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        byte[] imgbyte = (byte[])gvDoanhSo.Rows[r].Cells[3].Value;
                        MemoryStream ms = new MemoryStream(imgbyte);
                        Image finalPic = (Image)(new Bitmap(Image.FromStream(ms), new Size(70, 70)));
                        Clipboard.SetDataObject(finalPic);
                        oDoc.Application.Selection.Tables[1].Cell(r + 2, 4).Range.Paste();
                        oDoc.Application.Selection.Tables[1].Cell(r + 2, 4).Range.InsertParagraph();
                    }
                    //save file
                    oDoc.SaveAs(filename);
                }
            }


        }// main
    }

