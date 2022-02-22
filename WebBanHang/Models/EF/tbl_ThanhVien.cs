namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_ThanhVien()
        {
            tbl_KhachHang = new HashSet<tbl_KhachHang>();
        }

        [Key]
        public long MaThanhVien { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Không được để trống trường này")]
        [DisplayName("Tài Khoản")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Không được để trống trường này")]
        [DisplayName("Mật Khẩu")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Không được để trống trường này")]
        [DisplayName("Họ Tên")]
        [StringLength(100)]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Không được để trống trường này")]
        [DisplayName("Địa Chỉ")]
        [StringLength(100)]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Không được để trống trường này")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        [DisplayName("Số Điện Thoại")]
        [StringLength(100)]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Không được để trống trường này")]
        [RegularExpression("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Email không hợp lệ")]
        [DisplayName("Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_KhachHang> tbl_KhachHang { get; set; }
    }
}
