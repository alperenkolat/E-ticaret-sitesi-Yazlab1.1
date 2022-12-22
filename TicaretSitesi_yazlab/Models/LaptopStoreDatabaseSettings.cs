namespace TicaretSitesi_yazlab.Models
{
    public class LaptopStoreDatabaseSettings
    {
     
            public string ConnectionString { get; set; } = null!;

            public string DatabaseName { get; set; } = null!;

            public string laptopsCollectionName { get; set; } = null!;
            public string loginsCollectionName { get; set; } = null!;


    }
}
