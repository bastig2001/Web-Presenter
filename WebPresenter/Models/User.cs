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
    }
}