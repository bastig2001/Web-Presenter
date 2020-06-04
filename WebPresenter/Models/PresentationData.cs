using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebPresenter.Models {
    
    [Serializable]
    public class PresentationData : PresentationFundamentals {
        private string[] slideNotes;
        
        [StringLength(256), Required] // Key
        public new string Name { get; set; }
        
        [StringLength(256), Required] // Key
        public new string OwnerName { get; set; }
        
        [ForeignKey("OwnerName")]
        public User Owner { get; set; }

        [StringLength(256)] 
        public new string Title { get; set; }
        
        public string Text { get; set; }
        public string PermanentNotes { get; set; }
        public string[] SlideNotes { get => slideNotes; set => slideNotes = value; }
        public string[] ImagePresentation { get; set; }

        public PresentationData(string name, string ownerName, string title = "") {
            Name = name;
            OwnerName = ownerName;
            Title = title;
            SlideNotes = new[] {""};
            ImagePresentation = new[] {""};
        }

        public PresentationData() : this("", "") { }

        public void ResizeSlideNotes(int length) {
            Array.Resize(ref slideNotes, length);
        }

        protected bool Equals(PresentationData other) {
            return Name == other.Name && OwnerName == other.OwnerName && Equals(Owner, other.Owner);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PresentationData) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name, OwnerName, Owner);
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(OwnerName)}: {OwnerName}, {nameof(Title)}: {Title}, " +
                   $"{nameof(Text)}: {Text}, {nameof(PermanentNotes)}: {PermanentNotes}";
        }

        public new object Clone() {
            using var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            
            formatter.Serialize(stream, this);
            stream.Position = 0;

            return formatter.Deserialize(stream);
        }
    }
}