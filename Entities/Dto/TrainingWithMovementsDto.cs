using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class TrainingWithMovementsDto
    {
        public long TrainingId { get; set; }
        public long MovementId { get; set; }
        public int TrainingTime { get; set; }
        public int Difficulty { get; set; }
        public string TrainingName { get; set; }
        public string Region { get; set; }
        public List<Movement> Movements { get; set; } = new List<Movement>();
    }
}
