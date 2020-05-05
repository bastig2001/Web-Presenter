using System;
using System.Linq;
using Npgsql;

namespace WebPresenter.Services
{
    public class DatabasePresentationService : IPresentationsService
    {
        private readonly Presentation presentation;
        //private readonly WebPresenterContext WpContext;

        public DatabasePresentationService()
        {
            using(WebPresenterContext WpContext = new WebPresenterContext())
            {
                Presentations pres = WpContext.Presentations.Where(p => p.Id == 1).Single();

                this.presentation = new Presentation(pres.Title);
                this.presentation.PermanentNotes = pres.Notes;
                this.presentation.Text = pres.Text;

                Slides[] slides = WpContext.Slides.Where(s => s.Presentation == pres.Id).OrderBy(s => s.Seqnr).ToArray();

                for (int i = 0; i < slides.Length; i++)
                {
                    this.presentation.SetNumberOfSlides(slides.Length);
                    this.presentation.AddSingleImage(slides[i].Image);
                    this.presentation.SetSlideNotes(i, slides[i].Notes);
                }

            }
        }

        public Presentation GetPresentation()
        {
            return this.presentation;
        }
    }
}
