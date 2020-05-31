using System;
using System.Collections.Generic;
using System.Linq;
using WebPresenter.Models;

namespace WebPresenter.Services {
    public class PresentationsService {
        private readonly PresentationDataService data;
        private readonly StorageService<Presentation> presentations;
        
        public PresentationsService(PresentationDataService data, StorageService<Presentation> presentations) {
            this.data = data;
            this.presentations = presentations;
        }

        public Presentation GetPresentation(string id) {
            return presentations.GetValueOrDefault(id);
        }

        public IEnumerable<Presentation> GetPresentations() {
            return presentations.Values;
        }
        
        public IEnumerable<Presentation> GetPresentations(string ownerName) {
            return presentations.Values.Where(pres => pres.OwnerName == ownerName);
        }
        
        public IEnumerable<Presentation> GetPresentations(string name, string ownerName) {
            return presentations.Values.Where(pres => pres.OwnerName == ownerName && pres.Name == name);
        }
        
        public string StartPresentation(string name, string ownerName) {
            var presentationData = data.GetPresentation(name, ownerName);

            return presentationData == null ? "" : StartPresentation((PresentationData) presentationData.Clone());
        }
        
        public string StartPresentation(PresentationData presentationData) {
            return StartPresentation(new Presentation(presentationData));
        }

        public string StartPresentation(Presentation presentation) {
            string id;

            do {
                id = $"{Guid.NewGuid()}";
            } while (presentations.ContainsKey(id));

            presentations.Add(id, presentation);

            return id;
        }

        public void EndPresentation(string id) {
            presentations.Remove(id);
        }
    }
}