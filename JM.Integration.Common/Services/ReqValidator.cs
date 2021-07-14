namespace JM.Integration.Common.Services
{
    public class ReqValidator : IReqValidator
    {
        public bool IsValidRequest(string request)
        {
            return string.IsNullOrWhiteSpace(request) ? false : true;
        }
    }
}