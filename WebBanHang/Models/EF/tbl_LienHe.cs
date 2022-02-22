namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_LienHe
    {
        [Key]
        public long malienhe { get; set; }
        [DisplayName("Tên liên hệ")]
        [StringLength(50)]
        public string tenlienhe { get; set; }
        [DisplayName("Số điện thoại")]
     //   [Required(ErrorMessage = "Không được để trống trường này")]
      //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public int? sodienthoai { get; set; }
        [DisplayName("Email")]
        [StringLength(100)]
      //  [Required(ErrorMessage = "Không được để trống trường này")]
       // [RegularExpression("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Email không hợp lệ")]
        public string emaillienhe { get; set; }
        [DisplayName("Nội dung")]
        public string noidung { get; set; }
    }
}
