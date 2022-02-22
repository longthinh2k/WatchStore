namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_KhachHang()
        {
            tbl_BinhLuan = new HashSet<tbl_BinhLuan>();
            tbl_Donhang = new HashSet<tbl_Donhang>();
            tbl_GioHang = new HashSet<tbl_GioHang>();
        }

        [Key]
        public long makhachhang { get; set; }

        [StringLength(50)]
        public string tenkhachhang { get; set; }

        public int? sdt { get; set; }

        [StringLength(100)]
        public string diachi { get; set; }

        [StringLength(100)]
        public string gmail { get; set; }

        public long? MaThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_BinhLuan> tbl_BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Donhang> tbl_Donhang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GioHang> tbl_GioHang { get; set; }

        public virtual tbl_ThanhVien tbl_ThanhVien { get; set; }
    }
}
