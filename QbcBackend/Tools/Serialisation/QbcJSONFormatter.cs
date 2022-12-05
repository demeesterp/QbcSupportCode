using QbcBackend.Tools.Base.Model;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace QbcBackend.Tools.Serialisation
{
    /// <summary>
    /// Serialize Deserialize object to and from JSON string or JSON Binary 
    /// </summary>
    public class QbcJSONFormatter : IQbcFormatter
    {

        #region  private properties

        private DataContractJsonSerializer CreateSerializer(Type inputType)
        {
            return new DataContractJsonSerializer(inputType, new DataContractJsonSerializerSettings()
            {
                DateTimeFormat = new DateTimeFormat(Options.DateTimeFormatString)
            });
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public QbcJSONFormatter()
        {
            this.Options = new DefaultQbcFormatterOptions();
        }

        /// <summary>
        /// Formatter options : e.g default date format in string
        /// </summary>
        public IQbcFormatterOptions Options
        {
            get;
            set;
        }

        /// <summary>
        /// Deserialize object from message 
        /// </summary>
        /// <typeparam name="TMsg">Objecttype to deserialize to</typeparam>
        /// <param name="msg">binary message to be deserialized</param>
        /// <returns>Deserialized object</returns>
        public TMsg DeserializeObject<TMsg>(byte[] msg) where TMsg : class
        {
            TMsg retval = default(TMsg);
            using (MemoryStream stream = new MemoryStream(msg))
            {
                DataContractJsonSerializer ser = CreateSerializer(typeof(TMsg));
                retval = ser.ReadObject(stream) as TMsg;
            }
            return retval;
        }

        /// <summary>
        ///  Deserialize object from binary message 
        /// </summary>
        /// <param name="msg">binary message to be deserialized</param>
        /// <param name="targetType">Final object type to instantiate from stream</param>
        /// <returns>Deserialize object</returns>
        public QbcBase DeserializeObject(byte[] msg, Type targetType)
        {
            QbcBase retval = null;
            using (MemoryStream stream = new MemoryStream(msg))
            {
                DataContractJsonSerializer ser = CreateSerializer(targetType);
                retval = ser.ReadObject(stream) as QbcBase;
            }
            return retval;
        }

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="TMsg">Objecttype to deserialize to</typeparam>
        /// <param name="msg">string message to de deserialized</param>
        /// <returns>Deserialized object</returns>
        public TMsg DeserializeObjectFromString<TMsg>(string msg) where TMsg : class
        {
            return DeserializeObject<TMsg>(Encoding.UTF8.GetBytes(msg));
        }

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <param name="msg">string message to be deserialized</param>
        /// <param name="targetType">target type to deserialize to</param>
        /// <returns>deserialized object</returns>
        public QbcBase DeserializeObjectFromString(string msg, Type targetType)
        {
            return DeserializeObject(Encoding.UTF8.GetBytes(msg), targetType);
        }

        /// <summary>
        /// Serialize object to byte array
        /// </summary>
        /// <typeparam name="TMsg">Object type to be serialized</typeparam>
        /// <param name="message">object to be serialized</param>
        /// <returns>Serialized byte array</returns>
        public byte[] SerializeObject<TMsg>(TMsg message) where TMsg : class
        {
            byte[] retval = null;
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer ser = CreateSerializer(typeof(TMsg));
                ser.WriteObject(stream, message);
                retval = stream.ToArray();
            }
            return retval;
        }

        /// <summary>
        /// SerializeObject BaseGenco to Byte Arrau
        /// </summary>
        /// <param name="message">resource to be serialized</param>
        /// <returns>Serialized byte array</returns>
        public byte[] SerializeObject(QbcBase message)
        {
            byte[] retval = null;
            using (MemoryStream stream = new MemoryStream())
            {

                DataContractJsonSerializer ser = CreateSerializer(message.GetType());
                ser.WriteObject(stream, message);
                retval = stream.ToArray();
            }
            return retval;
        }

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <typeparam name="TMsg">Object type to be serialized</typeparam>
        /// <param name="message">resource to be serialized</param>
        /// <returns>Serialized string</returns>
        public string SerializeObjectToString<TMsg>(TMsg message) where TMsg : class
        {
            string retval = string.Empty;
            byte[] result = SerializeObject(message);
            if ( result != null && result.Length > 0)
            {
                retval = Encoding.UTF8.GetString(result, 0, result.Length);
            }
            return retval;
        }

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <param name="message">resource to be serialized</param>
        /// <returns>string with serialized object</returns>
        public string SerializeObjectToString(QbcBase message)
        {
            string retval = string.Empty;
            byte[] result = SerializeObject(message);
            if (result != null && result.Length > 0)
            {
                retval = Encoding.UTF8.GetString(result, 0, result.Length);
            }
            return retval;
        }
    }
}
