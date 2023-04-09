using System;
using System.Collections.Generic;

namespace BTLWeb.Models;

public partial class TKichThuocCamBien
{
    public string MaKtcamBien { get; set; } = null!;

    public string? KichThuoc { get; set; }

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; } = new List<TChiTietSanPham>();
}
