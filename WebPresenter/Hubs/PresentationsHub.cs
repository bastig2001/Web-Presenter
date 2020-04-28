using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebPresenter.Services;

namespace WebPresenter.Hubs {
    public class PresentationsHub : Hub {
        private readonly Presentation presentation;

        public PresentationsHub(IPresentationsService presentationsService) {
            presentation = presentationsService.GetPresentation();
        }
        
        public async Task SetPresentationState(PresentationState presentationState) {
            // presentation.PresentationState = JsonConvert.DeserializeObject<PresentationState>(presentationStateJson);
            presentation.PresentationState = presentationState;
            await Clients.Others.SendAsync("SetPresentationState", presentation.PresentationState);
        }
        
        public async Task SetTextState(TextState textState) {
            // presentation.TextState = JsonConvert.DeserializeObject<TextState>(textStateJson);
            presentation.TextState = textState;
            await Clients.Others.SendAsync("SetTextState", presentation.TextState);
        }
        
        public async Task SetName(string name) {
            presentation.Name = name;
            await Clients.Others.SendAsync("SetName", presentation.Name);
        }
        
        public async Task SetText(string text) {
            presentation.Text = text;
            await Clients.Others.SendAsync("SetText", presentation.Text);
        }
        
        public async Task SetPermanentNotes(string notes) {
            presentation.PermanentNotes = notes;
            await Clients.Others.SendAsync("SetPermanentNotes", presentation.PermanentNotes);
        }
        
        public async Task GoToSlide(int slideNumber) {
            presentation.CurrentSlideNumber = slideNumber;
            await Clients.Others.SendAsync("GoToSlide", presentation.CurrentSlideNumber);
        }
        
        public async Task MoveToNextSlide() {
            presentation.CurrentSlideNumber++;
            await Clients.Others.SendAsync("MoveToNextSlide");
        }
        
        public async Task MoveToPreviousSlide() {
            presentation.CurrentSlideNumber--;
            await Clients.Others.SendAsync("MoveToPreviousSlide");
        }
        
        public async Task SetSlideNotes(int slideNumber, string notes) {
            presentation.SetSlideNotes(slideNumber, notes);
            await Clients.Others.SendAsync("SetSlideNotes", 
                slideNumber, presentation.GetSlideNotes(slideNumber));
        }

        public async Task ClearSlideNotes() {
            presentation.ClearSlideNotes();
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