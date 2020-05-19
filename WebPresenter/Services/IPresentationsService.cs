namespace WebPresenter.Services {
    public interface IPresentationsService {
        public Presentation GetPresentation(string id);
        public string CreatePresentation();
    }
}