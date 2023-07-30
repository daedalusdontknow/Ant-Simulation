using System;
using System.IO;

namespace Antsimulation.Managers
{
    internal class LogManager
    {
        private string logFilePath;

        public LogManager()
        {
            // Set the log file path to "latest.log" in the program's directory
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "latest.log");
        }

        public void WriteLog(string message)
        {
            try
            {
                // Append the log message to the log file
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur while writing to the log file
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}