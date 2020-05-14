using System.Collections.Generic;

namespace WebPresenter {
    public class Presenter {
        public Presenter() {
            Presentations = new HashSet<Presentations>();
        }

        public int Id { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Presentations> Presentations { get; set; }
    }
}