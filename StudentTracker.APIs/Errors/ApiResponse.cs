﻿namespace StudentTracker.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode, string? message = null) 
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                400 => "A Bad Request, you have made",
                401 => "Unauthorized",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark side. Errors lead to anger, anger leads to hate and hate leads to career change",
                _ => null
            };
        }
    }
}
