namespace URLShortenerApp.Models.Result
{
    public interface IResult
    {
        bool Success { get; }
        ResponseMessage ErrorMessage { get; }
    }
}
