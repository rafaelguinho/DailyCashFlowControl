using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace DailyCashFlowControl.ConsolidatedResults.Infra
{
    public class MyDateTimeSerializer : IBsonDocumentSerializer
    {
        private readonly DateTimeSerializer serializer;

        public MyDateTimeSerializer()
        {
            serializer = new DateTimeSerializer(DateTimeKind.Utc);
        }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return serializer.Deserialize(context, args);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            serializer.Serialize(context, args, ((DateTime)value));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value, BsonSerializationInfo options)
        {
            serializer.Serialize(context, args, ((DateTime)value));
        }

        public bool TryGetMemberSerializationInfo(string memberName, out BsonSerializationInfo serializationInfo)
        {
            // Provide custom serialization options for the "MyCustomDate" member
            var serializer = new DateTimeSerializer(DateTimeKind.Utc);
            serializationInfo = new BsonSerializationInfo(memberName, serializer, typeof(DateTime));
            return true;
        }

        public Type ValueType => typeof(DateTime);
    }

}
