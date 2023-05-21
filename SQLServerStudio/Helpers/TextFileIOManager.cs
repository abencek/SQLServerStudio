using System;
using System.IO;
using System.Threading.Tasks;


namespace SQLServerStudio
{
    /// <summary>
    /// Helper class that reads text from text files
    /// </summary>
    public static class TextFileIOManager
    {
        #region Public Methods

        /// <summary>
        /// Reads content from text file asynchronously 
        /// </summary>
        /// <param name="fileFullpath">Full path to the file</param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public static async Task<string> ReadFileContentAsync(string fileFullpath)
        {
            try
            {
                var text = await File.ReadAllTextAsync(fileFullpath);
                return text;
            }
            catch
            {
                return String.Empty;
            }
        } 

        #endregion

    }
}
