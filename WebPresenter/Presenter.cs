namespace WebPresenter {
    public class Presenter : Viewer {
        private readonly string presenterGroupId;

        public string PresenterGroupId => presenterGroupId;

        public Presenter(string username, Presentation presentation, string groupId, string presenterGroupId) 
            : base(username, presentation, groupId) {

            this.presenterGroupId = presenterGroupId;
        }
    }
}