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
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.PresentationState = presentationState;
            await Clients.OthersInGroup(group).SendAsync("SetPresentationState", presentation.PresentationState);
        }
        
        public async Task SetTextState(TextState textState) {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.TextState = textState;
            await Clients.OthersInGroup(group).SendAsync("SetTextState", presentation.TextState);
        }
        
        public async Task SetTitle(string title) {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.Title = title;
            await Clients.OthersInGroup(group).SendAsync("SetTitle", presentation.Title);
        }
        
        public async Task SetText(string text) {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.Text = text;
            await Clients.OthersInGroup(group).SendAsync("SetText", presentation.Text);
        }
        
        public async Task SetPermanentNotes(string notes) {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.PermanentNotes = notes;
            await Clients.OthersInGroup(group).SendAsync("SetPermanentNotes", presentation.PermanentNotes);
        }
        
        public async Task GoToSlide(int slideNumber) {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.CurrentSlideNumber = slideNumber;
            await Clients.OthersInGroup(group).SendAsync("GoToSlide", presentation.CurrentSlideNumber);
        }
        
        public async Task MoveToNextSlide() {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.CurrentSlideNumber++;
            await Clients.OthersInGroup(group).SendAsync("MoveToNextSlide");
        }
        
        public async Task MoveToPreviousSlide() {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.CurrentSlideNumber--;
            await Clients.OthersInGroup(group).SendAsync("MoveToPreviousSlide");
        }
        
        public async Task SetSlideNotes(int slideNumber, string notes) {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.SetSlideNotes(slideNumber, notes);
            await Clients.OthersInGroup(group).SendAsync("SetSlideNotes", 
                slideNumber, presentation.GetSlideNotes(slideNumber));
        }

        public async Task ClearSlideNotes() {
            string group = GetGroup();
            var presentation = GetPresentation(group);
            
            presentation.ClearSlideNotes();
            await Clients.OthersInGroup(group).SendAsync("ClearSlideNotes");
        }

        private string GetGroup() {
            return groups.GetGroupName(Context.ConnectionId);
        }

        private Presentation GetPresentation(string groupName) {
            return presentations.GetPresentation(groupName);
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