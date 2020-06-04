using System;

namespace WebPresenter {
    public class PresentationFundamentals : ICloneable {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Title { get; set; }

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