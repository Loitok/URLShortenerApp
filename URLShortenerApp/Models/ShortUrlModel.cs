using System;

namespace URLShortenerApp.Models
{
    public class ShortUrlModel
    {
        public int Id { get; set; }

        public string OriginalUrl { get; set; }
        public string GeneratedUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public UserMasterModel UserMaster { get; set; }
    }
}