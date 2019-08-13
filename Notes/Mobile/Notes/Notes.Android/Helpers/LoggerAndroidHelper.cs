﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.SimpleLogger.Abstractions;
using System.IO;

namespace Notes.Droid.Helpers
{
    public class LoggerAndroidHelper : SimpleLoggerBase
    {
        private string accomplishPath(string fileOrSubfolderName)
        {
            var localFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            //var localFolder = "/Logger";
            return Path.Combine(localFolder, fileOrSubfolderName);
        }

        protected override bool AppendToFile(string filename, string message)
        {
            try
            {
                var fullPath = accomplishPath(filename);
                File.AppendAllText(fullPath, message + "\r\n");
                return true;
            }
            catch (System.IO.IOException ex)
            {
                // logging to file failed, we can't do anything better than return false 
                return false;
            }
        }

        protected override bool DeleteFile(string filename)
        {
            var fullPath = accomplishPath(filename);
            try
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
                return true;
            }
            catch (IOException ex)
            {
                return false;
            }
        }

        protected override long GetFileSizeKb(string fileName)
        {
            var fullPath = accomplishPath(fileName);
            try
            {
                if (File.Exists(fullPath))
                {
                    var fi = new FileInfo(fullPath);
                    return Convert.ToInt64(Math.Round((double)fi.Length / (double)1024));
                }
                return -1;
            }
            catch (IOException ex)
            {
                return -1;
            }
        }

        protected override string GetNextAvailableFileName(string logFileNameBase)
        {
            string theFileName = "";
            int i = 1;
            while (String.IsNullOrEmpty(theFileName))
            {
                var fn = String.Format("{0}{1}.log", logFileNameBase, i);
                if (File.Exists(accomplishPath(fn)))
                    i++;
                else
                    theFileName = fn;
            }
            return theFileName;
        }

        protected override string LoadFromFile(string filename)
        {
            try
            {
                var fullPath = accomplishPath(filename);
                if (File.Exists(fullPath))
                {
                    return File.ReadAllText(fullPath);

                }
                else
                {
                    File.Create(fullPath);
                    return filename;
                }  
            }
            catch (Exception ex)
            {
                // file read failed, return empty string
                return "";
            }
        }

        protected override bool SaveToFile(string filename, string message)
        {
            try
            {
                var fullPath = accomplishPath(filename);
                File.WriteAllText(fullPath, message);
                return true;
            }
            catch (System.IO.IOException ex)
            {
                // logging to file failed, we can't do anything better than return false 
                return false;
            }
        }
    }
}