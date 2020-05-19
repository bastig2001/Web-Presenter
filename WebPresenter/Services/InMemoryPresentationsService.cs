using System;
using System.Collections.Generic;

namespace WebPresenter.Services {
    public class InMemoryPresentationsService : IPresentationsService {
        private readonly Dictionary<string, Presentation> presentations;

        public InMemoryPresentationsService() {
            presentations = new Dictionary<string, Presentation>();
            presentations.Add("1", new Presentation());
        }
        
        public Presentation GetPresentation(string id) {
            return presentations[id];
        }

        public string CreatePresentation() {
            bool created = false;
            string id;
            do {
                id = $"{Guid.NewGuid()}";
                created = presentations.TryAdd(id, new Presentation());
            } while (!created);

            return id;
        }
    }
}