using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortenerApp.Models;
using URLShortenerApp.Models.Result;
using URLShortenerApp.Models.Result.Generics;

namespace URLShortenerApp.Services.Abstraction
{
    public interface IShortUrlService
    {
        Task<IResult<ShortUrlModel>> GetById(int id);
        Task<IResult<ShortUrlModel>> GetByPath(string path);
        Task<IResult<int>> Save(ShortUrlModel shortUrl);
        Task<IResult<List<ShortUrlModel>>> GetAll();
        Task<IResult> DeleteShortUrl(int shortUrlId);
    }
}
