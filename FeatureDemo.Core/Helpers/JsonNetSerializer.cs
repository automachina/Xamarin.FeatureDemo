using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using XLabs.Serialization;

namespace FeatureDemo.Core.Helpers
{
    public class JsonNetSerializer : JsonSerializer, IJsonSerializer
    {
        public SerializationFormat Format => SerializationFormat.Json;

        public T Deserialize<T>(byte[] data)
        {
            return this.DeserializeFromBytes<T>(data);
        }

        public object Deserialize(byte[] data, Type type)
        {
            return this.DeserializeFromBytes(data, type);
        }

        public T Deserialize<T>(Stream stream)
        {
            return this.DeserializeFromStream<T>(stream);
        }

        public object Deserialize(Stream stream, Type type)
        {
			return this.DeserializeFromStream(stream,type);
        }

        public T Deserialize<T>(string data)
        {
            return this.DeserializeFromString<T>(data);
        }

        public object Deserialize(string data, Type type)
        {
            return this.DeserializeFromString(data, type); 
        }

        public void Flush()
        {
        }

        public void Serialize<T>(T obj, Stream stream)
        {
            this.SerializeToStream(obj, stream);
        }

        public string Serialize<T>(T obj)
        {
            return this.SerializeToString(obj);
        }

        public byte[] SerializeToBytes<T>(T obj)
        {
            return this.GetSerializedBytes(obj); 
        }
    }
}
