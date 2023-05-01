
namespace WebApiTienda.Utils
{
    public class ResponseApi<T>
    {
        private string status;
        private string message;
        private T data;

        public ResponseApi(T data, string status, string message)
        {
            this.data = data;
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

        public T Data
        {
            get { return data; }
            set { data = value; }
        }
    }

}
