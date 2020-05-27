using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPresenter.Models;
using WebPresenter.Services;

namespace WebPresenter.Controllers {
    [ApiController]
    [Route("controllers/[controller]")]
    public class PresentationsController : ControllerBase {
        private readonly PresentationsService presentations;
        private readonly PresentationDataService data;

        public PresentationsController(PresentationsService presentations, PresentationDataService data) {
            this.presentations = presentations;
            this.data = data;
        }

        [HttpGet("data")]
        public IEnumerable<PresentationData> GetAllPresentationData() {
            return data.GetPresentations();
        }

        [HttpGet("data/{ownerName}")]
        public IEnumerable<PresentationData> GetAllPresentationData_byOwner(string ownerName) {
            return data.GetPresentations(ownerName);
        }

        [HttpGet("data/{ownerName}/{name}")]
        public PresentationData GetPresentationData(string ownerName, string name) {
            return data.GetPresentation(name, ownerName);
        }

        [HttpGet("current")]
        public IEnumerable<Presentation> GetPresentations() {
            return presentations.GetPresentations();
        }

        [HttpGet("current/{ownerName}")]
        public IEnumerable<Presentation> GetPresentations_byOwner(string ownerName) {
            return presentations.GetPresentations(ownerName);
        }
        
        [HttpGet("current/{ownerName}/{name}")]
        public IEnumerable<Presentation> GetPresentations_byOwnerAndName(string ownerName, string name) {
            return presentations.GetPresentations(name, ownerName);
        }

        [HttpGet("view/{id}")]
        public Presentation GetPresentation(string id) {
            return presentations.GetPresentation(id);
        }

        [HttpPut("{id}/image-presentation")]
        public async Task<IActionResult> UploadImagePresentation(string id, IFormFile imageFile) {
            try {
                await presentations.GetPresentation(id).SetImagePresentation(imageFile);
                await hubContext.Clients.All.SendAsync("SetImagePresentation");
                return Ok();
            }
            catch (Exception e) {
                Console.Error.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/image-presentation")]
        public IEnumerable<string> GetImagePresentation(string id) {
            return presentations.GetPresentation(id).ImagePresentation;
        }

        [HttpPost]
        public ContentResult CreatePresentation() {
            return new ContentResult {
                Content = JsonSerializer.Serialize(presentations.CreatePresentation()),
                ContentType = "application/json"
            };
        }
    }
}