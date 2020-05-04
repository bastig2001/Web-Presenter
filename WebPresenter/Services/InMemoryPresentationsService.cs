using System.Collections.Generic;

namespace WebPresenter.Services {
    public class InMemoryPresentationsService : IPresentationsService {
        private readonly Dictionary<uint, Presentation> presentations;

        public InMemoryPresentationsService() {
            presentations = new Dictionary<uint, Presentation>();
            presentations.Add(1, new Presentation());
        }
        
        public Presentation GetPresentation(uint id) {
            return presentations[id];
        }
    }
}