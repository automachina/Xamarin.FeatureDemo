using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using XLabs.Serialization;

namespace FeatureDemo.Core.Helpers
{
    public class JsonSerializerX : JsonSerializer, IJsonSerializer
    {
        public SerializationFormat Format => throw new NotImplementedException();

        public new T Deserialize<T>(byte[] data)
        {
            using(var stream = new MemoryStream(data))
            {
                using(var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return base.Deserialize<T>(reader);
				}
            }

        }

        public new object Deserialize(byte[] data, Type type)
        {
            return Deserialize(data, type);
        }

        public new T Deserialize<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public new object Deserialize(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public new T Deserialize<T>(string data)
        {
            throw new NotImplementedException();
        }

        public new object Deserialize(string data, Type type)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public new void Serialize<T>(T obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public new string Serialize<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeToBytes<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
