using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebPresenter {
    public enum PresentationState {
        Slides,
        Text
    }

    public enum TextState {
        Html,
        Markdown,
        Monospace,
        Paragraphs
    }

    public class Presentation {
        public PresentationState PresentationState { get; set; }
        public TextState TextState { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PermanentNotes { get; set; }
        
        private int currentSlideNumber = 0, numberOfSlides = 1;

        public int CurrentSlideNumber {
            get => currentSlideNumber;
            set {
                if (0 <= value && value < numberOfSlides) {
                    currentSlideNumber = value;
                }
            }
        }

        public int NumberOfSlides => numberOfSlides;

        private string[] slideNotes;

        public IEnumerable<string> SlideNotes => slideNotes;
        
        private string[] imagePresentation;

        public IEnumerable<string> ImagePresentation => imagePresentation;
        
        private string id;

        public string Id => id;

        public Presentation(string id, string title = "New Presentation") {
            this.id = id;
            PresentationState = PresentationState.Text;
            TextState = TextState.Paragraphs;
            Title = title;
            Text = "";
            PermanentNotes = "";
            slideNotes = new []{""};
            imagePresentation = new []{""};
        }
        
        public async Task SetImagePresentation(IFormFile multiImageFile) {
            imagePresentation = await Helper.GetImagesFromFile(multiImageFile);
            SetNumberOfSlides(imagePresentation.Length);
        }

        public async Task SetImagePresentation(MemoryStream multiImageStream) {
            imagePresentation = await Helper.GetImagesFromStream(multiImageStream);
            SetNumberOfSlides(imagePresentation.Length);
        }

        public void SetImagePresentation(IEnumerable<string> images) {
            imagePresentation = images.ToArray();
            SetNumberOfSlides(imagePresentation.Length);
        }
        
        public string GetSlideNotes(int slideNumber) {
            return slideNotes[slideNumber];
        }

        public void SetSlideNotes(int slideNumber, string notes) {
            slideNotes[slideNumber] = notes;
        }

        public void SetSlideNotes(IEnumerable<string> newSlideNotes) {
            slideNotes = newSlideNotes.ToArray();

            if (slideNotes.Length < numberOfSlides) {
                ResizeSlideNotes();
            }
        }

        public void ClearSlideNotes() {
            slideNotes = new string[numberOfSlides];
        }

        private void SetNumberOfSlides(int newNumberOfSlides) { 
            numberOfSlides = newNumberOfSlides;
            ResizeSlideNotes();
        }

        private void ResizeSlideNotes() {
            Array.Resize(ref slideNotes, numberOfSlides);
        }
    }
}