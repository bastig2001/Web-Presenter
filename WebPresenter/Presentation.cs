using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebPresenter.Services;

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

        public string[] SlideNotes => slideNotes;
        
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
            SetNumberOfSlides(imagePresentation.Length);
        }

        public async Task SetImagePresentation(MemoryStream multiImageStream) {
            imagePresentation = await Helper.GetImagesFromStream(multiImageStream);
            SetNumberOfSlides(imagePresentation.Length);
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

        public void SetNumberOfSlides(int newNumberOfSlide) {
            numberOfSlides = newNumberOfSlide;
            ResizeSlideNotes();
        }

        private void ResizeSlideNotes() {
            Array.Resize(ref slideNotes, numberOfSlides);
        }
        
        public void AddSingleImage(string img)
        {
            this.ImagePresentation.Append(img);
        }
        public void Save()
        {
            using(WebPresenterContext WpContext = DatabasePresentationService.WpContext)
            {
                Presentations DbPres = new Presentations();
                DbPres.Presenterid = 1;

                string[] TmpImgArray = this.ImagePresentation.ToArray();
                string[] TmpNoteArray = this.SlideNotes.ToArray();

                for (short i = 0; i < this.NumberOfSlides - 1; i++)
                {
                    Slides slide = new Slides();

                    slide.Image = TmpImgArray[i];
                    slide.Notes = TmpNoteArray[i];
                    slide.Seqnr = i;

                    DbPres.Slides.Add(slide);
                }

                WpContext.Presentations.Add(DbPres);
            }
        }
    }
}