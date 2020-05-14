using System.Linq;

namespace WebPresenter.Services {
    public class DatabasePresentationService : IPresentationsService {
        public static readonly WebPresenterContext WpContext = new WebPresenterContext();

        public Presentation GetPresentation(string id) {
            return ConvertToDisplayType(WpContext.Presentations.Single(p => p.Id == int.Parse(id)));
        }

        public string CreatePresentation() {
            throw new System.NotImplementedException();
        }

        public Presentation GetPresentation() {
            return new Presentation();
        }

        public Presentation GetRecentPresentation() {
            return ConvertToDisplayType(WpContext.Presentations.Single(p => p.Id == WpContext.Presentations.Max(p => p.Id)));
        }

        /*
        public Presentation[] GetAllPresentations()
        {

        }
        */

        private Presentation ConvertToDisplayType(Presentations presentation) {
            var convertedPres = new Presentation(presentation.Title) {
                PermanentNotes = presentation.Notes, 
                Text = presentation.Text
            };


            var slides = WpContext.Slides.Where(s => s.Presentation == presentation.Id).OrderBy(s => s.Seqnr).ToArray();

            for (var i = 0; i < slides.Length; i++) {
                convertedPres.SetNumberOfSlides(slides.Length);
                convertedPres.AddSingleImage(slides[i].Image);
                convertedPres.SetSlideNotes(i, slides[i].Notes);
            }

            return convertedPres;
        }
    }
}