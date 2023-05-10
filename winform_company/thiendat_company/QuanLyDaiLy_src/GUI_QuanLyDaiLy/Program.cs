using BUS_QuanLyDaiLy;
using DTO_QuanLyDaiLy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace QuanLyDaiLy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TrangChuQuanLy());
            Application.Run(new Login());
            //Application.Run(new baocaocongno());
            //Application.Run(new phieuxuathang());
            //Application.Run(new FormChuongTrinhQLDL());

           
            Console.WriteLine("thiên-------------------------------");

            //int a = DAL_QuanLyTaiKhoan.KiemTraDangNhap("THIENE", "thien123");
            //Console.WriteLine(a);


            //ArrayList arrList = DAL_TiepNhanDL.GetLoaiDL();

            //foreach (DTO_LoaiDL item in arrList)
            //{
            //    Console.WriteLine(item.IdLoaiDL + " " + item.TenLoaiDL);
            //}

            //Console.WriteLine(kq);

            //int kq= DAL_MatHang.XoaMatHang(4);
            // Console.WriteLine(kq);

            // ArrayList al = DAL_DaiLy.DsDaiLy();
            //DateTime a = new DateTime(2018,10,1);
            // int b = DAL_CongNo.ThemCongNo(8, a, 200000, 50000, 0);
            // Console.WriteLine(b);
            //DTO_CongNo cn = new DTO_CongNo(7, a, 4000, 2000, 2000);           
            //int kq=DAL_CongNo.CapNhatCongNo(cn);
            //Console.WriteLine(kq);
            //ArrayList al = DAL_DaiLy.DsDaiLy();
            //foreach (DTO_DaiLy dl in al)
            //{
            //    Console.WriteLine(dl.NgayNhan);
            //}
            //string tenloai = DAL_LoaiDaiLy.GetTenById(1);
            //Console.WriteLine(tenloai);

            //ArrayList al = DAL_DoanhSo.DoanhSo(7);

            //foreach (DTO_DoanhSo ds in al)
            //{
            //    Console.WriteLine(ds.IdDaiLy + "," + ds.SoPhieuXuat + "," + ds.TiLe + "," + ds.TongDoanhSo + "," + ds.Thang);

            //}

            //ArrayList al = DAL_CongNo.CongNo(7);

            //foreach (DTO_CongNo cn in al)
            //{
            //    Console.WriteLine(cn.IdDaiLy +","+ cn.Thang+","+cn.TienNo);

            //}
        }

       

    }
}
