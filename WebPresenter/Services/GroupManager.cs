using System.Collections.Generic;

namespace WebPresenter.Services {
    public class GroupManager {
        private readonly StorageService<string> connectionGroups;

        public GroupManager(StorageService<string> connectionGroups) {
            this.connectionGroups = connectionGroups;
        }

        public void AddConnectionGroup(string connectionId, string presentationId) {
            if (!connectionGroups.TryAdd(connectionId, presentationId)) {
                connectionGroups[connectionId] = presentationId;
            }
        }

        public string GetGroupName(string connectionId) {
            return connectionGroups.GetValueOrDefault(connectionId);
        }

        public void RemoveConnection(string connectionId) {
            connectionGroups.Remove(connectionId);
        }
    }
}