
using System;

namespace LogAn
{
    public class LogAnalyzer
    {
        private readonly IExtenstionManager _manager;

        public bool WasLastFileNameValid { get; set; }

        public LogAnalyzer(IExtenstionManager manager)
        {
            _manager = manager;
        }

        public bool IsValidLogFileName(string fileName)
        {
            //IExtenstionManager manager = new FileExtensionManager();
            WasLastFileNameValid = _manager.IsValid(fileName);

            return WasLastFileNameValid;
        }
    }

    public class FileExtensionManager : IExtenstionManager
    {
        public bool IsValid(string fileName)
        {

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("filename has to be provided");
            }

            if (!fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }

    public interface IExtenstionManager
    {
        bool IsValid(string fileName);
    }
}
