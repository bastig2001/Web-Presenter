using System.ComponentModel;

namespace WebPresenter {
    public class DbConnectionSettings {
        [DefaultValue("localhost")]
        public string Server { get; set; }
        
        [DefaultValue("5432")]
        public string Port { get; set; }
        
        [DefaultValue("WebPresenter")]
        public string Database { get; set; }
        
        [DefaultValue("postgres")]
        public string User { get; set; }
        
        [DefaultValue("wasistpasstiert")]
        public string Password { get; set; }

        public string GetConnectionString() {
            return $"Server={Server};Port={Port};Database={Database};Username={User};Password={Password};";
        }
    }
}