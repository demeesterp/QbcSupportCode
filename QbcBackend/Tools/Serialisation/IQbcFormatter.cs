using QbcBackend.Tools.Base.Model;
using System;

namespace QbcBackend.Tools.Serialisation
{
    /// <summary>
    /// Formatter used to serialize and deserialize object to and from strings and/or byte arrays
    /// </summary>
    public interface IQbcFormatter
    {
        /// <summary>
        /// Serialize object to byte array
        /// </summary>
        /// <typeparam name="TMsg">Object type to be serialized</typeparam>
        /// <param name="message">object to be serialized</param>
        /// <returns>Serialized byte array</returns>
        Byte[] SerializeObject<TMsg>(TMsg message) where TMsg : class;

        /// <summary>
        /// SerializeObject BaseGenco to Byte Arrau
        /// </summary>
        /// <param name="message">resource to be serialized</param>
        /// <returns>Serialized byte array</returns>
        Byte[] SerializeObject(QbcBase message);

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <typeparam name="TMsg">Object type to be serialized</typeparam>
        /// <param name="message">resource to be serialized</param>
        /// <returns>Serialized string</returns>
        string SerializeObjectToString<TMsg>(TMsg message) where TMsg : class;

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <param name="message">resource to be serialized</param>
        /// <returns>string with serialized object</returns>
        string SerializeObjectToString(QbcBase message);

        /// <summary>
        /// Deserialize object from message 
        /// </summary>
        /// <typeparam name="TMsg">Objecttype to deserialize to</typeparam>
        /// <param name="msg">binary message to be deserialized</param>
        /// <returns>Deserialized object</returns>
        TMsg DeserializeObject<TMsg>(Byte[] msg) where TMsg : class;

        /// <summary>
        ///  Deserialize object from binary message 
        /// </summary>
        /// <param name="msg">binary message to be deserialized</param>
        /// <param name="targetType">Final object type to instantiate from stream</param>
        /// <returns>Deserialize object</returns>
        QbcBase DeserializeObject(Byte[] msg, Type targetType);

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="TMsg">Objecttype to deserialize to</typeparam>
        /// <param name="msg">string message to de deserialized</param>
        /// <returns>Deserialized object</returns>
        TMsg DeserializeObjectFromString<TMsg>(string msg) where TMsg : class;

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <param name="msg">string message to be deserialized</param>
        /// <param name="targetType">target type to deserialize to</param>
        /// <returns>deserialized object</returns>
        QbcBase DeserializeObjectFromString(string msg, Type targetType);

        /// <summary>
        /// Formatter options : e.g default date format in string
        /// </summary>
        IQbcFormatterOptions Options
        {
            get;
            set;
        }
    }
}
