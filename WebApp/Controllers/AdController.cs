using Core.Contracts;
using Core.Models;
using DataAccess.Announcement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAnnoucementService _annoucementService;
        private readonly ILogger<AnnouncementRepository> _logger;

        public AdController(IAnnoucementService annoucementService,ILogger<AnnouncementRepository> logger)
        {
            _annoucementService = annoucementService;
            _logger = logger;   
        }

        [HttpGet]

        public ActionResult<IEnumerable<AnnouncementForList>> Annoucements([Range(0, int.MaxValue)] int offset = 0, [Range(1, 100)] int limit = 3)
        {
            _logger.LogInformation("Getting annoucemenet with offset={offset} and limit={limit}", offset, limit);
            return Ok(_annoucementService.GetAll(offset, limit));
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncementForList>> CreateAd(
            [Required][FromForm] int userId,
            [Required][FromForm] string name,
            [Required][FromForm] string description)
        {
            var createdAd = await _annoucementService.CreateAd(userId, name, description);
            return Ok(createdAd);
        }

        [HttpGet]

        public async Task<ActionResult<AnnouncementForList>> GetAdById([Required] int adId)
        {
            var ad=await _annoucementService.GetById(adId);
            if (ad == null)
            {
                _logger.LogWarning("Can not find the ad with id {userId}", adId);
                return NotFound();
            }

            return Ok(ad);
        }

        [HttpPatch]
        [Route("{adId}")]

        public async Task<ActionResult<AnnouncementForList>> ModifyAd(
            [Required][FromRoute] int adId,
            [Required][FromForm] string name)
        {
            var existingAd=await _annoucementService.GetById(adId);
            if(existingAd == null)
            {
                return NotFound();
            }

            var modifiedAd=await _annoucementService.ModifyNameAd(adId, name);
            return Ok(modifiedAd);  
        }

        [HttpDelete]
        [Route("{adId}")]

        public async Task<ActionResult<AnnouncementForList>> DeleteAd([Required][FromRoute] int adId)
        {
            var existingUser=await _annoucementService.GetById(adId);   
            if( existingUser == null)
            {
                return NotFound();
            }

            await _annoucementService.DeleteAd(adId);
            return NoContent();
        }
    }
}
