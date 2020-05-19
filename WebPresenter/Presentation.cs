using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebPresenter.Models;

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
        protected internal PresentationData Data { get; set; }
        public PresentationState PresentationState { get; set; }
        public TextState TextState { get; set; }
        public string Title { get => Data.Title; set => Data.Title = value; }
        public string Text { get => Data.Text; set => Data.Text = value; }
        public string PermanentNotes { get => Data.PermanentNotes; set => Data.PermanentNotes = value; }
        
        private int currentSlideNumber = 0, numberOfSlides;

        public int CurrentSlideNumber {
            get => currentSlideNumber;
            set {
                if (0 <= value && value < numberOfSlides) {
                    currentSlideNumber = value;
                }
            }
        }

        public int NumberOfSlides => numberOfSlides;

        public IEnumerable<string> SlideNotes => Data.SlideNotes;

        public IEnumerable<string> ImagePresentation => Data.ImagePresentation;

        public Presentation(string id, string title = "New Presentation") : this(new PresentationData(id, title)){}

        public Presentation(PresentationData data) {
            PresentationState = PresentationState.Text;
            TextState = TextState.Paragraphs;
            Data = data;
            numberOfSlides = Data.ImagePresentation.Length;
        }
        
        public async Task SetImagePresentation(IFormFile multiImageFile) {
            Data.ImagePresentation = await Helper.GetImagesFromFile(multiImageFile);
            SetNumberOfSlides(Data.ImagePresentation.Length);
        }

        public async Task SetImagePresentation(MemoryStream multiImageStream) {
            Data.ImagePresentation = await Helper.GetImagesFromStream(multiImageStream);
            SetNumberOfSlides(Data.ImagePresentation.Length);
        }

        public void SetImagePresentation(IEnumerable<string> images) {
            Data.ImagePresentation = images.ToArray();
            SetNumberOfSlides(Data.ImagePresentation.Length);
        }
        
        public string GetSlideNotes(int slideNumber) {
            return Data.SlideNotes[slideNumber];
        }

        public void SetSlideNotes(int slideNumber, string notes) {
            Data.SlideNotes[slideNumber] = notes;
        }

        public void SetSlideNotes(IEnumerable<string> newSlideNotes) {
            Data.SlideNotes = newSlideNotes.ToArray();

            if (Data.SlideNotes.Length < numberOfSlides) {
                ResizeSlideNotes();
            }
        }

        public void ClearSlideNotes() {
            Data.SlideNotes = new string[numberOfSlides];
        }

        private void SetNumberOfSlides(int newNumberOfSlides) { 
            numberOfSlides = newNumberOfSlides;
            ResizeSlideNotes();
        }

        private void ResizeSlideNotes() {
            Data.ResizeSlideNotes(numberOfSlides);
        }
    }
}