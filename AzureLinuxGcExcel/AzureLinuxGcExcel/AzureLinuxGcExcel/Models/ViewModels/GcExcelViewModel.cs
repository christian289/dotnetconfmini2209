namespace AzureLinuxGcExcel.Models.ViewModels
{
    public class GcExcelViewModel : Library.MyGcExcel
    {
        public GcExcelViewModel()
        {
            
        }

        public GcExcelViewModel(string inputFile) : base(inputFile)
        {

        }
    }
}
