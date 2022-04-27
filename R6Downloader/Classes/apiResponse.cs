namespace R6Downloader
{
    public class apiResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string binary { get; set; }
        public string content { get; set; }

        public apiResponse(string _status, string _message, string _binary, string _content)
        {
            this.status = _status;
            this.message = _message;
            this.binary = _binary;
            this.content = _content;
        }

        public bool isValid()
        {
            if (this.status == "success")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}