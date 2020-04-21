using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebPresenter.Services;

namespace WebPresenter.Hubs {
    public class PresentationsHub : Hub {
        private readonly Presentation presentation;

        public PresentationsHub(IPresentationsService presentationsService) {
            presentation = presentationsService.GetPresentation();
        }

        public Presentation GetPresentation() {
            return presentation;
        }

        public async Task SetPresentationState(PresentationState presentationState) {
            // presentation.PresentationState = JsonConvert.DeserializeObject<PresentationState>(presentationStateJson);
            presentation.PresentationState = presentationState;
            await Clients.Others.SendCoreAsync("SetPresentationState",
                new object[] {presentation.PresentationState});
        }
        
        public async Task SetTextState(TextState textState) {
            // presentation.TextState = JsonConvert.DeserializeObject<TextState>(textStateJson);
            presentation.TextState = textState;
            await Clients.Others.SendCoreAsync("SetTextState",
                new object[] {presentation.TextState});
        }
        
        public async Task SetName(string name) {
            presentation.Name = name;
            await Clients.Others.SendCoreAsync("SetName", 
                new object[] {presentation.Name});
        }
        
        public async Task SetText(string text) {
            presentation.Text = text;
            await Clients.Others.SendCoreAsync("SetText", 
                new object[] {presentation.Text});
        }
        
        public async Task SetPermanentNotes(string notes) {
            presentation.PermanentNotes = notes;
            await Clients.Others.SendCoreAsync("SetPermanentNotes", 
                new object[] {presentation.PermanentNotes});
        }
        
        public async Task GoToSlide(int slideNumber) {
            presentation.CurrentSlideNumber = slideNumber;
            await Clients.Others.SendCoreAsync("GoToSlide", 
                new object[] {presentation.CurrentSlideNumber});
        }
        
        public async Task MoveToNextSlide() {
            presentation.CurrentSlideNumber++;
            await Clients.Others.SendCoreAsync("MoveToNextSlide", new object[] {});
        }
        
        public async Task MoveToPreviousSlide() {
            presentation.CurrentSlideNumber--;
            await Clients.Others.SendCoreAsync("MoveToPreviousSlide", new object[] {});
        }
        
        public async Task SetSlideNotes(int slideNumber, string notes) {
            presentation.SetSlideNotes(slideNumber, notes);
            await Clients.Others.SendCoreAsync("setSlideNotes",
                new object[] {slideNumber, presentation.GetSlideNotes(slideNumber)});
        }

        public async Task ClearSlideNotes() {
            presentation.ClearSlideNotes();
            await Clients.Others.SendCoreAsync("ClearSlideNotes", new object[] {});
        }

        public async Task UploadImagePresentation(IAsyncEnumerable<string> stream) {
            await using (var memoryStream = new MemoryStream()) {
                var memoryStreamWriter = new StreamWriter(memoryStream);
                await foreach (var item in stream) {
                    memoryStreamWriter.Write(item);
                    await memoryStreamWriter.FlushAsync();
                }

                presentation.SetImagePresentation(memoryStream);
            }

            await Clients.All.SendCoreAsync("ImagePresentationSet", new object[] {});
        }
        
        public async IAsyncEnumerable<string> GetImagePresentation(
            int delay,
            [EnumeratorCancellation] 
            CancellationToken cancellationToken
            ) {
            foreach (var image in presentation.ImagePresentation) {
                cancellationToken.ThrowIfCancellationRequested();
                yield return image;
                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}