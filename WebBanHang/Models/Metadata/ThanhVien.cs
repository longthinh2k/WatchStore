using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.Metadata
{
    [MetadataTypeAttribute(typeof(ThanhVien))]
    public partial class tbl_ThanhVien
    {
        internal sealed class ThanhVien
        { 
            public long MaThanhVien { get; set; }
            //TaiKhoan
            [Column(TypeName = "ntext")]
            [Required(ErrorMessage = "Không được để trống trường này")]
            public string TaiKhoan { get; set; }
            //MatKhau
            [Column(TypeName = "ntext")]
            [Required(ErrorMessage = "Không được để trống trường này")]
            public string MatKhau { get; set; }
            //HoTen
            [Column(TypeName = "ntext")]
            [Required(ErrorMessage = "Không được để trống trường này")]
            public string HoTen { get; set; }
            //DiaChi
            [Column(TypeName = "ntext")]
            [Required(ErrorMessage = "Không được để trống trường này")]
            public string DiaChi { get; set; }
            //SoDienThoai
            [Column(TypeName = "ntext")]
            [Required(ErrorMessage = "Không được để trống trường này")]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Số điện thoại không hợp lệ")]
            public string SoDienThoai { get; set; }
            //Email
            [Column(TypeName = "ntext")]
            [Required(ErrorMessage = "Không được để trống trường này")]
            [RegularExpression("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; }
        }
    }
}