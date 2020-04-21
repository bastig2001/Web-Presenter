namespace WebPresenter.Services {
    public class InMemoryPresentationsService : IPresentationsService {
        private readonly Presentation presentation;

        public InMemoryPresentationsService() {
            presentation = new Presentation();
        }
        
        public Presentation GetPresentation() {
            return presentation;
        }
    }
}