using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.DB
{
    public partial class MatchOdds
    {
        [Key]
        public Guid ID { get; set; }
        public Guid MatchId { get; set; }
        [Required]
        [StringLength(150)]
        public string Specifier { get; set; }
        [Column(TypeName = "decimal(7, 4)")]
        public decimal Odd { get; set; }

        [ForeignKey(nameof(MatchId))]
        [InverseProperty("MatchOdds")]
        public virtual Match Match { get; set; }
    }
}
