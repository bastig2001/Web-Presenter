using System.Collections.Generic;
using System.Linq;

namespace WebPresenter.Services {
    public class StorageService<T> {
        private readonly Dictionary<string, T> dict = new Dictionary<string, T>();

        public IEnumerable<T> Values => dict.Values;

        public T this[string key] {
            get => dict[key];
            set => dict[key] = value;
        }

        public T GetValueOrDefault(string key) {
            return dict.GetValueOrDefault(key);
        }

        public bool ContainsKey(string key) {
            return dict.ContainsKey(key);
        }

        public void Add(string key, T value) {
            dict.Add(key, value);
        }

        public bool TryAdd(string key, T value) {
            return dict.TryAdd(key, value);
        }

        public bool Remove(string key) {
            return dict.Remove(key);
        }

        public IEnumerable<KeyValuePair<string, T>> AsEnumerable() {
            return dict.AsEnumerable();
        }
    }
}