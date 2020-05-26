using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPresenter.Models {
    
    public class PresentationData {
        private string[] slideNotes;
        
        [StringLength(256), Required] // Key
        public string Name { get; set; }
        
        [StringLength(256), Required] // Key
        public string OwnerName { get; set; }
        
        [ForeignKey("OwnerName")]
        public User Owner { get; set; }

        [StringLength(256)] 
        public string Title { get; set; }
        
        public string Text { get; set; }
        public string PermanentNotes { get; set; }
        public string[] SlideNotes { get => slideNotes; set => slideNotes = value; }
        public string[] ImagePresentation { get; set; }

        public PresentationData(string name, User owner, string title = "") {
            Name = name;
            Owner = owner;
            OwnerName = owner.Name;
            Title = title;
            SlideNotes = new[] {""};
            ImagePresentation = new[] {""};
        }
        
        public PresentationData(string name, string ownerName, string title = "") {
            Name = name;
            OwnerName = ownerName;
            Title = title;
            SlideNotes = new[] {""};
            ImagePresentation = new[] {""};
        }

        public PresentationData() : this("", new User()) { }

        public void ResizeSlideNotes(int length) {
            Array.Resize(ref slideNotes, length);
        }
    }
}