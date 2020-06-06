using System;
using WebPresenter.Models;

namespace WebPresenter {
    [Serializable]
    public class PresentationFundamentals : ICloneable {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Title { get; set; }
        
        public PresentationFundamentals() {}

        public PresentationFundamentals(PresentationData data) {
            Name = data.Name;
            OwnerName = data.OwnerName;
            Title = data.Title;
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(OwnerName)}: {OwnerName}, {nameof(Title)}: {Title}";
        }

        public object Clone() {
            return new PresentationFundamentals {
                Name = Name,
                OwnerName = OwnerName,
                Title = Title
            };
        }
    }
}