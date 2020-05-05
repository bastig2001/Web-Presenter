using System;
using System.Collections.Generic;

namespace WebPresenter
{
    public partial class Presentations
    {
        public Presentations()
        {
            Slides = new HashSet<Slides>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public int Presenterid { get; set; }

        public virtual Presenter Presenter { get; set; }
        public virtual ICollection<Slides> Slides { get; set; }
    }
}
