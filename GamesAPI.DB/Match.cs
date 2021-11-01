using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.DB
{
    public partial class Match
    {
        public Match()
        {
            MatchOdds = new HashSet<MatchOdds>();
        }

        [Key]
        public Guid ID { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        [Required]
        [StringLength(150)]
        public string TeamA { get; set; }
        [Required]
        [StringLength(150)]
        public string TeamB { get; set; }

        /// <summary>
        /// 1=Football, 2=Basketball
        /// </summary>
        public int Sport { get; set; }

        [InverseProperty("Match")]
        public virtual ICollection<MatchOdds> MatchOdds { get; set; }
    }
}
