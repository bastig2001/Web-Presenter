using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebPresenter.Services;

namespace WebPresenter.Hubs {
    public class PresentationsHub : Hub {
        private readonly IPresentationsService service;
        private uint sid;

        public PresentationsHub(IPresentationsService presentationsService) {
            service = presentationsService;
        }

        public override Task OnConnectedAsync() {
            sid = uint.Parse(Context.GetHttpContext().Request.Query["sid"]);
            Console.WriteLine($"OnConnectedAsync: {sid}");
            return Groups.AddToGroupAsync(Context.ConnectionId, $"{sid}");
        }

        public override Task OnDisconnectedAsync(Exception exception) {
            Console.WriteLine($"OnDisconnectedAsync: {sid}; {exception}");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SetPresentationState(uint id, PresentationState presentationState) {
            service.GetPresentation(id).PresentationState = presentationState;
            await Clients.Others.SendAsync("SetPresentationState", service.GetPresentation(id).PresentationState);
            Console.WriteLine($"PresentationsHub presentation state set: {sid}");
        }
        
        public async Task SetTextState(uint id, TextState textState) {
            service.GetPresentation(id).TextState = textState;
            await Clients.Others.SendAsync("SetTextState", service.GetPresentation(id).TextState);
        }
        
        public async Task SetName(uint id, string name) {
            service.GetPresentation(id).Name = name;
            await Clients.Others.SendAsync("SetName", service.GetPresentation(id).Name);
        }
        
        public async Task SetText(uint id, string text) {
            service.GetPresentation(id).Text = text;
            await Clients.Others.SendAsync("SetText", service.GetPresentation(id).Text);
        }
        
        public async Task SetPermanentNotes(uint id, string notes) {
            service.GetPresentation(id).PermanentNotes = notes;
            await Clients.Others.SendAsync("SetPermanentNotes", service.GetPresentation(id).PermanentNotes);
        }
        
        public async Task GoToSlide(uint id, int slideNumber) {
            service.GetPresentation(id).CurrentSlideNumber = slideNumber;
            await Clients.Others.SendAsync("GoToSlide", service.GetPresentation(id).CurrentSlideNumber);
        }
        
        public async Task MoveToNextSlide(uint id) {
            service.GetPresentation(id).CurrentSlideNumber++;
            await Clients.Others.SendAsync("MoveToNextSlide");
        }
        
        public async Task MoveToPreviousSlide(uint id) {
            service.GetPresentation(id).CurrentSlideNumber--;
            await Clients.Others.SendAsync("MoveToPreviousSlide");
        }
        
        public async Task SetSlideNotes(uint id, int slideNumber, string notes) {
            service.GetPresentation(id).SetSlideNotes(slideNumber, notes);
            await Clients.Others.SendAsync("SetSlideNotes", 
                slideNumber, service.GetPresentation(id).GetSlideNotes(slideNumber));
        }

        public async Task ClearSlideNotes(uint id) {
            service.GetPresentation(id).ClearSlideNotes();
            await Clients.Others.SendAsync("ClearSlideNotes");
        }
        
        // public async Task UploadImagePresentation(IAsyncEnumerable<string> stream) {
        //     Console.WriteLine("uploading");
        //     await using (var memoryStream = new MemoryStream()) {
        //         var memoryStreamWriter = new StreamWriter(memoryStream);
        //         await foreach (var item in stream) {
        //             memoryStreamWriter.Write(item);
        //             await memoryStreamWriter.FlushAsync();
        //         }
        //
        //         presentation.SetImagePresentation(memoryStream);
        //     }
        //
        //     await Clients.All.SendCoreAsync("ImagePresentationSet", new object[] {});
        // }
        //
        // public async IAsyncEnumerable<string> GetImagePresentation(
        //     int delay,
        //     [EnumeratorCancellation] 
        //     CancellationToken cancellationToken
        //     ) {
        //     foreach (var image in presentation.ImagePresentation) {
        //         cancellationToken.ThrowIfCancellationRequested();
        //         yield return image;
        //         await Task.Delay(delay, cancellationToken);
        //     }
        // }
    }
}