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
        public ActionResult<IEnumerable<Ad>> GetAllAds([Range(0,int.MaxValue)] int offset=0, [Range(0,100)] int limit=10)
        {
            var ads=_adsService.GetAllAds(offset,limit);
            return Ok(ads.Select(a=>new AdForList(a)));
        }

        [HttpGet]
        [Route("userId")]
        [Authorize]
        public ActionResult<IEnumerable<Ad>> GetAllAdsForUser([Range(0, int.MaxValue)] int offset = 0, [Range(0, 100)] int limit = 5)
        {
            string userIdString=User.Claims.First(c=>c.Type==ClaimTypes.NameIdentifier).Value;
            int userId=int.Parse(userIdString);

            var ads = _adsService.GetAllAdsForUser(userId, offset, limit);
            return Ok(ads.Select(a=>new AdForList(a)));
        }

        [HttpGet]
        [Route("{adId}")]
        public async Task<ActionResult<AdForList>> GetAdById([Required] int adId)
        {
            var ad = await _adsService.GetById(adId);
            if (ad == null)
            {
                return NotFound();
            }
            return Ok(new AdForList(ad));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ad>> CreateAd([Required][FromForm] string name, [Required][FromForm] string description)
        {
            string userIdString = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userId = int.Parse(userIdString);

            var createdAd = await _adsService.CreateAd(userId,name,description);
            return Ok(new AdForList(createdAd));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<AdForList>> DeleteAd([Required][FromForm] int adId)
        {
            var existingAd=await _adsService.GetById(adId);
            if (existingAd == null)
            {
                return NotFound();
            }
            await _adsService.DeleteAd(adId);
            return NoContent();
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<AdForList>> ModifyUser(
            [Required][FromForm] int id, 
            [Required][FromForm] string name, 
            [Required][FromForm] string description)
        {
            var existingAd=await _adsService.GetById(id);
            if (existingAd == null)
            {
                return NotFound();
            }

            var modifiedAd=await _adsService.UpdateAd(id,name,description);
            return Ok(new AdForList(modifiedAd));
        }
    }
}
