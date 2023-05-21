using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;


namespace SQLServerStudio
{
    /// <summary>
    /// View Model for main application window
    /// </summary>
    public class MainWindowViewModel :BaseViewModel
    {

        #region Private Members

        /// <summary>
        /// Service manages application dialogs
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Service manages simple message dialogs
        /// </summary>
        private readonly IMessageService _messageService;

        #endregion


        #region Public Properties

        /// <summary>
        /// View Model for application Settings
        /// </summary>
        public SettingsViewModel Settings { get; set; } = new();

        /// <summary>
        /// View Model for list of SQL scripts
        /// </summary>
        public FileListViewModel FileList { get; set; } = new();

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dialogService">Instance of <see cref="IDialogService"/></param>
        /// <param name="messageService">Instance of <see cref="IMessageService"/></param>
        public MainWindowViewModel(IDialogService dialogService, IMessageService messageService)
        {
            _dialogService = dialogService;
            _messageService = messageService;

            //Set default values
            Settings.ServerName = "SQLSERVER";
            Settings.ScriptsLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Set commands
            Settings.OpenServerNameDialogCommand = new RelayCommand(OpenServerNameDialog);
            Settings.OpenFolderDialogCommand = new RelayCommand(OpenFolderDialog);
            Settings.ExecuteScriptsCommand = new RelayCommand(ExecuteScripts, CanExecuteScripts);

            //Get files from selected folder
            FileList.Items = GetFilesFromFolder(Settings.ScriptsLocation);
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// Opens dialog to set server name
        /// </summary>
        /// <param name="parameter">Owner window of this dialog</param>
        private void OpenServerNameDialog(object parameter)
        {
            _dialogService.ShowDialog<ServerNameDialogViewModel, ServerNameDialog>(ServerNameDialogCloseCallback, parameter);
        }


        /// <summary>
        /// Opens dialog to select new folder
        /// </summary>
        /// <param name="parameter">Owner window of this dialog</param>
        private void OpenFolderDialog(object parameter)
        {
            _dialogService.ShowDialog<FolderDialogViewModel, FolderDialog>(FolderDialogCloseCallback, parameter);
        }


        /// <summary>
        /// Executes all selected SQL scripts from FileList View Model
        /// </summary>
        /// <param name="parameter"></param>
        public async void ExecuteScripts(object parameter)
        {
            //Reset active item
            FileList.ActiveItem = null;

            try
            {
                using SQLServerManager manager = new(Settings.ServerName);

                //Start SQL scripts execution
                for (var i = 0; i < FileList.Items.Count; i++)
                {
                    var item = FileList.Items[i];

                    //Item was not selected, skip this and continue with next one
                    if (item.IsChecked == false)
                    {
                        item.ActionText = String.Empty;
                        continue;
                    }

                    //Change item state
                    item.ItemState = FileListItemState.Execution;
                    //Get SQL script from the file
                    string sql = await TextFileIOManager.ReadFileContentAsync(item.FilePath);
                    //Execute SQL script and get results
                    List<DataTable> result = await manager.ExecuteSQLAsync(sql);

                    //Set action text according to result
                    if (result == null)
                    {
                        item.ActionText = String.Empty;
                    }
                    else
                    {
                        item.ActionText = "View Results";
                    }

                    // Set item state according to reported errors
                    if (manager.ExecutionErrors == String.Empty)
                    {
                        item.ItemState = FileListItemState.DoneSuccess;
                    }
                    else
                    {
                        item.ItemState = FileListItemState.DoneError;
                    }

                    //Store result
                    item.Results = result;
                }
            }
            catch (Exception ex)
            {
                //Reset state for executed item
                foreach (var item in FileList.Items)
                { 
                    if (item.ItemState == FileListItemState.Execution)
                        item.ItemState = FileListItemState.Selection;
                }

                //Show error message
                _messageService.ShowDialog(ex.Message);
            }
 
        }


        /// <summary>
        /// Checks if <see cref="ExecuteScripts(object)"/> can run
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True, if command can run</returns>
        public bool CanExecuteScripts(object parameter)
        {
            if (string.IsNullOrEmpty(Settings.ServerName) || 
                string.IsNullOrEmpty(Settings.ScriptsLocation) || 
                FileList.Items.Count() == 0 || 
                FileList.Items.Any(x=>x.ItemState != FileListItemState.Selection))
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        /// <summary>
        /// Sets active item for FileList View Model
        /// </summary>
        /// <param name="parameter">View Model of selected item</param>
        private async void SetActiveItem(object parameter)
        {
            var item = parameter as FileListItemViewModel;
            //Save active item
            FileList.ActiveItem = item;
            //Get SQL script for this item
            try
            {
                if (item.ItemState == FileListItemState.Selection)
                    item.FileContent = await TextFileIOManager.ReadFileContentAsync(item.FilePath);
            }
            catch(Exception ex)
            {
                _messageService.ShowDialog(ex.Message);
            }

        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Gets all files from selected folder and updates <see cref="FileList"/>
        /// </summary>
        private ObservableCollection<FileListItemViewModel> GetFilesFromFolder(string folderPath)
        {
            try
            {
                //Search for sql files
                var files = Directory.GetFiles(folderPath, "*.sql", SearchOption.TopDirectoryOnly);

                //Set model for each file
                var items =  new ObservableCollection<FileListItemViewModel>
                (
                    files.Select(file => new FileInfo(file))
                        .Select(file => new FileListItemViewModel
                        {
                            ItemState = FileListItemState.Selection,
                            IsChecked = false,
                            FileName = file.Name,
                            ActionText = "View Code",
                            FilePath = file.FullName,
                            SetActiveItemCommand = new RelayCommand(SetActiveItem)
                        })
                );

                return items;
            }
            catch
            {
                //No permission, no files will return
                return new ObservableCollection<FileListItemViewModel>();
            }

        }

        /// <summary>
        /// Callback that runs after FolderDialog window close
        /// </summary>
        /// <param name="parameter">Dialog View Model</param>
        private void FolderDialogCloseCallback(object parameter)
        {
            if (parameter is FolderDialogViewModel vm && vm.DirectoryStructure.SelectedItem != null)
            {
                //Reset active item
                FileList.ActiveItem = null;
                //Set folder
                Settings.ScriptsLocation = vm.DirectoryStructure.SelectedItem.FullPath;
                //Get new files from selected folder
                FileList.Items = GetFilesFromFolder(Settings.ScriptsLocation);
            }

        }

        /// <summary>
        /// Callback that runs after ServerName Dialog window will close
        /// </summary>
        /// <param name="parameter">Dialog View Model</param>
        private void ServerNameDialogCloseCallback(object parameter)
        {
            var vm = parameter as ServerNameDialogViewModel;

            if (vm is not null)
                Settings.ServerName = vm.ServerName;

        }

        #endregion
    }
}
