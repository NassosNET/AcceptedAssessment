using System;
using System.Collections.Generic;

namespace GamesAPI.DTOs
{
    public partial class MatchDto
    {
        public MatchDto()
        {
            MatchOdds = new List<MatchOddsDto>();
        }
        public Guid ID { get; set; }
        public string Description { get; set; }
        public DateTime MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }

        /// <summary>
        /// 1=Football, 2=Basketball
        /// </summary>
        public int Sport { get; set; }
        public virtual List<MatchOddsDto> MatchOdds { get; set; }
    }
}
