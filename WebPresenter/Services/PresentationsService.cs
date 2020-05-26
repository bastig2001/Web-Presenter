using System.Collections.Generic;

namespace WebPresenter.Services {
    public class PresentationsService {
        private readonly PresentationDataService data;
        private readonly HashSet<Presentation> presentations;
        
        public PresentationsService(PresentationDataService data) {
            this.data = data;
            presentations = new HashSet<Presentation>();
        }
    }
}