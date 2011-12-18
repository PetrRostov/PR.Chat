using System.Runtime.Serialization;

namespace PR.Chat.Application.DTO
{
    [DataContract]
    public class NickDto
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}