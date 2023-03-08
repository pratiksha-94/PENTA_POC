using PUC.Contracts;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace PUC.PageModels
{
    public class MainPageModel : FreshMvvm.FreshBasePageModel
    {
        public string DriveName { get; set; }
        public string SelectedFolder { get; set; }

        private string _selectedFile;
        public string SelectedFile
        {
            get
            {
                return _selectedFile;
            }
            set
            {
                _selectedFile = value;
                RaisePropertyChanged(nameof(SelectedFile));
                _deviceService.OpenFile(SelectedFolder, SelectedFile);
            }
        }
        private IDeviceService _deviceService;
        public List<string> Files { get; set; }
        public string FolderName { get; set; }

        public MainPageModel()
        {
            DriveName = @"C:\";
            _deviceService = DependencyService.Get<IDeviceService>();

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
        public Command CreateFolderCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrWhiteSpace(DriveName))
                    {
                        await CoreMethods.DisplayAlert("ERROR", "PLEASE SELECT DRIVE", "Ok");
                        return;
                    }

                    if(!Directory.Exists(DriveName))
                    {
                        await CoreMethods.DisplayAlert("ERROR", "KINDLY SELECT EXISTING DRIVE", "Ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(FolderName))
                    {

                        await CoreMethods.DisplayAlert("ERROR", "PLEASE SELECT FOLDER", "Ok");
                        return;
                    }
                    string directoryPath = Path.Combine(DriveName, FolderName);

                    bool result = _deviceService.CreateFolder(directoryPath);
                    if (result)
                    {
                        await CoreMethods.DisplayAlert(
                            "SUCCESS"
                            , string.Format("Folder Created At {0}", directoryPath)
                            , "OK");
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert(
                            "ERRORR"
                            , string.Format("Error while creating folder At {0}", directoryPath)
                            , "OK");
                    }

                });

            }
        }

        public Command SelectFolderCommand
        {
            get
            {
                return new Command(() =>
                {
                    SelectedFolder = _deviceService.GetFolder();
                    RaisePropertyChanged(nameof(SelectedFolder));
                });
            }
        }

        public Command UploadFileToSelectedFolderCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(SelectedFolder))
                    {
                        await CoreMethods.DisplayAlert("ERROR", "PLEASE SELECT FOLDER TO CONTINUE", "Ok");
                        return;
                    }
                    _deviceService.SelectFilesUsingDialogAndThenCopyFilesToSelectedFolder(SelectedFolder);
                });
            }
        }

        public Command OpenFileCommand
        {
            get
            {
                return new Command(() =>
                {
                    Files = _deviceService.GetFilesFromFolder(SelectedFolder);
                    RaisePropertyChanged(nameof(Files));
                });
            }
        }

        public Command PickedFilesCommand
        {
            get
            {
                return new Command(() =>
                {
                    Files = _deviceService.GetFilesFromFolder(SelectedFolder);
                    RaisePropertyChanged(nameof(Files));
                });
            }
        }

    }
}
