namespace SimpleCRUD.Infrastructure.Services
{
    public class Response<T> where T : class, new()
    {
        /// <summary>
        /// constructor to set data
        /// </summary>
        /// <param name="status">can be true or false</param>
        /// <param name="data">can be a single or multiple objects.</param>
        public Response(bool status, T data)
        {
            IsSuccess = status;
            Data = data;
        }
        public Response(bool status, string errMessage)
        {
            IsSuccess = status;
            ErrorMessage = errMessage;
        }

        public bool IsSuccess = false;
        public T Data;
        public string ErrorMessage;
    }
}
