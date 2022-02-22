namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DanhMucSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DanhMucSanPham()
        {
            tbl_SanPham = new HashSet<tbl_SanPham>();
        }

        [Key]
        public long madanhmuc { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Tên danh mục")]
        public string tendanhmuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SanPham> tbl_SanPham { get; set; }
    }
}
