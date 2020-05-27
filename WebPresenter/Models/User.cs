using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPresenter.Models {
    
    public class User {
        [StringLength(256), Key]
        public string Name { get; set; }
        
        public ICollection<PresentationData> Presentations { get; set; }

        public User(string name) {
            Name = name;
            Presentations = new List<PresentationData>();
        }

        public User() : this("") { }

        protected bool Equals(User other) {
            return Name == other.Name;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode() {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}