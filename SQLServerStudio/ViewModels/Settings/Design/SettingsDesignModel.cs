
namespace SQLServerStudio
{
    /// <summary>
    /// A design View Model for application settings
    /// </summary>
    public class SettingsDesignModel : SettingsViewModel
    {
        /// <summary>
        /// A single static instance of this model
        /// </summary>
        public static SettingsDesignModel Instance { get; set; } = new();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsDesignModel()
        {
             ServerName = "localhost";
             ScriptsLocation = @"C:\TestFolder";
        }

    }
}
