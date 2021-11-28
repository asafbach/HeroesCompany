

using System;
using Api.Entities;

namespace Api.Dtos
{
    public class HeroResDto
    {
        public string response { get; set; }
        public string[] errors { get; set; }
        public Hero hero { get; set; }
        public Hero[] heroes { get; set; }


    }
}