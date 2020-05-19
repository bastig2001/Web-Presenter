using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebPresenter.Hubs;
using WebPresenter.Services;

namespace WebPresenter.Controllers {
    [ApiController]
    [Route("controllers/[controller]")]
    public class PresentationsController : ControllerBase {
        private readonly IPresentationsService presentations;
        private readonly IHubContext<PresentationsHub> hubContext;

        public PresentationsController(IPresentationsService presentations, IHubContext<PresentationsHub> hubContext) {
            this.presentations = presentations;
            this.hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public Presentation Get(string id) {
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