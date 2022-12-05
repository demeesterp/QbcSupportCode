using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QbcBackend.Botany.Validation;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;

namespace QbcApi.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public GlobalExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            var r = HandleException(context.Exception as NotExistsException);
            if (r == null)
            {
                r = HandleException(context.Exception as NotUniqueException);
            }
            if (r == null)
            {
                r = HandleException(context.Exception as ValidationErrorException);
            }
            if (r == null)
            {
                r = HandleException(context.Exception as ArgumentNullException);
            }
            if (r == null)
            {
                r = HandleException(context.Exception);
            }
            context.Result = r;
        }


        #region private helpers


        private ActionResult HandleException(NotExistsException exc)
        {
            ActionResult retval = null;
            if (exc != null)
            {
                retval = new ObjectResult(exc.ErrorInfo) { StatusCode = StatusCodes.Status404NotFound };
            }
            return retval;
        }

        private ActionResult HandleException(NotUniqueException exc)
        {
            ActionResult retval = null;
            if (exc != null)
            {
                retval = new ObjectResult(exc.ErrorInfo) { StatusCode = StatusCodes.Status406NotAcceptable };
            }
            return retval;
        }

        private ActionResult HandleException(ValidationErrorException exc)
        {
            ActionResult retval = null;
            if (exc != null)
            {
                retval = new ObjectResult(exc.ErrorInfo) { StatusCode = StatusCodes.Status406NotAcceptable };
            }
            return retval;
        }

        private ActionResult HandleException(ArgumentNullException exc)
        {
            ActionResult retval = null;
            if (exc != null)
            {
                var error = new ErrorModel()
                {
                    ErrorCode = exc.GetType().Name,
                    ErrorMessage = exc.Message,
                    PropertyName = exc.ParamName
                };

                retval = new ObjectResult(error)
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                };
            }
            return retval;
        }

        private ActionResult HandleException(Exception exc)
        {
            ActionResult retval = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            List<ErrorModel> errors = new List<ErrorModel>();
            Exception current = exc;
            while (current != null)
            {
                errors.Add(new ErrorModel()
                {
                    ErrorCode = exc.GetType().FullName,
                    ErrorMessage = exc.Message,
                    ErrorValue = exc.StackTrace
                });
                current = exc.InnerException;
            }
            return retval;
        }

        #endregion

    }
}
