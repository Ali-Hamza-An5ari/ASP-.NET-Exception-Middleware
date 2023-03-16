namespace dapperCRUD.Middleware.With_Exception
{
    public class AException : Exception
    {
        public string ErrorCode { get; }

        public AException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public void GetException()
        {
            // throw custom exception with an error code
            throw new AException("An error occurred.", "ERR001");
        }

    }

}
