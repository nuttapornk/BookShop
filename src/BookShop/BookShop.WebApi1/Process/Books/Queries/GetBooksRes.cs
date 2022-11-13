using ReflectionIT.Mvc.Paging;
using System.Text.Json.Serialization;

namespace BookShop.WebApi1.Process.Books.Queries
{
    public class GetBooksRes
    {
        public GetBooksRes()
        {
            this.Data = new List<GetBooksResData>();
        }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; }

        [JsonPropertyName("data")]
        public List<GetBooksResData> Data { get; set; }

    }


    public class GetBooksResData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("isbn")]
        public string Isbn { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal? Price { get; set; } = null;

        [JsonPropertyName("status")]
        public bool Status { get; set; } = false;
    }

}
