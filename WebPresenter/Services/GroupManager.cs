using System.Collections.Generic;

namespace WebPresenter.Services {
    public class GroupManager {
        private readonly Dictionary<string, string> connectionGroups;

        public GroupManager() {
            connectionGroups = new Dictionary<string, string>();
        }

        public void AddConnectionGroup(string connectionId, string groupName) {
            if (!connectionGroups.TryAdd(connectionId, groupName)) {
                connectionGroups[connectionId] = groupName;
            }
        }

        public string GetGroupName(string connectionId) {
            return connectionGroups.GetValueOrDefault(connectionId);
        }
    }
}