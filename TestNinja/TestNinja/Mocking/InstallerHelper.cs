using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private IFileDownloader _fileDownLoader;
        private string _setupDestinationFile;

        public InstallerHelper(IFileDownloader fileDownLoader)
        {
            _fileDownLoader = fileDownLoader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownLoader.DownloadFile(
                   string.Format("http://example.com/{0}/{1}",
                       customerName,
                       installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }

            //var client = new WebClient();
            //try
            //{
            //    client.DownloadFile(
            //       string.Format("http://example.com/{0}/{1}",
            //           customerName,
            //           installerName),
            //        _setupDestinationFile);

            //    return true;
            //}
            //catch (WebException)
            //{
            //    return false;
            //}
        }
    }
}