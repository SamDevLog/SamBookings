namespace Api.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set;}
        public string Message { get; set;}

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode
            switch{
                400 => "You have made a bad request you naughty!",
                401 => "Ummmm! Looks like you are now supposed to be here!!",
                404 => "We unfortunately don't have what you came looking for!!",
                500 => "We messed up our server code! Plz do not tell our boss!",

                _ => null
            };
        }
    }
}