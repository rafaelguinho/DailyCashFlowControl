

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace DailyCashFlowControl.Domain.Models
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

    public class ConsolidatedItemResult
    {
        public ConsolidatedItemResult(DateTime date, string dateKey, string transactionId, decimal value, decimal totalByDate, int order)
        {
            Date = date;
            DateKey = dateKey;
            TransactionId = transactionId;
            Value = value;
            TotalByDate = totalByDate;
            Order = order;
        }

        public ConsolidatedItemResult()
        {
                
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Order { get; set; }

        [BsonSerializer(typeof(MyDateTimeSerializer))]
        public DateTime Date { get; set; }

        public string DateKey { get; set; }

        public string TransactionId { get; set; }

        public decimal Value { get; set; }

        public decimal TotalByDate { get; set; }
    }
}
