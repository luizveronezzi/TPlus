namespace TVPlus.Model
{
    public class apiretorno 
    {
        public bool success { get; set; }
        public int statuscode { get; set; }
        public string mensage { get; set; }
        public dynamic data { get; set; }
        public byte[] databyte { get; set; }

        public apiretorno()
        {
            success = true;
            statuscode = 200;
            mensage = "OK";
            data = null;
        }

        public apiretorno ApiOk()
        {
            return this;
        }
        public apiretorno ApiOk(string message)
        {
            mensage = message;
            return this;
        }
        public apiretorno ApiOk(dynamic dados)
        {
            data = dados;
            return this;
        }
        public apiretorno ApiOk(string message, dynamic dados)
        {
            mensage = message;
            data = dados;
            return this;
        }
        public apiretorno ApiError()
        {
            success = false;
            statuscode = 400;
            mensage = "ERRO";
            return this;
        }
        public apiretorno ApiError(string message)
        {
            success = false;
            statuscode = 400;
            mensage = message;
            return this;
        }
        public apiretorno ApiError(dynamic dados)
        {
            success = false;
            statuscode = 400;
            mensage = "ERRO";
            data = dados;
            return this;
        }
        public apiretorno ApiError(string message, dynamic dados)
        {
            success = false;
            statuscode = 400;
            mensage = message;
            data = dados;
            return this;
        }
    }
}
