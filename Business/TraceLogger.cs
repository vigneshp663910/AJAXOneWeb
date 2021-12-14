using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Business
{
    public static class TraceLogger
    {
        #region Class Variables
        static bool enableTraceLog = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugTraceOn"]);
        #endregion

        #region Public Methods
        /// <summary>
        /// This method would write object name, method name, entry time into
        /// the method, exit time from the method, key input data value and key
        /// output data value into a text file. Text file path is configurable
        /// in web config. Trace log would be written into a text file when
        /// DebugTraceOn value is set to true in web config.
        /// </summary>
        /// <param name="startTime">DateTime</param>
        /// <param name="inputData">string</param>
        /// <param name="outputData">string</param>
        public static void Log(DateTime startTime, string inputData = "", string outputData = "")
        {
            try
            {
                //Checks if trace log capture is turned on.
                if (enableTraceLog)
                {
                    string filePath = Convert.ToString(ConfigurationManager.AppSettings["LogPath"]);

                    StackTrace stackTrace = new StackTrace(true);          // get call stack
                    StackFrame stackFrame = stackTrace.GetFrame(1);  // get method calls (frames)

                    StringBuilder traceLogInfo = new StringBuilder();
                    traceLogInfo.Append("Object Name: ");
                    traceLogInfo.Append(stackFrame.GetFileName());
                    traceLogInfo.Append(",");
                    traceLogInfo.Append("Method Name: ");
                    traceLogInfo.Append(stackFrame.GetMethod());
                    traceLogInfo.Append(",");
                    traceLogInfo.Append("Start Time: ");
                    traceLogInfo.Append(startTime);
                    traceLogInfo.Append(",");
                    traceLogInfo.Append("End Time: ");
                    traceLogInfo.Append(DateTime.Now);
                    traceLogInfo.Append(",");
                    traceLogInfo.Append("Input Data: ");
                    traceLogInfo.Append(inputData);
                    traceLogInfo.Append(",");
                    traceLogInfo.Append("Output Data: ");
                    traceLogInfo.Append(outputData);
                    if (!File.Exists(filePath))
                    {
                        FileStream aFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(aFile);
                        sw.Write(Convert.ToString(traceLogInfo));
                        sw.WriteLine("-----------------------------------------------------------");
                        sw.Close();
                        aFile.Close();
                    }
                    else
                    {
                        FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(aFile);
                        sw.WriteLine(Convert.ToString(traceLogInfo));
                        sw.WriteLine("-----------------------------------------------------------");
                        sw.Close();
                        aFile.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError("Unable to log the trace information", ex);
            }
        }
        #endregion
    }
}
