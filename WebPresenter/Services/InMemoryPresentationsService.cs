using System;
using System.Collections.Generic;
using WebPresenter.Models;

namespace WebPresenter.Services {
    public class InMemoryPresentationsService : IPresentationsService {
        private readonly Dictionary<string, Presentation> presentations;
        private readonly User anyone = new User("anyone");

        public InMemoryPresentationsService() {
            presentations = new Dictionary<string, Presentation>();
            presentations.Add("1", new Presentation("1", anyone));
        }
        
        public Presentation GetPresentation(string id) {
            return presentations[id];
        }

        public string CreatePresentation() {
            bool created = false;
            string id;
            do {
                id = $"{Guid.NewGuid()}";
                created = presentations.TryAdd(id, new Presentation(id, anyone));
            } while (!created);

            return id;
        }
    }
}