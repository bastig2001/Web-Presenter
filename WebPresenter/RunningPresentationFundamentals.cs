using System;

namespace WebPresenter {
    [Serializable]
    public class RunningPresentationFundamentals : PresentationFundamentals {
        public string Id { get; set; }
        
        public RunningPresentationFundamentals() {}

        public RunningPresentationFundamentals(Presentation presentation, string id) : base(presentation.Data) {
            Id = id;
        } 
        
        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(OwnerName)}: {OwnerName}, {nameof(Title)}: {Title}, " +
                   $"{nameof(Id)}: {Id}";
        }
        
        public new object Clone() {
            return new RunningPresentationFundamentals {
                Name = Name,
                OwnerName = OwnerName,
                Title = Title,
                Id = Id
            };
        }
    }
}