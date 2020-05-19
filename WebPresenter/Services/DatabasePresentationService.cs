using System;
using System.Collections;
using System.Linq;

namespace WebPresenter.Services
{
    public class DatabasePresentationService : IPresentationsService
    {
        public static WebPresenterContext WpContext = new WebPresenterContext();

        public Presentation GetPresentation()
        {
            return new Presentation();
        }

        public string CreatePresentation()
        {
            return "yes";
        }

        public Presentation GetRecentPresentation()
        {
            return this.ConvertToDisplayType(WpContext.Presentations.Where(p => p.Id == WpContext.Presentations.Max(p => p.Id)).Single());
        }

        public Presentation GetPresentation(string id)
        {
            return this.ConvertToDisplayType(WpContext.Presentations.Where(p => p.Id == Int32.Parse(id)).Single());
        }


        private Presentation ConvertToDisplayType(Presentations presentation)
        {
            Presentation ConvertedPres = new Presentation(presentation.Title);

            ConvertedPres.PermanentNotes = presentation.Notes;
            ConvertedPres.Text = presentation.Text;

            Slides[] slides = WpContext.Slides.Where(s => s.Presentation == presentation.Id).OrderBy(s => s.Seqnr).ToArray();

            for (int i = 0; i < slides.Length; i++)
            {
                ConvertedPres.SetNumberOfSlides(slides.Length);
                ConvertedPres.AddSingleImage(slides[i].Image);
                ConvertedPres.SetSlideNotes(i, slides[i].Notes);
            }

            return ConvertedPres;
        }
    }
}
