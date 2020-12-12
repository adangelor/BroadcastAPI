namespace BroadcastApi.ViewModels
{
    public class Response
    {
        public Response()
        {

        }

        public Response(object res, string msgExito)
        {
            IsSuccess = true;
            Result = res;
            Message = msgExito;
        }

        public Response(string msgError)
        {
            IsSuccess = false;
            Result = null;
            Message = msgError;
        }

        public Response(string msgError, int codeError)
        {
            IsSuccess = false;
            Result = null;
            Message = msgError;
            ResponseCode = codeError;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public int ResponseCode { get; set; }
    }
}
