using Newtonsoft.Json;

namespace AzureLinuxGcExcel.Models.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

            ChartDatas = new List<_ChartData>();
        }

        public List<_ChartData> ChartDatas { get; set; }

        public string ChartDatasJson => JsonConvert.SerializeObject(ChartDatas);


        public class _ChartData
        {
            public _ChartData(string fileName, object[,] data)
            {
                this.FileName = fileName;
                this.Data = data;
            }

            public string FileName { get; set; }

            public object[,] Data { get; set; }

        }
    }
}
