using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanChat.API.Response
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.Code = "1";
            response.ResponseData = ex.Message;
            return response;
        }
        public static ApiResponse GetApiResponse(ResponseType type, object? contract)
        {
            ApiResponse response;

            response = new ApiResponse { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "0";
                    response.Message = "Success";
                    break;
                case ResponseType.AlreadyExist:
                    response.Code = "3";
                    response.Message = "Already exists";
                    break;
                case ResponseType.NotFound:
                    response.Code = "2";
                    response.Message = "Not Found";
                    break;
                case ResponseType.Failure:
                    response.Code = "4";
                    response.Message = "Fail";
                    break;
            }
            return response;
        }
    }
}
