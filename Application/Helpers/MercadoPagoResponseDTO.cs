namespace Application.Helpers
{
    public class MercadoPagoResponseDTO
    {
            public int id { get; set; }
            public bool live_mode { get; set; }
            public string type { get; set; }
            public string date_created { get; set; }
            public int userid { get; set; }
            public string api_version { get; set; }
            public string action { get; set; }
            public Data data { get; set; }
    }
    public class Data
    {
        public string id { get; set; }
    }
}
