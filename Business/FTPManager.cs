using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic; 
using System.Globalization;

namespace Business
{
    public class FTPCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class FTPDetails
    {
        public FTPCredentials Credentials { get; set; }
        public string FTPPath { get; set; }
        public string FTPFileName { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }
    public class FTPManager
    {
        /// <summary>
        /// Uploads a file to FTP
        /// </summary>
        /// A <param name="ftpPath">string that contains 
        /// FTP Path to upload the file</param>
        /// A <param name="ftpFileName">string that contains 
        /// filename to be uploaded</param>
        ///  A <param name="sourcePath">string that contains 
        /// source path of the filename to be uploaded</param>
        /// <returns>boolean value upon successs/failure of upload</returns>
        public bool Upload(FTPDetails ftpDetails)
        {
            FileInfo fileInfo = new FileInfo(ftpDetails.SourcePath);
            //To Provide Complete URI here             
            FtpWebRequest ftpRequest;
            string uri = Path.Combine(ftpDetails.FTPPath, ftpDetails.FTPFileName);
            ftpRequest = Login(uri, ftpDetails.Credentials);
            // Specify the command to be executed.
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
      //    ftpRequest.UsePassive = false; 
            // Specify the data transfer type.
            ftpRequest.UseBinary = true;
            // Notify the server about the size of the uploaded file
            ftpRequest.ContentLength = fileInfo.Length;
            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buffer = new byte[buffLength];
            int contentLength;
            FileStream fs = null;
            Stream stream = null;
            try
            {
                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                fs = fileInfo.OpenRead();
                // Stream to which the file to be upload is written
                stream = ftpRequest.GetRequestStream();
                // Read from the file stream 2kb at a time
                contentLength = fs.Read(buffer, 0, buffLength);
                // Till Stream content ends
                while (contentLength != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    stream.Write(buffer, 0, contentLength);
                    contentLength = fs.Read(buffer, 0, buffLength);
                }
                //System.Security.AccessControl.FileSecurity fSecurity = fileInf.GetAccessControl();
                //fSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule("Everyone", System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));
                // Close the file stream and the Request Stream
                stream.Close();
                stream.Dispose();
                fs.Close();
                fs.Dispose();
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Downloads the requested file to FTP
        /// </summary>
        /// A <param name="ftpPath">string that contains 
        /// FTP Path to upload the file</param>
        /// A <param name="ftpFileName">string that contains 
        /// filename to be uploaded</param>
        ///  A <param name="destinationPath">string that contains 
        /// destination path of the filename to be downloaded</param>
        /// <returns>boolean value upon successs/failure of download</returns>
        public bool Download(FTPDetails ftpDetails)
        {
            FtpWebRequest ftpRequest;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                FileStream outputStream = new FileStream(ftpDetails.DestinationPath + "\\" + ftpDetails.FTPFileName, FileMode.Create);
                string uri = Path.Combine(ftpDetails.FTPPath, ftpDetails.FTPFileName);
                ftpRequest = Login(uri, ftpDetails.Credentials);
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                //1  throw new VPSException(ErrorCode.FTPERROR, ex);
            }
            return true;
        }
        /// <summary>
        /// Fetches the list of files available in the 
        /// specified directory of FTP
        /// </summary>
        /// A <param name="ftpPath">string that contains FTP Path</param> 
        /// <returns>array of file names available in the FTP</returns>
        /// 

        //public string[] GetAllFiles(FTPDetails ftpDetails)
        //{            
        //    StringBuilder result = new StringBuilder();
        //    WebResponse response = null;
        //    StreamReader reader = null;
        //    string[] list = null;
        //    try
        //    {              
        //        FtpWebRequest ftpRequest;
        //        string uri = ftpDetails.FTPPath;               
        //        ftpRequest = Login(uri, ftpDetails.Credentials);               
        //        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
        //        ftpRequest.Proxy = null;                
        //        //ftpRequest.UsePassive = false;

        //        response = ftpRequest.GetResponse();               
        //        reader = new StreamReader(response.GetResponseStream());               
        //        list = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //        //string line = reader.ReadLine(); //1001
        //        foreach (string lines in list)
        //        {                    
        //            string data = lines;
        //            string date;                    
        //            DateTime dateTime;
        //           // try
        //           // {
        //           //     date = data.Substring(0, 17);
        //           //     dateTime = Convert.ToDateTime(date, CultureInfo.InvariantCulture);
        //           // }
        //           // catch
        //           // {
        //           //     date = string.Concat(System.DateTime.Now.Year,data.Substring(46, 12)) ;
        //           //     dateTime = Convert.ToDateTime(date);
        //           // }
        //           // ExceptionLogger.LogError("File Name: D:\\VPS Kalisma\\3.Coding\\VPSIntegrationBusiness\\FTPManager.cs; dateTime after parse at line No. 173: " + dateTime.ToString(), null);
        //           // string timeString24Hour = dateTime.ToString("MM/dd/yyyy HH:mm", CultureInfo.CurrentCulture);
        //           // //to be tested
        //           // data = data.Remove(0, 24);
        //           // data = data.Remove(0, 5);
        //           // data = data.Remove(0, 10);
        //           // string name = data;
        //           //// line = data + " " + timeString24Hour;
        //           // string datetime = timeString24Hour.Substring(timeString24Hour.Length - 16, 16);
        //           // result.Append(line);
        //           // //1001


        //            data = data.Remove(0, 56);
        //            string line = data;
        //            result.Append(line);
        //            result.Append("\n");
        //        }                
        //        //1001
        //        //while (line != null)
        //        //{
        //        //    result.Append(line);
        //        //    result.Append("\n");
        //        //    line = reader.ReadLine();
        //        //}
        //        //1001
        //        reader.Close();
        //        if (result.Length > 0)
        //        {
        //            // to remove the trailing '\n'
        //            result = result.Remove(result.ToString().LastIndexOf('\n'), 1);
        //            //result = result.ToString().Split('\n');
        //        }
        //        return result.ToString().Split('\n');
        //    }
        //    catch (Exception ex)
        //    {
        //        if (reader != null)
        //        {
        //            reader.Close();
        //        }
        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //        ExceptionLogger.LogError("File Name: D:\\VPS Kalisma\\3.Coding\\VPSIntegrationBusiness\\FTPManager.cs; Date at line No. error :" + ex.Message, null);
        //        throw new VPSException(ErrorCode.FTPERROR, ex);
        //    }
        //}
        public string[] GetAllFiles(FTPDetails ftpDetails)
        {
            string[] downloadFiles = null;
            StringBuilder result = new StringBuilder();
            FtpWebRequest ftpRequest = null;
            StreamReader reader = null;
            WebResponse response = null;
            try
            {
                string uri = ftpDetails.FTPPath;
                ftpRequest = Login(uri, ftpDetails.Credentials);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpRequest.Proxy = null;
                ftpRequest.UsePassive = false;
                response = ftpRequest.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                if (result.ToString().Trim().Length > 1)
                {
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    downloadFiles = result.ToString().Split('\n');
                    //downloadFiles = downloadFiles.Where(w => w != downloadFiles[0]).ToArray();

                }
                return downloadFiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (response != null)
                    response.Close();

            }
        }
        /// <summary>
        /// Fetches the filtered files available in the 
        /// specified directory of FTP
        /// </summary>
        /// <param name="ftpPath">string that contains FTP Path 
        /// to get the files</param>
        /// <param name="ftpFilter">string that contains filter
        /// to select the files</param>
        /// <returns></returns>
        public List<string> GetFilesOnFilter(FTPDetails ftpDetails, string ftpFilter)   
        {
            try
            {
                string[] ftpFileList = GetAllFiles(ftpDetails);
                if (ftpFileList != null)
                {
                   
                    var filteredFileList = from l in
                                               (from list in ftpFileList
                                                orderby list
                                                select list.Substring(39, list.Length - 39))
                                          where l.StartsWith(ftpFilter)
                                           select l;
                  
                    return filteredFileList.ToList();

                }
                return null;
            }
            catch (Exception ex)
            {
                //1   throw new VPSException(ErrorCode.FTPERROR, ex);
            }
            return null;
        }

        /// <summary>
        /// Deletes a file from FTP
        /// </summary>
        /// A <param name="fileName">string that contains 
        /// <param name="ftpPath">string that contains FTP Path 
        /// to delete the files</param>
        /// filename to be deleted</param>
        /// <returns>boolean value upon successs/failure of deleted</returns>
        public bool Delete(FTPDetails ftpDetails)
        {
            try
            {
                string uri = Path.Combine(ftpDetails.FTPPath, ftpDetails.FTPFileName);
                FtpWebRequest ftpRequest = Login(uri, ftpDetails.Credentials);
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                //1  throw new VPSException(ErrorCode.FTPERROR, ex);
            }
            return true;
        }

        /// <summary>
        /// Logs the user in to the FTP
        /// </summary>
        /// A <param name="uri">string that contains 
        /// url of the FTP</param>
        /// A <param name="UserName">string that contains 
        /// user name for logging in</param>
        /// A <param name="Password">string that contains 
        /// password for logging in</param>
        /// <returns>FTP Web Request object</returns>
        public FtpWebRequest Login(string uri, FTPCredentials credential)
        {
            try
            {
                FtpWebRequest ftpRequest;
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftpRequest.Credentials = new NetworkCredential(credential.UserName, credential.Password);
                ftpRequest.KeepAlive = false;
                return ftpRequest;
            }
            catch (Exception ex)
            {
              //  throw new VPSException(ErrorCode.FTPERROR, ex);
            }
            return null;
        }

        /// <summary>
        /// Check the existence of file in FTP
        /// </summary>
        /// <param name="ftpPath">string that contains FTP Path 
        /// to check the files</param>
        /// A<param name="fileName">string the contains filename</param>
        /// <returns>REturns true,if file exists in FTP</returns>
        public bool IsFileExist(FTPDetails ftpDetails)
        {
            try
            {
                string uri = string.Concat(ftpDetails.FTPPath, ftpDetails.FTPFileName);
                FtpWebRequest ftpRequest;
                ftpRequest = Login(uri, ftpDetails.Credentials);

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader ftpStream = new StreamReader(response.GetResponseStream());

                List<string> files = new List<string>();
                string file = ftpStream.ReadLine();
                while (file != null)
                {
                    files.Add(file);
                    file = ftpStream.ReadLine();
                }

                ftpStream.Close();
                response.Close();

                return files.Contains(ftpDetails.FTPFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
