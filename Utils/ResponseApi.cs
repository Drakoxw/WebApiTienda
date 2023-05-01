
namespace WebApiTienda.Utils
{
    public class ResponseApi<T>
    {
        private string status;
        private string message;
        private T response;

        public ResponseApi(T data, string status, string message)
        {
            this.response = data;
            this.status = status;
            this.message = message;
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public T Response
        {
            get { return response; }
            set { response = value; }
        }
    }

}
