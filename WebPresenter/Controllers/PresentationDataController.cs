using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<PresentationFundamentals> GetPresentations() {
            return data.GetPresentations().Select(presentation => new PresentationFundamentals(presentation));
        }

        [HttpGet("{ownerName}")]
        public IEnumerable<PresentationFundamentals> GetPresentations_byOwner(string ownerName) {
            return data.GetPresentations(ownerName).Select(presentation => new PresentationFundamentals(presentation));
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

        [HttpPut]
        public IActionResult SavePresentation(PresentationData presentation) {
            if (data.SavePresentation(presentation)) {
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