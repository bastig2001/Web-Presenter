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

    public class Presentation : PresentationFundamentals {
        protected internal PresentationData Data { get; }
        public new string Name => Data.Name;
        public new string OwnerName => Data.OwnerName;
        public PresentationState PresentationState { get; set; }
        public TextState TextState { get; set; }
        public new string Title { get => Data.Title; set => Data.Title = value; }
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

        public Presentation(string name, User owner, string title = "New Presentation") : 
            this(new PresentationData(name, owner.Name, title)) { }
        
        public Presentation(string name, string ownerName, string title = "New Presentation") : 
            this(new PresentationData(name, ownerName, title)) { }

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

        protected bool Equals(Presentation other) {
            return currentSlideNumber == other.currentSlideNumber && 
                   numberOfSlides == other.numberOfSlides && 
                   Equals(Data, other.Data) && 
                   PresentationState == other.PresentationState && 
                   TextState == other.TextState;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Presentation) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(currentSlideNumber, numberOfSlides, Data, (int) PresentationState, (int) TextState);
        }
    }
}