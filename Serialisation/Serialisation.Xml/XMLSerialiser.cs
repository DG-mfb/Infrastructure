using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisation.Xml
{
    public class XMLSerialiser : IXmlSerialiser
    {
        public T Deserialise<T>(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public object[] Deserialize(Stream stream, IList<Type> messageTypes)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string data)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(string data)
        {
            throw new NotImplementedException();
        }

        public void Serialise<T>(XmlWriter xmlWriter, T value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Stream stream, object[] o)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (o == null)
                throw new ArgumentNullException("value");

            var obj = o[0];
           
            var type = obj.GetType();
            
            var xmlSerialzer = new XmlSerializer(type);
            xmlSerialzer.Serialize(stream, obj);
        }

        public string Serialize(object o)
        {
            throw new NotImplementedException();
        }
    }
}