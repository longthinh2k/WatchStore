namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SanPham()
        {
            tbl_BinhLuan = new HashSet<tbl_BinhLuan>();
            tbl_ChiTietDonHang = new HashSet<tbl_ChiTietDonHang>();
            tbl_ChiTietGioHang = new HashSet<tbl_ChiTietGioHang>();
        }

        [Key]
        public long masp { get; set; }

        [StringLength(200)]
        [DisplayName("Tên sản phẩm")]
        public string tensp { get; set; }

        //[StringLength(200)]
        [DisplayName("Giá sản phẩm")]
        public double? giasanpham { get; set; }

        [StringLength(200)]
        [DisplayName("Ảnh sản phẩm 1")]
        public string anhsp1 { get; set; }

        [StringLength(200)]
        [DisplayName("Ảnh sản phẩm 2")]
        public string anhsp2 { get; set; }

        [StringLength(200)]
        [DisplayName("Ảnh sản phẩm 3")]
        public string anhsp3 { get; set; }

        [StringLength(200)]
        [DisplayName("Ảnh sản phẩm 4")]
        public string anhsp4 { get; set; }

        [StringLength(200)]
        [DisplayName("Đánh giá sản phẩm")]
        public string danhgiasanpham { get; set; }

        [StringLength(200)]
        [DisplayName("Mô tả sản phẩm 1")]
        public string motasanpham1 { get; set; }
        [DisplayName("Mô tả sản phẩm 2")]
        public string motasanpham2 { get; set; }
        [DisplayName("Mô tả sản phẩm 3")]
        public string motasanpham3 { get; set; }
        [DisplayName("Mô tả sản phẩm 4")]
        public string motasanpham4 { get; set; }
        [DisplayName("Mã danh mục")]
        public long madanhmuc { get; set; }
        [DisplayName("Lượt xem sản phẩm ")]
        public int? luotxemsanpham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_BinhLuan> tbl_BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTietDonHang> tbl_ChiTietDonHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTietGioHang> tbl_ChiTietGioHang { get; set; }

        public virtual tbl_DanhMucSanPham tbl_DanhMucSanPham { get; set; }
    }
}
