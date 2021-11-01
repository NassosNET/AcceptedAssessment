using System;
using System.Collections.Generic;

namespace GamesAPI.DTOs
{
    public partial class MatchOddsDto
    {
        public Guid ID { get; set; }
        public Guid MatchId { get; set; }
        public string Specifier { get; set; }
        public decimal Odd { get; set; }
        public virtual MatchDto Match { get; set; }
    }
}
