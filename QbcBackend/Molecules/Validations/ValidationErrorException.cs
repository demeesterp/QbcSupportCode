using FluentValidation.Results;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;

namespace QbcBackend.Botany.Validation
{
    public class ValidationErrorException : QbcBusinessException
    {

        #region private properties

        private const string stdMessage = "Validation failed for the resource";
        private const string stdCode = "ValidationFailedError";

        #endregion


        protected override string GetStdMessage(string msg = "")
        {
            string retval = msg;
            if (String.IsNullOrWhiteSpace(msg))
            {
                retval = stdMessage;
            }
            return retval;
        }

        protected override string GetStdCode(string code = "")
        {
            string retval = code;
            if (String.IsNullOrWhiteSpace(code))
            {
                retval = stdCode;
            }
            return retval;
        }


        public ValidationErrorException(string msg = stdMessage) : base(msg) { }


        public ValidationErrorException(List<ErrorModel> errors, string msg = stdMessage)
        {
            this.ErrorInfo = errors;
        }

        public ValidationErrorException(ValidationResult validationResults, string msg = stdMessage)
            : base(msg)
        {
            this.ErrorInfo = new List<ErrorModel>();
            foreach (var v in validationResults.Errors)
            {
                this.ErrorInfo.Add(new ErrorModel()
                {
                    ErrorCode = v.ErrorCode,
                    ErrorMessage = v.ErrorMessage,
                    ErrorValue = v.AttemptedValue,
                    PropertyName = v.PropertyName,
                    ResourceName = v.ResourceName
                });
            }
        }


    }
}
