using System;
using System.Collections.Generic;

namespace BTLWeb.Models;

public partial class TChiTietSanPham
{
    public string MaChiTietSp { get; set; } = null!;

    public string? MaSp { get; set; }

    public string? MaKtcamBien { get; set; }

    public string? MaMauSac { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? Video { get; set; }

    public double? DonGiaBan { get; set; }

    public double? GiamGia { get; set; }

    public int? Slton { get; set; }

    public virtual TKichThuocCamBien? MaKtcamBienNavigation { get; set; }

    public virtual TMauSac? MaMauSacNavigation { get; set; }

    public virtual TDanhMucSp? MaSpNavigation { get; set; }

    public virtual ICollection<TAnhChiTietSp> TAnhChiTietSps { get; } = new List<TAnhChiTietSp>();

    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; } = new List<TChiTietHdb>();
}
