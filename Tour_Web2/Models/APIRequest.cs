using static Tour_Web_Utility.SD;

namespace Tour_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; }

        public object Data {  get; set; }
    }
}
