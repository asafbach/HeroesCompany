

using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class HeroDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ability { get; set; }

        public int TrainerId { get; set; }
        public DateTime StartTrainingDate { get; set; } = DateTime.MinValue;
        private DateTime SessionStartDate;
        private int TrainingPerSession;

        public string Suit { get; set; }
        public decimal StartingPower { get; set; }
        public decimal CurrentPower { get; set; }


    }
}