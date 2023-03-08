using System;
using System.Collections.Generic;
using System.Text;

namespace PUC.Contracts
{
    public interface IDeviceService
    {
        bool CreateFolder(string folderName);

        string GetFolder();
        List<string> GetFilesFromFolder(string selectedfolder);

        void SelectFolder(List<string> files, string destinationPath);

        void SelectFilesUsingDialogAndThenCopyFilesToSelectedFolder(string selectedFolderPath);

        void OpenFile(string selectedFolder, string selectedFile);
      

    }
}
