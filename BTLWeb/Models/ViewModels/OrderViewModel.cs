using System.ComponentModel.DataAnnotations;

namespace BTLWeb.Models.ViewModels
{
    public class OrderViewModel
    {
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string TenKhachHang { set; get; }

        [Required]
        [MaxLength(256)]
        public string SoDienThoai { set; get; }

        [Required]
        [MaxLength(256)]
        public string DiaChi { set; get; }

        [Required]
        [MaxLength(256)]
        public string GhiChu { set; get; }

        [MaxLength(256)]
        public string PhuongThucThanhToan { set; get; }

        public DateTime? NgayHoaDon { set; get; }
        public bool TrangThai { set; get; }

        [MaxLength(128)]
        public string MaKhachHang { set; get; }


        public IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }
    }
}
