using System;
using System.Collections.Generic;
using System.Text;

namespace QbcBackend.Tools.QbcException
{
    public abstract class QbcBusinessException : Exception
    {

        #region protected methods

        protected abstract string GetStdMessage(string msg = "");

        protected abstract string GetStdCode(string code = "");


        #endregion


        public QbcBusinessException(string msg = "")
            : base(msg)
        {
            this.ErrorInfo = new List<ErrorModel>() { new ErrorModel(this.GetStdCode(), this.GetStdMessage(msg)) };
        }

        public QbcBusinessException(object resource, string property, string code = "", string message = "")
            : base()
        {
            this.ErrorInfo = new List<ErrorModel>() { new ErrorModel(resource, property, GetStdCode(code), this.GetStdMessage(message)) };

        }

        #region public properties

        public List<ErrorModel> ErrorInfo
        {
            get;
            set;
        }

        #endregion




    }
}
