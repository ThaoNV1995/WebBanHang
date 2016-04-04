namespace MayTinhDaiPhat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhThucThanhToan")]
    public partial class HinhThucThanhToan
    {
        [Key]
        public int MaHTTT { get; set; }

        [StringLength(100)]
        public string TenHTTT { get; set; }
    }
}
