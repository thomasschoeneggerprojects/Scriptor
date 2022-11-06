using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Service
{
    public class ServiceErrorInformation
    {
        public Exception Exception { get; private set; }

        public string Message { get; private set; }

        public string CallerMember { get; private set; }

        private ServiceErrorInformation(Exception exception, String message, String callerMember)
        {
            Exception = exception;
            Message = message;
            CallerMember = callerMember;
        }

        public static ServiceErrorInformation Create(Exception exception, String message, String callerMember)
        {
            return new ServiceErrorInformation(exception, message, callerMember);
        }
    }
}