using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPresenter.Services;

namespace WebPresenter.Controllers {
    [ApiController]
    [Route("data/[controller]")]
    public class PresentationsController : ControllerBase {
        private readonly PresentationsService presentations;

        public PresentationsController(PresentationsService presentations) {
            this.presentations = presentations;
        }

        [HttpGet]
        public IEnumerable<PresentationFundamentals> GetPresentations() {
            return presentations.GetPresentations();
        }

        [HttpGet("by/{ownerName}")]
        public IEnumerable<PresentationFundamentals> GetPresentations_byOwner(string ownerName) {
            return presentations.GetPresentations(ownerName);
        }
        
        [HttpGet("by/{ownerName}/{name}")]
        public IEnumerable<PresentationFundamentals> GetPresentations_byOwnerAndName(string ownerName, string name) {
            return presentations.GetPresentations(name, ownerName);
        }

        [HttpGet("{id}")]
        public Presentation GetPresentation(string id) {
            return presentations.GetPresentation(id);
        }
        
        [HttpGet("{id}/image-presentation")]
        public IEnumerable<string> GetImagePresentation(string id) {
            return presentations.GetPresentation(id).ImagePresentation;
        }

        [HttpPut("{id}/image-presentation")]
        public async Task<IActionResult> UploadImagePresentation(string id, IFormFile imageFile) {
            try {
                await presentations.GetPresentation(id).SetImagePresentation(imageFile);
                return Ok();
            }
            catch (Exception e) {
                Console.Error.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult StartPresentation(PresentationFundamentals fundamentals) {
            string presentationId = presentations.StartPresentation(fundamentals);

            if (presentationId == "") {
                return BadRequest();
            }
            
            return Ok(Content(presentationId));
        }

        [HttpDelete("{id}")]
        public void EndPresentation(string id) {
            presentations.EndPresentation(id);
        }
    }
}