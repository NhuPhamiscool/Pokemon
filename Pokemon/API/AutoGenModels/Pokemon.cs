using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.AutoGen
{
    [Table("Pokemon")]
    [Index("Name", IsUnique = true)]
    public partial class Pokemon
    {
        [Key]
        [Column("ID", TypeName = "Pokemon_ID")]
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Species { get; set; } = null!;
        [Column("Average_Height", TypeName = "Meters")]
        public string AverageHeight { get; set; } = null!;
        [Column("Average_Weight", TypeName = "Kilograms")]
        public string AverageWeight { get; set; } = null!;
        [Column("Catch_Rate", TypeName = "Statistic")]
        public string CatchRate { get; set; } = null!;
        [Column("Growth_Rate", TypeName = "Growth_Rates")]
        public string GrowthRate { get; set; } = null!;
        [Column("Experience_Yield")]
        public long ExperienceYield { get; set; }
        [Column("Gender_Ratio", TypeName = "Ratio")]
        public double? GenderRatio { get; set; }
    }
}
