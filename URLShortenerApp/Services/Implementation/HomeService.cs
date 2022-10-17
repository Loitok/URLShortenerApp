using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data;
using URLShortenerApp.Models;
using URLShortenerApp.Models.Result;
using URLShortenerApp.Models.Result.Generics;
using URLShortenerApp.Services.Abstraction;

namespace URLShortenerApp.Services.Implementation
{
    public class HomeService : IHomeService
    {
        private readonly UrlShortenerContext _context;

        public HomeService(UrlShortenerContext context)
        {
            _context = context;
        }

        public async Task<IResult<UserMasterModel>> FindByEmail(string email)
        {
            try
            {
                var check = await _context.UserMasterModels
                    .FirstOrDefaultAsync(s => s.Email == email);

                return Result<UserMasterModel>.CreateSuccess(check);
            }
            catch (Exception e)
            {
                return Result<UserMasterModel>.CreateFailure("Find by Email error", e);
            }
            
        }

        public async Task<IResult> Register(UserMasterModel user)
        {
            try
            {
                user.Password = GetMD5(user.Password);
                await _context.UserMasterModels.AddAsync(user);
                await _context.SaveChangesAsync();

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailure("Register Error", e);
            }
        }

        public async Task<IResult<List<UserMasterModel>>> FindByPassword(string email, string password)
        {
            try
            {
                var fPassword = GetMD5(password);
                var data = await _context.UserMasterModels
                    .Where(s => s.Email.Equals(email)
                                && s.Password.Equals(fPassword)).ToListAsync();
                
                return Result<List<UserMasterModel>>.CreateSuccess(data);
            }
            catch (Exception e)
            {
                return Result<List<UserMasterModel>>.CreateFailure("Find by Password Error", e);
            }
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
