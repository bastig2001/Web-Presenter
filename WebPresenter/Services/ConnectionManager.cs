namespace WebPresenter.Services {
    public class ConnectionManager {
        private readonly StorageService<Viewer> connections;

        public ConnectionManager(StorageService<Viewer> connections) {
            this.connections = connections;
        }

        public void AddClient(string connectionId, Viewer client) {
            if (!connections.TryAdd(connectionId, client)) {
                connections[connectionId] = client;
            }
        }

        public Viewer GetClient(string connectionId) {
            return connections.GetValueOrDefault(connectionId);
        }

        public void RemoveConnection(string connectionId) {
            connections.Remove(connectionId);
        }
    }
}