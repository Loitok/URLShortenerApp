using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortenerApp.Models;
using URLShortenerApp.Models.Result;
using URLShortenerApp.Models.Result.Generics;

namespace URLShortenerApp.Services.Abstraction
{
    public interface IHomeService
    {
        Task<IResult<UserMasterModel>> FindByEmail(string email);
        Task<IResult> Register(UserMasterModel user);
        Task<IResult<List<UserMasterModel>>> FindByPassword(string email, string password);
    }
}
