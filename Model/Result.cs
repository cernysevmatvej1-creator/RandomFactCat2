using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDeepSeekl.Common
{

    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
      

        public static Result Ok(string message = null) =>
            new() { Success = true, Message = message ?? "Успешно" };

        public static Result Fail(string message) =>
            new() { Success = false, Message = message };
    }
    public class Result<T>
    {
        public T Data {  get; set; }    
        public bool Success { get; set; }
        public string Message { get; set; }

        public static Result<T> Ok(T data ,string message = null) =>
            new() { Data = data,  Success = true, Message = message ?? "Успешно" };

        public static Result<T> Fail(string message) =>
            new() { Success = false, Message = message };
    }
}
