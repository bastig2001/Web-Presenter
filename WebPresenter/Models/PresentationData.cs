using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPresenter.Models {
    
    [Table("presentations", Schema = "public")]
    public class PresentationData {
        private string[] slideNotes;
        
        [Column("id")] [StringLength(256)] [Key] 
        public string Id { get; set; }

        [Column("title")] [StringLength(256)] 
        public string Title { get; set; }
        
        [Column("text")]
        public string Text { get; set; }
        
        [Column("permanentnotes")]
        public string PermanentNotes { get; set; }
        
        [Column("slidenotes")]
        public string[] SlideNotes { get => slideNotes; set => slideNotes = value; }
        
        [Column("imagepresentation")]
        public string[] ImagePresentation { get; set; }

        public PresentationData(string id, string title = "") {
            Id = id;
            Title = title;
            SlideNotes = new[] {""};
            ImagePresentation = new[] {""};
        }

        public PresentationData() : this("") { }

        public void ResizeSlideNotes(int length) {
            Array.Resize(ref slideNotes, length);
        }
    }
}