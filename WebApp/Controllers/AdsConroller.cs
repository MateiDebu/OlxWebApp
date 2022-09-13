using Core.Contracts;
using Core.Models;
using DataAccess.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsConroller : ControllerBase
    {
        private readonly IAdsService _adsService;

        public AdsConroller(IAdsService adsService)
        {
            _adsService = adsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ad>> GetAllAds([Range(0,int.MaxValue)] int offset=0, [Range(0,100)] int limit=20)
        {
            var ads=_adsService.GetAllAds(offset,limit);
            return Ok(ads.Select(p=>new AdForList(p)));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ad>> CreateAd([Required][FromForm] string name, [Required][FromForm] string description)
        {
            string userIdString = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userId=int.Parse(userIdString);

            var createdAd = await _adsService.CreateAd(userId,name,description);
            return Ok(new AdForList(createdAd));
        }

    }
}
