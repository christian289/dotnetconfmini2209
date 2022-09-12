using AzureLinuxGcExcel.Library;

namespace AzureLinuxGcExcel.Models.ViewModels
{
    public class SSGViewModel : MySSG
    {
        public SSGViewModel()
        {

        }

        public SSGViewModel(string inputFile) : base(inputFile)
        {

        }
    }
}
