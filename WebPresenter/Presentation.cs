using System;
using System.Collections.Generic;
using System.IO;
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
        public string Name { get; set; }
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

        public Presentation(string name = "New Presentation") {
            PresentationState = PresentationState.Text;
            TextState = TextState.Paragraphs;
            Name = name;
            Text = "";
            PermanentNotes = "";
            slideNotes = new []{""};
            imagePresentation = new []{""};
        }
        
        public async Task SetImagePresentation(IFormFile multiImageFile) {
            imagePresentation = await Helper.GetImagesFromFile(multiImageFile);
            SetNumberOfSlide(imagePresentation.Length);
        }

        public async Task SetImagePresentation(MemoryStream multiImageStream) {
            imagePresentation = await Helper.GetImagesFromStream(multiImageStream);
            SetNumberOfSlide(imagePresentation.Length);
        }
        
        public string GetSlideNotes(int slideNumber) {
            return slideNotes[slideNumber];
        }

        public void SetSlideNotes(int slideNumber, string notes) {
            slideNotes[slideNumber] = notes;
        }

        public void ClearSlideNotes() {
            slideNotes = new string[numberOfSlides];
        }

        private void SetNumberOfSlide(int newNumberOfSlide) {
            numberOfSlides = newNumberOfSlide;
            ResizeSlideNotes();
        }

        private void ResizeSlideNotes() {
            Array.Resize(ref slideNotes, numberOfSlides);
        }
    }
}