using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Authorize]
    public class HeroesController : BaseApiController
    {
        private readonly DataContext _context;

        public HeroesController(DataContext context, ILogger<User> logger)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes()
        {
            var userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Users.Where(user => user.Email == userMail).SingleOrDefaultAsync();
            if(user == null){
                return NotFound();
            }
            var heroes =  await _context.Heroes.Where(hero => hero.TrainerId == user.Id).ToListAsync();
            if(heroes == null){
                return NotFound(); 
            }
            return Ok(heroes);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Hero>>> GetAllHeroes()
        {
            var heroes =  await _context.Heroes.ToListAsync();
            if(heroes == null){
                return NotFound(); 
            }
            return Ok(heroes);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Hero>> Create(Hero hero)
        {
            if (await HeroNameExists(hero.Name)) return BadRequest("Name allready exist");
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(hero);
        }

        [HttpPut("train/{id}")]
        public async Task<ActionResult<HeroResDto>> Train(int id)
        {
            var hero =  await _context.Heroes
                .Where(hero => hero.Id == id)
                .SingleOrDefaultAsync();
            if(hero == null )return NotFound();
            var res = TrainHero(hero);
            await _context.SaveChangesAsync();
            return Ok(new HeroResDto{
                response = res,
                hero = hero
            });
           
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<HeroResDto>> DeletHero(int id)
        {
            var hero =  await _context.Heroes
                .Where(hero => hero.Id == id)
                .SingleOrDefaultAsync();
                if(hero == null )return NotFound();
                _context.Heroes.Remove(hero);
                await _context.SaveChangesAsync();
                return Ok(new HeroResDto{
                    response = "Hero has been deleted",
                    hero = hero
                });
        }

        private async Task<bool> HeroNameExists(string name)
        {
            return await _context.Heroes.AnyAsync(hero => hero.Name == name.ToLower());
        }

        private string TrainHero(Hero hero){
            var currentDate = DateTime.Now;
            if(hero.StartTrainingDate == DateTime.MinValue){
                hero.StartTrainingDate = currentDate;
            }
            var hours = (currentDate - hero.SessionStartDate).TotalHours;
            if(hours < 24 && hero.TrainingPerSession >= 5){
                return $"Sorry but you need to wait {24 - Math.Round(hours)} hours until next training";
            }
            else if(hours > 24){
                hero.SessionStartDate = DateTime.Now;
                hero.TrainingPerSession = 0;
            }
            hero.TrainingPerSession ++;
            var rand = new Random().Next(1,10);
            decimal increasePercent = (decimal)rand/100;
            hero.CurrentPower *= (1 + increasePercent);
            return $"Hero got stronger by {rand} percent!";
        }
    }
}