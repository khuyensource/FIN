using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaricoPay
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Printed();
        }

        private void Printed()
        {
            try
            {
                if (Session["MaHDPrinte"] != null)
                {
                    BWInCacNghiepVuKho objNVK = new BWInCacNghiepVuKho();
                    DataSet ds = new DataSet();
                    //Trung Tam PP
                    ds = objNVK.GetTrungTamPP(ref error, Session["Sitecode"].ToString());
                    lblTrungTamPP.Text = ds.Tables[0].Rows[0]["sitename"].ToString();
                    lblDiaChi.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    lblTelFax.Text = ds.Tables[0].Rows[0]["TelFax"].ToString();

                    BWNhapHoaDonBanHang obj = new BWNhapHoaDonBanHang();
                    DataTable dtHoDon = obj.dsInfoHoaDonChuaIn(ref error, Session["MaHDPrinte"].ToString()).Tables[0];
                    if (dtHoDon.Rows[0]["so"].ToString() == "3")
                    {
                        lblTitle.Text = "Lệnh Xuất Hàng (Có Chiết Khấu)";
                    }
                    else
                    {
                        lblTitle.Text = "Lệnh Xuất Hàng";
                    }
                    lblTenTVV.Text = dtHoDon.Rows[0]["MaTVV"].ToString() + "_" + dtHoDon.Rows[0]["TenTVV"].ToString();
                    lblDienThoai.Text = dtHoDon.Rows[0]["DienThoai"].ToString();
                    lblSoDonHang.Text = dtHoDon.Rows[0]["MaHD"].ToString();
                    lblNgayDonHang.Text = dtHoDon.Rows[0]["NgayHD"].ToString();
                    lbGhiChu.Text = dtHoDon.Rows[0]["PayNo"].ToString();
                    double dTienChuaCK = double.Parse(dtHoDon.Rows[0]["TienHD"].ToString());

                    lblTienChuaChietKhau.Text = String.Format("{0:###,###}", dTienChuaCK);

                    //PhieuThu
                    lblTenTVVPhieuThu.Text = dtHoDon.Rows[0]["TenTVV"].ToString();
                    lblSoTien.Text = "";

                    DataTable dt = obj.dsChiTietHoaDonPrint(ref error, Session["MaHDPrinte"].ToString()).Tables[0];
                    gvListChiTietHoaDon.DataSource = dt;
                    gvListChiTietHoaDon.DataBind();
                    HienThiGiaNTD();
                    double dTienKhuyenMaiBangTien = 0;
                    double dTongTienNTD = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ThanhTien"].ToString() != "")
                        {
                            double dTienTemp = double.Parse(dt.Rows[i]["ThanhTien"].ToString());
                            if (dTienTemp < 0)
                            {
                                dTienKhuyenMaiBangTien = dTienKhuyenMaiBangTien + dTienTemp;
                            }
                        }
                        dTongTienNTD = dTongTienNTD + cls.cToDouble(dt.Rows[i]["ThanhTienNTD"]);
                    }
                    lblTienConPhaiThanhToan.Text = String.Format("{0:###,###}", dTienChuaCK);
                    lblKhuyenMaiBangTien.Text = String.Format("{0:###,###}", System.Math.Abs(dTienKhuyenMaiBangTien));
                    lblSoTien.Text = String.Format("{0:###,###}", dTienChuaCK);
                    //lblSoTien.Text = String.Format("{0:###,###}", dTienChuaCK + dTienKhuyenMaiBangTien);
                    lblSoTienNTD.Text = String.Format("{0:###,###}", dTongTienNTD);

                    PrintHelper.PrintWebControl(pnlPrintHoaDon);
                }
                else
                {
                    lblChuKyKeToan.Text = "Looix";
                }
            }
            catch (Exception ex)
            {
                lblChuKyKeToan.Text = error + " Lỗi: " + ex;
                return;
            }
        }
    }
}