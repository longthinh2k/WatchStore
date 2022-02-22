namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Donhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Donhang()
        {
            tbl_ChiTietDonHang = new HashSet<tbl_ChiTietDonHang>();
        }

        [Key]
        public long madonhang { get; set; }

        public long makhachhang { get; set; }
        [DisplayName("Ngày đặt hàng")]
        public DateTime? ngaydathang { get; set; }

        [StringLength(50)]
        [DisplayName("Hình thức đặt hàng")]
        public string trangthai { get; set; }
        [DisplayName("Tổng tiền")]
        public double? tongtien { get; set; }
        [DisplayName("Địa chỉ")]
        [StringLength(100)]
        public string diachi { get; set; }

        [DisplayName("Số điện thoại")]
        public int? sdt { get; set; }

        [StringLength(100)]
        [DisplayName("Mã giảm giá")]
        public string magiamgia { get; set; }

        [StringLength(50)]
        [DisplayName("Tên khách hàng")]
        public string tenkhachhang { get; set; }

        [StringLength(50)]
        [DisplayName("Trạng thái")]
        public string trangthaidathang { get; set; }
        [DisplayName("Xóa")]
        public int? IsDaxoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTietDonHang> tbl_ChiTietDonHang { get; set; }

        public virtual tbl_KhachHang tbl_KhachHang { get; set; }
    }
}
