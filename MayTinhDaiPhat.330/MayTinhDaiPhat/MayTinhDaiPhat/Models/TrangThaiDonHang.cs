namespace MayTinhDaiPhat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThaiDonHang")]
    public partial class TrangThaiDonHang
    {
        [Key]
        public int MaTTDH { get; set; }

        [StringLength(50)]
        public string TenTTDH { get; set; }
    }
}
