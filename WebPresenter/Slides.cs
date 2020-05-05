using System;
using System.Collections.Generic;

namespace WebPresenter
{
    public partial class Slides
    {
        public int Id { get; set; }
        public short Seqnr { get; set; }
        public string Notes { get; set; }
        public string Image { get; set; }
        public int Presentation { get; set; }

        public virtual Presentations PresentationNavigation { get; set; }
    }
}
