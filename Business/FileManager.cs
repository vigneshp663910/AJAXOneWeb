using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Net; 
using System.IO;
using System.Xml.Serialization;
using System.Xml.Xsl; 
using System.Data;
using System.Data.OleDb;
 

namespace Business
{
    public class FileManager
    {
        public static FTPDetails GetFTPDetails(string FTPPath, string FTPUserName, string FTPPassword)
        {
            FTPCredentials credential = new FTPCredentials()
            {
                UserName = FTPUserName,
                Password = FTPPassword
            };
            return new FTPDetails()
            {
                Credentials = credential,
                FTPPath = FTPPath
            };
        }
        public static string GetFileName(string fileNameStartsWith, string extension)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}.{8}", fileNameStartsWith, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, extension);
        }
        public void DownloadAllFilesToBeImported(string FTPPath, string FTPUserName, string FTPPassword, string DestinationPath, string FTPFileName)
        {
            FTPManager ftp = new FTPManager();
            List<string> fileNames = new List<string>();
            FTPDetails ftpDetails = GetFTPDetails(FTPPath, FTPUserName, FTPPassword);
            //try
            //{
            fileNames = ftp.GetFilesOnFilter(ftpDetails, FTPFileName);
            ftpDetails.DestinationPath = DestinationPath;
            if (fileNames == null)
                fileNames = new List<string>();
            foreach (string file in fileNames)
            {
                try
                {
                    ftpDetails.FTPFileName = file;
                    ftp.Download(ftpDetails);
                    try
                    {
                        ftp.Delete(ftpDetails);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogError(string.Format("Unable to delete the file {0} from ftp", ftpDetails.FTPFileName), ex);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(string.Format("Unable to download the file {0} from ftp", ftpDetails.FTPFileName), ex);
                }
            }
            //}
            //catch (VPSException vpsEx)
            //{
            //    throw vpsEx;
            //}
            //catch (Exception ex)
            //{
            //    throw new VPSException(ErrorCode.GENE, ex);
            //}
        }
        public Boolean UploadFile(string FTPPath, string FTPUserName, string FTPPassword, string SourcePath, string FTPFileName)
        {
            try
            {
                FTPManager ftp = new FTPManager();
                FTPDetails ftpDetails = GetFTPDetails(FTPPath, FTPUserName, FTPPassword);
                ftpDetails.SourcePath = SourcePath;
                ftpDetails.FTPFileName = FTPFileName;
                ftp.Upload(ftpDetails);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MoveFile(FileInfo sourceFile, string destinationPath)
        {
            string newFileName = string.Empty;
            try
            {
                string filename = Path.Combine(destinationPath, sourceFile.Name);

                if (File.Exists(filename))
                {
                    string tmpFileName = sourceFile.Name.Replace(sourceFile.Extension, string.Empty);
                    newFileName = GetFileName(tmpFileName, sourceFile.Extension);
                    newFileName = Path.Combine(destinationPath, newFileName);
                }
                else
                    newFileName = filename;

                sourceFile.MoveTo(newFileName);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(string.Format("Unable to move the  the file {0} to the location {1} ", sourceFile.FullName, newFileName), ex);
            }
        }
        public void DeleteFile(string file)
        {
            try
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch (IOException ioExp)
            {
                //  throw new VPSException(ErrorCode.IOE, ioExp);
            }
            catch (InvalidOperationException inExp)
            {
                //  throw new VPSException(ErrorCode.IOE, inExp);
            }
            catch (Exception ex)
            {
                //  throw new VPSException(ErrorCode.GENE, ex);
            }
        }
 
    }
}
