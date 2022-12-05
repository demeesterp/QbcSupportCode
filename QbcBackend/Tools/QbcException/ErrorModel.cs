using System;

namespace QbcBackend.Tools.QbcException
{
    public class ErrorModel
    {


        public ErrorModel()
        {

        }

        public ErrorModel(string code, string message)
        {
            this.ErrorMessage = code;
            this.ErrorCode = message;
        }

        public ErrorModel(object resource, string property, string code, string message)
        {
            this.ErrorMessage = message;
            this.ErrorCode = code;
            Type currentType = resource.GetType();
            var propertyInfo = currentType.GetProperty(property);
            if (propertyInfo != null)
            {
                var propertyValue = propertyInfo.GetValue(resource);
                if (propertyValue != null)
                {
                    this.ErrorValue = propertyValue;
                }
            }
            this.ResourceName = currentType.FullName;
            this.PropertyName = property;
        }



        public string ErrorCode
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public object ErrorValue
        {
            get;
            set;
        }

        public string PropertyName
        {
            get;
            set;
        }


        public string ResourceName
        {
            get;
            set;
        }


    }
}
