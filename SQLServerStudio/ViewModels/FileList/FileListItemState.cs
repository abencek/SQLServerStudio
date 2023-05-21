

namespace SQLServerStudio
{
    /// <summary>
    /// State of a single item in FileList control
    /// </summary>
    public enum FileListItemState
    {
        /// <summary>
        /// Item can be selected or unselected for execution
        /// </summary>
        Selection,

        /// <summary>
        /// SQL script for an item is executing
        /// </summary>
        Execution,

        /// <summary>
        /// Execution was completed with success
        /// </summary>
        DoneSuccess,

        /// <summary>
        /// Execution was completed with error
        /// </summary>
        DoneError
    }
}
