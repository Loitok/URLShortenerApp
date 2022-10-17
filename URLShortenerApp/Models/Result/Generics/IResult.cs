using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortenerApp.Models.Result.Generics
{
    public interface IResult<out TData> : IResult
    {
        TData Data { get; }
    }
}
