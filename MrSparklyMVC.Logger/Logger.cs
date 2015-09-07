using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MrSparklyMVC.Logger
{
    /// <summary>
    /// Logging class for writing errors to text file
    /// </summary>
    public class Logger
    {
        //default filename
        string strFileName = "log.txt";

        #region constructors
        /// <summary>
        /// constructor for non-default filename
        /// </summary>
        /// <param name="fileName"></param>
        public Logger(string fileName)
        {
            strFileName = fileName;
            CreateTextFile();
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Logger()
        {
            CreateTextFile();
        }
        #endregion

        /// <summary>
        /// Creates a new Log File
        /// </summary>
        private void CreateTextFile()
        {
            try
            {
                FileStream outFile = new FileStream(strFileName, FileMode.Append,
                                                    FileAccess.Write);
                StreamWriter writer = new StreamWriter(outFile);
                outFile.Flush();
                writer.Close();
                outFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening/creating log file: " + ex.ToString());
            }            
        }

        /// <summary>
        /// Method for writing error to log file
        /// </summary>
        /// <param name="pErrorMsg"></param>
        /// <param name="pLogLevel"></param>
        /// <param name="pClassName"></param>
        /// <param name="pMethodName"></param>
        private void log(string pErrorMsg, string pLogLevel, string pClassName, string pMethodName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(strFileName, true);
                string strLogLine = DateTime.Now.ToLongDateString() + "|" + pLogLevel + "|" + pErrorMsg + "|" + pClassName + "|" + pMethodName;
                writer.WriteLine(strLogLine);
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to log file: " + ex.ToString());
            }
        }

        /// <summary>
        /// Method to write exception details to log file 
        /// </summary>
        /// <param name="pErrorMsg"></param>
        /// <param name="pLogLevel"></param>
        /// <param name="pEx"></param>
        private void log(string pErrorMsg, string pLogLevel, Exception pEx)
        {
            try
            {
                StreamWriter writer = new StreamWriter(strFileName, true);
                string strLogLine = DateTime.Now.ToLongDateString() + "|" + pLogLevel + "|" + pErrorMsg + "|" + pEx.ToString();
                writer.WriteLine(strLogLine);
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to log file: " + ex.ToString());
            }
        }
    }
}
