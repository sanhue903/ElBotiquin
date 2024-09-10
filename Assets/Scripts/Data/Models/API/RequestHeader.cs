
namespace RestClient.Core.Models
{
    public class RequestHeader
    {
        public RequestHeader(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
        public string Key { get;set; }

        public string Value { get; set; }   
    }
}