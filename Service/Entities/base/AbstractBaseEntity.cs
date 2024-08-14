using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.@base
{
    public abstract class AbstractBaseEntity
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("active_flag"), DefaultValue(true)]
        public bool ActiveFlag { get; set; } = true;

        [Column("delete_flag"), DefaultValue(false)]
        public bool DeleteFlag { get; set; } = false;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("created_by")]
        public string CreatedBy { get; set; } = string.Empty;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Column("updated_by")]
        public string UpdatedBy { get; set; } = string.Empty;

    }
}
