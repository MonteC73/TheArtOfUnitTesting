
using System;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtenstionManager _manager;

        // Allows setting dependency via a property
        public IExtenstionManager ExtenstionManager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public bool WasLastFileNameValid { get; set; }

        public LogAnalyzer()
        {
            var extensionMenagerFactory = new ExtensionManagerFactory();
            _manager = extensionMenagerFactory.Create();
        }

        public LogAnalyzer(IExtenstionManager manager)
        {
            _manager = manager;
        }

        public bool IsValidLogFileName(string fileName)
        {
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

    public class ExtensionManagerFactory // refactor this to static
    {
        private IExtenstionManager _customManager = null;

        public IExtenstionManager Create()
        {
            return _customManager ?? new FileExtensionManager();
        }

        public void SetManager(IExtenstionManager manager)
        {
            _customManager = manager;
        }
    }

    public interface IExtenstionManager
    {
        bool IsValid(string fileName);
    }
}
