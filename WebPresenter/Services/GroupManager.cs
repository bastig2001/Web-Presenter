using System.Collections.Generic;

namespace WebPresenter.Services {
    public class GroupManager {
        private readonly Dictionary<string, string> connectionGroups;

        public GroupManager() {
            connectionGroups = new Dictionary<string, string>();
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