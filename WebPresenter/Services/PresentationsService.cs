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
        
        public IEnumerable<RunningPresentationFundamentals> GetPresentationFundamentals() {
            return presentations.AsEnumerable()
                .Select(pair => new RunningPresentationFundamentals(pair.Value, pair.Key));
        }
        
        public IEnumerable<RunningPresentationFundamentals> GetPresentationFundamentals(string ownerName) {
            return presentations.AsEnumerable()
                .Where(pair => pair.Value.OwnerName == ownerName)
                .Select(pair => new RunningPresentationFundamentals(pair.Value, pair.Key));
        }
        
        public IEnumerable<RunningPresentationFundamentals> GetPresentationFundamentals(string name, string ownerName) {
            return presentations.AsEnumerable()
                .Where(pair => pair.Value.OwnerName == ownerName && pair.Value.Name == name)
                .Select(pair => new RunningPresentationFundamentals(pair.Value, pair.Key));
        }

        public string StartPresentation(PresentationFundamentals fundamentals) {
            return StartPresentation(fundamentals.Name, fundamentals.OwnerName);
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

        internal object CreatePresentation()
        {
            throw new NotImplementedException();
        }
    }
}