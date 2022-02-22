namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_GioHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_GioHang()
        {
            tbl_ChiTietGioHang = new HashSet<tbl_ChiTietGioHang>();
        }

        [Key]
        public long magiohang { get; set; }

        public long makhachhang { get; set; }

        public double? tongtien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTietGioHang> tbl_ChiTietGioHang { get; set; }

        public virtual tbl_KhachHang tbl_KhachHang { get; set; }
    }
}
