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
using BUS_QuanLyDaiLy;
using DTO_QuanLyDaiLy;
using System.Collections;
using Word = Microsoft.Office.Interop.Word;
using Size = System.Drawing.Size;
using System.IO;

namespace QuanLyDaiLy
{
    public partial class PhieuXuat : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable tb;
        private DTO_ThongTinTaiKhoan taiKhoan;
        private DTO_NhanVienQuanLy nhanVien;
        private ArrayList dsDaiLy;
        private int[] dsIdDL;
        private float tongTien;
        public PhieuXuat()
        {
            InitializeComponent();
        }
        public PhieuXuat(DataTable Tb, DTO_ThongTinTaiKhoan tk, DTO_NhanVienQuanLy nv)
        {
            InitializeComponent();
            this.tb = Tb;
            taiKhoan = tk;
            nhanVien = nv;
          
            Load();
        }

        private void Load()
        {
            tongTien = 0;
            //tb = new DataTable();
            dsDaiLy = BUS_DaiLy.DsDaiLy();
            dsIdDL = new int[dsDaiLy.Count];

            foreach (DataRow r in tb.Rows)
            {
                tongTien = tongTien +(float)Convert.ToDouble(r[5].ToString());
            }
            int i = 0;
            foreach(DTO_DaiLy dl in dsDaiLy)
            {
                cbDaiLy.Items.Add(dl.TenDaiLy);
                dsIdDL[i] = dl.IdDL;
                i++;
            }
            cbDaiLy.SelectedIndex = 0;
            gvhang.DataSource = tb;
            txtTongTien.Text = tongTien.ToString();


            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void gvhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            if (hang < tb.Rows.Count)
            {
               
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            int idDaiLy = dsIdDL[cbDaiLy.SelectedIndex];
            DateTime ngayXuat = dtNgayLap.Value;
            //Console.WriteLine(ngayXuat.ToString("yyyy-MM-dd"));
            float tienCon = (float)Convert.ToDouble(txtTienCon.Text);
            float tienNo = BUS_DaiLy.GetTienNo(idDaiLy);
            float tienNoMoi = tienCon + tienNo;
            int kqUpdate = BUS_DaiLy.UpdateTienNo(idDaiLy, tienNoMoi);
            if (kqUpdate == 1)
            {
                string cmnd = taiKhoan.Cmnd;
                DTO_PhieuXuatHang phieuXuatHang = new DTO_PhieuXuatHang(0, ngayXuat, idDaiLy, cmnd);
                int idPhieuXuat = BUS_PhieuXuat.ThemPhieuXuatGetId(phieuXuatHang);
                foreach (DataRow r in tb.Rows)
                {
                    DTO_ChiTietXuat ctx = new DTO_ChiTietXuat(idPhieuXuat, (int)r[0], (int)r[2], (float)r[4], r[3].ToString(), (float)r[5]);
                    BUS_ChiTietXuat.ThemChiTietXuat(ctx);
                }
            }
            else
            {

            }


        }

        private void txtSoTienTra_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gvhang_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            if (hang < tb.Rows.Count)
            {

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTienCon.Text.Equals("") == false)
            {
                int idDaiLy = dsIdDL[cbDaiLy.SelectedIndex];
                DateTime ngayXuat = dtNgayLap.Value;
                //Console.WriteLine(ngayXuat.ToString("yyyy-MM-dd"));
                float tienCon = (float)Convert.ToDouble(txtTienCon.Text);
                float tienNo = BUS_DaiLy.GetTienNo(idDaiLy);
                Console.WriteLine(tienNo);
                float tienNoMoi = tienCon + tienNo;
                int kqUpdate = BUS_DaiLy.UpdateTienNo(idDaiLy, tienNoMoi);
                if (kqUpdate == 1)
                {
                    string cmnd = taiKhoan.Cmnd;
                    DTO_PhieuXuatHang phieuXuatHang = new DTO_PhieuXuatHang(0, ngayXuat, idDaiLy, cmnd);
                    int idPhieuXuat = BUS_PhieuXuat.ThemPhieuXuatGetId(phieuXuatHang);
                    foreach (DataRow r in tb.Rows)
                    {
                        int idMatHang = (int)r[0];
                        int soLuong =int.Parse(r[2].ToString());
                        float donGia = (float)Convert.ToDouble(r[4]);
                        string donViTinh = r[3].ToString();
                        float thanhTien = (float)Convert.ToDouble(r[5]);
                        // DTO_ChiTietXuat ctx = new DTO_ChiTietXuat(idPhieuXuat, (int)r[0], (int)r[2], (float)Convert.ToDouble(r[4]), r[3].ToString(), (float)Convert.ToDouble(r[5]));
                        DTO_ChiTietXuat ctx = new DTO_ChiTietXuat(idPhieuXuat,idMatHang, soLuong, donGia,donViTinh, thanhTien);
                        BUS_ChiTietXuat.ThemChiTietXuat(ctx);
                    }
                    MessageBox.Show("Xuất thành công");
                }else if (kqUpdate == 2)
                {
                    MessageBox.Show("Số tiền còn lại cộng với nợ cũ vượt quy định");
                }
                else
                {
                    MessageBox.Show("Xuất thất bại");
                }

                //tb.Columns.Add("Id", typeof(int));
                //tb.Columns.Add("Mặt hàng", typeof(string));
                //tb.Columns.Add("Số lượng", typeof(string));
                //tb.Columns.Add("đơn vị tính", typeof(string));
                //tb.Columns.Add("Đơn giá", typeof(string));
                //tb.Columns.Add("Thành tiền", typeof(string));
            }

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = "*.docx";
            savefile.Filter = "DOCX files(*.docx)|*.docx";

            if (savefile.ShowDialog() == DialogResult.OK && savefile.FileName.Length > 0)
            {
                Export_Data_To_Word(gvhang, savefile.FileName);
                MessageBox.Show("File saved!", "Message Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;


                ///////   ĐÂY LÀ SAVE BÌNH THƯỜNG CHƯA CĂN CHỈNH

                /*Document oDoc = new Document();
                oDoc.Application.Visible = true;

                oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

                //dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                Object oMissing = System.Reflection.Missing.Value;
               for (int r = 0; r <= RowCount - 1; r++)
               {
                   oTemp = "";
                   for (int c = 0; c < ColumnCount - 1; c++)
                   {
                       oTemp = oTemp + DGV.Rows[r].Cells[c].Value + "\t";
                   }
                   var oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                   oPara1.Range.Text = oTemp;
                   oPara1.Range.InsertParagraphAfter();
                   byte[] imgbyte = (byte[])dataGridViewPrint.Rows[r].Cells[7].Value;
                   MemoryStream ms = new MemoryStream(imgbyte);
                   //Image sparePicture = Image.FromStream(ms);
                   Image finalPic = (Image)new Bitmap(Image.FromStream(ms), new System.Drawing.Size(50, 50));
                   Clipboard.SetDataObject(finalPic);
                   var oPara2 = oDoc.Content.Paragraphs.Add(ref oMissing);
                   oPara2.Range.Paste();
                   oPara2.Range.InsertParagraphAfter();
                   //oTemp += "\n";
               }
               //save the file
               oDoc.SaveAs2(filename);*/
                //////////////////////////////////////////////////////////////////////////////////////


                //////        ĐÂY LÀ SAVE CÓ CHIA CỘT THẲNG HÀNG       

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
                    byte[] imgbyte = (byte[])gvhang.Rows[r].Cells[3].Value;
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

            private void txtSoTienTra_EditValueChanged_1(object sender, EventArgs e)
        {
            if(txtSoTienTra.Text.Equals("") == false && txtTongTien.Text.Equals("") == false)
            {
                float tongTien = (float)Convert.ToDouble(txtTongTien.Text);
                float tienTra = (float)Convert.ToDouble(txtSoTienTra.Text);
                if (txtSoTienTra.Text.Equals("") == false && tienTra <= tongTien)
                {
                    float tiencon = tongTien - tienTra;
                    txtTienCon.Text = tiencon.ToString();
                }
            }
            else
            {
                txtTienCon.Text = "";
            }
            
        }
    }
}
