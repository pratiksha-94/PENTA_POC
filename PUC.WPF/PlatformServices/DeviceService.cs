using PUC.Contracts;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PUC.WPF.PlatformServices
{
    public class DeviceService : IDeviceService
    {
        public bool CreateFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            return true;
        }

        public string GetFolder()
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
                return "";
                
            }
            
           
        
        public void SelectFolder(List<string> files, string destinationPath)
        {
            Directory.GetDirectories(destinationPath);
        }



        public void SelectFilesUsingDialogAndThenCopyFilesToSelectedFolder(string selectedFolderPath)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true
            };
            if (dialog.ShowDialog() == true)
            {
                var pickedFiles = (from file in dialog.FileNames
                                   let extension = Path.GetExtension(file)
                                   select file);

                foreach (string file in pickedFiles)
                {
                    string fileName = Path.GetFileName(file);
                    string destinationPath = Path.Combine(selectedFolderPath, fileName);
                    File.Copy(file, destinationPath, true);
                }
            }
        }

        public List<string> GetFilesFromFolder(string selectedfolder)
        {
            List<string> files = new List<string>();
            DirectoryInfo d = new DirectoryInfo(selectedfolder); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles(); //Getting Text files
          //  string str = "";

            foreach (FileInfo file in Files)
            {
                files.Add(file.FullName);
            }
            return files;
        }

        public void OpenFile(string selectedFolder, string selectedFile)
        {
            if (string.IsNullOrEmpty(selectedFile))
                return;
            Process fileopener = new Process();
            fileopener.StartInfo.FileName = selectedFile;
            //fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }


    }
}
