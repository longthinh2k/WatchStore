namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_KinhNghiem
    {
        [Key]
        public long mabaiviet { get; set; }

        [StringLength(50)]
        public string tieude { get; set; }

        public string anh { get; set; }

        public string noidung1 { get; set; }

        public string noidung2 { get; set; }
    }
}
