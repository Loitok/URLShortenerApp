using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data;
using URLShortenerApp.Helpers;
using URLShortenerApp.Models;
using URLShortenerApp.Models.Result;
using URLShortenerApp.Models.Result.Generics;
using URLShortenerApp.Services.Abstraction;

namespace URLShortenerApp.Services.Implementation
{
    public class ShortUrlService : IShortUrlService

    {
        private readonly UrlShortenerContext _context;

        public ShortUrlService(UrlShortenerContext context)
        {
            _context = context;
        }

        public async Task<IResult<ShortUrlModel>> GetById(int id)
        {
            try
            {
                var result = await _context.ShortUrls.FindAsync(id);

                return Result<ShortUrlModel>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return Result<ShortUrlModel>.CreateFailure("Get by Id Error", e);
            }
        }

        public async Task<IResult<ShortUrlModel>> GetByPath(string path)
        {
            try
            {
                var result = await _context.ShortUrls.FindAsync(ShortUrlHelper.Decode((path)));

                return Result<ShortUrlModel>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return Result<ShortUrlModel>.CreateFailure("Get by Path error", e);
            }
        }

        public async Task<IResult<List<ShortUrlModel>>> GetAll()
        {
            try
            {
                var result = await _context.ShortUrls.ToListAsync();

                return Result<List<ShortUrlModel>>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return Result<List<ShortUrlModel>>.CreateFailure("Get shortUrls error", e);
            }
        }

        public async Task<IResult> DeleteShortUrl(int shortUrlId)
        {
            try
            {
                var shortUrl = await _context.ShortUrls
                    .FirstAsync(x => x.Id == shortUrlId);
                
                _context.ShortUrls.Remove(shortUrl);

                await _context.SaveChangesAsync();

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailure("Delete Short Url error", e);
            }
        }

        public async Task<IResult<int>> Save(ShortUrlModel shortUrl)
        {
            try
            {
                shortUrl.CreatedOn = DateTime.Now;
                await _context.ShortUrls.AddAsync(shortUrl);
                await _context.SaveChangesAsync();

                return Result<int>.CreateSuccess(shortUrl.Id);
            }
            catch (Exception e)
            {
                return Result<int>.CreateFailure("Save ShortURL error", e);
            }
        }
    }
}
