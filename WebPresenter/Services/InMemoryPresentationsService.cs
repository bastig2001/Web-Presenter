using System;
using System.Collections.Generic;

namespace WebPresenter.Services {
    public class InMemoryPresentationsService : IPresentationsService {
        private readonly Dictionary<string, Presentation> presentations;

        public InMemoryPresentationsService() {
            presentations = new Dictionary<string, Presentation>();
            presentations.Add("1", new Presentation("1"));
        }
        
        public Presentation GetPresentation(string id) {
            return presentations[id];
        }

        public string CreatePresentation() {
            bool created = false;
            string id;
            do {
                id = $"{Guid.NewGuid()}";
                created = presentations.TryAdd(id, new Presentation(id));
            } while (!created);

            return id;
        }
    }
}