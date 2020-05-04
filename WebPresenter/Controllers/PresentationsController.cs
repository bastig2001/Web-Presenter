using System;
using System.Collections.Generic;
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
        private readonly IPresentationsService service;
        private readonly IHubContext<PresentationsHub> hubContext;

        public PresentationsController(IPresentationsService service, IHubContext<PresentationsHub> hubContext) {
            this.service = service;
            this.hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public Presentation Get(uint id) {
            return service.GetPresentation(id);
        }

        [HttpPut("{id}/image-presentation")]
        public async Task<IActionResult> UploadImagePresentation(uint id, IFormFile imageFile) {
            try {
                await service.GetPresentation(id).SetImagePresentation(imageFile);
                await hubContext.Clients.All.SendAsync("SetImagePresentation");
                return Ok();
            }
            catch (Exception e) {
                Console.Error.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/image-presentation")]
        public IEnumerable<string> GetImagePresentation(uint id) {
            return service.GetPresentation(id).ImagePresentation;
        }
    }
}