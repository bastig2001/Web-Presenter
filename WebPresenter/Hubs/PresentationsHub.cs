using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebPresenter.Services;

namespace WebPresenter.Hubs {
    public class PresentationsHub : Hub {
        private readonly PresentationsService presentations;
        private readonly ConnectionManager connections;

        private string groupName;
        private Presentation presentation;

        public PresentationsHub(PresentationsService presentations, ConnectionManager connections) {
            this.presentations = presentations;
            this.connections = connections;
        }

        public override Task OnConnectedAsync() {
            string presentationId = Context.GetHttpContext().Request.Query["presentation-id"];
            string clientType = Context.GetHttpContext().Request.Query["client-type"];
            string username = Context.GetHttpContext().Request.Query["username"];

            if (clientType == "viewer") {
                connections.AddClient(
                    Context.ConnectionId, 
                    new Viewer(
                        username, 
                        presentations.GetPresentation(presentationId), 
                        presentationId
                        )
                    );
            }
            else if (clientType == "presenter") {
                string presenterGroupId = $"{presentationId}-presenter";
                
                connections.AddClient(
                    Context.ConnectionId, 
                    new Presenter(
                        username, 
                        presentations.GetPresentation(presentationId), 
                        presentationId, 
                        presenterGroupId
                        )
                );
                Groups.AddToGroupAsync(Context.ConnectionId, presenterGroupId);
            }
            
            return Groups.AddToGroupAsync(Context.ConnectionId, $"{presentationId}");
        }
        
        public override Task OnDisconnectedAsync(Exception exception) {
            Viewer client = connections.GetClient(Context.ConnectionId);
            connections.RemoveConnection(Context.ConnectionId);

            if (client.GetType() == typeof(Presenter)) {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, ((Presenter) client).PresenterGroupId);
            }
            
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, client.GroupId);
        }

        public async Task SetPresentationState(PresentationState presentationState) {
            SetContextVariables();
            presentation.PresentationState = presentationState;
            await Clients.OthersInGroup(groupName).SendAsync("SetPresentationState", presentation.PresentationState);
        }
        
        public async Task SetTextState(TextState textState) {
            SetContextVariables();
            presentation.TextState = textState;
            await Clients.OthersInGroup(groupName).SendAsync("SetTextState", presentation.TextState);
        }
        
        public async Task SetTitle(string title) {
            SetContextVariables();
            presentation.Title = title;
            await Clients.OthersInGroup(groupName).SendAsync("SetTitle", presentation.Title);
        }
        
        public async Task SetText(string text) {
            SetContextVariables();
            presentation.Text = text;
            await Clients.OthersInGroup(groupName).SendAsync("SetText", presentation.Text);
        }
        
        public async Task SetPermanentNotes(string notes) {
            SetContextVariables();
            presentation.PermanentNotes = notes;
            await Clients.OthersInGroup(groupName).SendAsync("SetPermanentNotes", presentation.PermanentNotes);
        }
        
        public async Task GoToSlide(int slideNumber) {
            SetContextVariables();
            presentation.CurrentSlideNumber = slideNumber;
            await Clients.OthersInGroup(groupName).SendAsync("GoToSlide", presentation.CurrentSlideNumber);
        }
        
        public async Task MoveToNextSlide() {
            SetContextVariables();
            presentation.CurrentSlideNumber++;
            await Clients.OthersInGroup(groupName).SendAsync("MoveToNextSlide");
        }
        
        public async Task MoveToPreviousSlide() {
            SetContextVariables();
            presentation.CurrentSlideNumber--;
            await Clients.OthersInGroup(groupName).SendAsync("MoveToPreviousSlide");
        }
        
        public async Task SetSlideNotes(int slideNumber, string notes) {
            SetContextVariables();
            presentation.SetSlideNotes(slideNumber, notes);
            await Clients.OthersInGroup(groupName).SendAsync("SetSlideNotes", 
                slideNumber, presentation.GetSlideNotes(slideNumber));
        }

        public async Task ClearSlideNotes() {
            SetContextVariables();
            presentation.ClearSlideNotes();
            await Clients.OthersInGroup(groupName).SendAsync("ClearSlideNotes");
        }

        public async Task ReloadImagePresentation() {
            SetContextVariables();
            await Clients.OthersInGroup(groupName).SendAsync("ReloadImagePresentation");
        }

        public async Task EndPresentation() {
            SetContextVariables();
            await Clients.OthersInGroup(groupName).SendAsync("EndPresentation");
        }

        public bool SavePresentation() {
            SetContextVariables();
            return presentations.SavePresentation(presentation);
        }

        private void SetContextVariables() {
            groupName = connections.GetClient(Context.ConnectionId);
            presentation = presentations.GetPresentation(groupName);
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