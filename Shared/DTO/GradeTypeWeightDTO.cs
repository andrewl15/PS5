using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOOR.Shared.DTO
{
    public class GradeTypeWeightDTO
    {
        public int SchoolId { get; set; }
        public int SectionId { get; set; }
        [StringLength(2)]
        public string GradeTypeCode { get; set; } = null!;
        public byte NumberPerSection { get; set; }
        public byte PercentOfFinalGrade { get; set; }
        public bool DropLowest { get; set; }
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
    }
}
