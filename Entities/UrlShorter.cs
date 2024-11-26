namespace UrlShortener.Entities
{
    public class UrlShorter
    {
        public Guid Id { get; set; }
        public string OriginUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
