using System;
using System.Collections.Generic;
using System.Text;

namespace QbcBackend.Tools.QbcException
{
    public class NotExistsException : QbcBusinessException
    {
        #region private properties

        private const string stdMessage = "The requested resource does not exist!";
        private const string stdCode = "NotExistsError";

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

        public NotExistsException(string msg = stdMessage)
            : base(msg) { }



        public NotExistsException(object resource, string property, string code = stdCode, string message = stdMessage)
            : base(resource, property, code, message) { }




    }
}
