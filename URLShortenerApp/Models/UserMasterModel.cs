using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace URLShortenerApp.Models
{
    public class UserMasterModel
    {
        public UserMasterModel()
        {
            ShortUrlModels = new List<ShortUrlModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public Role Role { get; set; }

        public List<ShortUrlModel> ShortUrlModels { get; set; }
    }

    public enum Role : byte
    {
        Admin,
        User
    }
}
