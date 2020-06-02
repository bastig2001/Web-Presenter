namespace WebPresenter {
    public struct PresentationFundamentals {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Title { get; set; }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(OwnerName)}: {OwnerName}, {nameof(Title)}: {Title}";
        }
    }
}