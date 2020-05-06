using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebPresenter.Services;

namespace WebPresenter.Hubs {
    public class PresentationsHub : Hub {
        private readonly IPresentationsService presentations;
        private readonly GroupManager groups;

        public PresentationsHub(IPresentationsService presentations, GroupManager groups) {
            this.presentations = presentations;
            this.groups = groups;
        }

        public override Task OnConnectedAsync() {
            string presentationId = Context.GetHttpContext().Request.Query["presentation-id"];
            groups.AddConnectionGroup(Context.ConnectionId, presentationId);
            return Groups.AddToGroupAsync(Context.ConnectionId, $"{presentationId}");
        }

        public override Task OnDisconnectedAsync(Exception exception) {
            string presentationId = groups.GetGroupName(Context.ConnectionId);
            groups.RemoveConnection(Context.ConnectionId);
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{presentationId}");
        }

        public async Task SetPresentationState(PresentationState presentationState) {
            var presentation = GetPresentation();
            presentation.PresentationState = presentationState;
            await Clients.Others.SendAsync("SetPresentationState", presentation.PresentationState);
        }
        
        public async Task SetTextState(TextState textState) {
            var presentation = GetPresentation();
            presentation.TextState = textState;
            await Clients.Others.SendAsync("SetTextState", presentation.TextState);
        }
        
        public async Task SetName(string name) {
            var presentation = GetPresentation();
            presentation.Name = name;
            await Clients.Others.SendAsync("SetName", presentation.Name);
        }
        
        public async Task SetText(string text) {
            var presentation = GetPresentation();
            presentation.Text = text;
            await Clients.Others.SendAsync("SetText", presentation.Text);
        }
        
        public async Task SetPermanentNotes(string notes) {
            var presentation = GetPresentation();
            presentation.PermanentNotes = notes;
            await Clients.Others.SendAsync("SetPermanentNotes", presentation.PermanentNotes);
        }
        
        public async Task GoToSlide(int slideNumber) {
            var presentation = GetPresentation();
            presentation.CurrentSlideNumber = slideNumber;
            await Clients.Others.SendAsync("GoToSlide", presentation.CurrentSlideNumber);
        }
        
        public async Task MoveToNextSlide() {
            var presentation = GetPresentation();
            presentation.CurrentSlideNumber++;
            await Clients.Others.SendAsync("MoveToNextSlide");
        }
        
        public async Task MoveToPreviousSlide() {
            var presentation = GetPresentation();
            presentation.CurrentSlideNumber--;
            await Clients.Others.SendAsync("MoveToPreviousSlide");
        }
        
        public async Task SetSlideNotes(int slideNumber, string notes) {
            var presentation = GetPresentation();
            presentation.SetSlideNotes(slideNumber, notes);
            await Clients.Others.SendAsync("SetSlideNotes", 
                slideNumber, presentation.GetSlideNotes(slideNumber));
        }

        public async Task ClearSlideNotes() {
            var presentation = GetPresentation();
            presentation.ClearSlideNotes();
            await Clients.Others.SendAsync("ClearSlideNotes");
        }

        private Presentation GetPresentation() {
            return presentations.GetPresentation(groups.GetGroupName(Context.ConnectionId));
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