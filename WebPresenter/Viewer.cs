namespace WebPresenter {
    public class Viewer {
        private readonly string username;
        private readonly Presentation presentation;
        private readonly string groupId;

        public string Username => username;
        public Presentation Presentation => presentation;
        public string GroupId => groupId;

        public Viewer(string username, Presentation presentation, string groupId) {
            this.username = username;
            this.presentation = presentation;
            this.groupId = groupId;
        }
    }
}