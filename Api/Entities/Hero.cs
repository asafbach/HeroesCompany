using System;

namespace Api.Entities
{

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ability { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTrainingDate { get; set; } = DateTime.MinValue;
        public DateTime SessionStartDate {get;set;}
        public int TrainingPerSession {get;set;}
        public string Suit { get; set; }
        public decimal StartingPower { get; set; }
        public decimal CurrentPower { get; set; }

        // public string Train(){
        //     var currentDate = DateTime.Now;
        //     var minutes = (currentDate - SessionStartDate).TotalMinutes;
        //     if(minutes < 1440 && TrainingPerSession == 5){
        //         var hours = minutes%60;
        //         minutes = hours* 60 - minutes;
        //         return $"Sorry but you need to wait {hours} hours and minutes {minutes} until next training";
        //     }
        //     else if(minutes > 1440){
        //         SessionStartDate = DateTime.Now;
        //         TrainingPerSession = 0;
        //     }
        //     TrainingPerSession ++;
        //     var rand = new Random().Next(1,10);
        //     decimal increasePercent = (decimal)rand/100;
        //     CurrentPower *= (1 + increasePercent);
        //     return $"Hero got stronger by {increasePercent} percent!";
        // }
    }
}