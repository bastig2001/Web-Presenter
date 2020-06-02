using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebPresenter.Models;
using WebPresenter.Services;

namespace WebPresenter.Controllers {
    [ApiController]
    [Route("data/[controller]")]
    public class PresentationDataController : ControllerBase {
        private readonly PresentationDataService data;

        public PresentationDataController(PresentationDataService data) {
            this.data = data;
        }
        
        [HttpGet]
        public IEnumerable<PresentationData> GetPresentations() {
            return data.GetPresentations();
        }

        [HttpGet("{ownerName}")]
        public IEnumerable<PresentationData> GetPresentations_byOwner(string ownerName) {
            return data.GetPresentations(ownerName);
        }

        [HttpGet("{ownerName}/{name}")]
        public PresentationData GetPresentation(string ownerName, string name) {
            return data.GetPresentation(name, ownerName);
        }

        [HttpPost]
        public IActionResult CreatePresentation(PresentationFundamentals fundamentals) {
            if (data.CreatePresentation(fundamentals)) {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{ownerName}/{name}")]
        public IActionResult RemovePresentation(string ownerName, string name) {
            if (data.RemovePresentation(name, ownerName)) {
                return Ok();
            }

            return BadRequest();
        }
    }
}