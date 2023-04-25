using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace SubscribeBot
{
    
    public class Chats
    {
        public List<string> ChatIds;
        public Chats()
        {

        }

        public List<string> Read()
        {
            string chats = "";
            try
            {
                chats = File.ReadAllText("chats");
                ChatIds ch = JsonSerializer.Deserialize<ChatIds>(chats);
                return ch.Ids;
            }
            catch { }

            return new List<string>();
            
        }
        public void Save(List<string> chatIds)
        {
            ChatIds st = new ChatIds();
            st.Ids = chatIds;
            string chats = JsonSerializer.Serialize(st, options: new JsonSerializerOptions() { IgnoreNullValues = true});
            File.WriteAllText("chats", chats);
        }
    }
    [Serializable]
    public class ChatIds
    {
        public ChatIds()
        {

        }
        public List<string> Ids { get; set; }
    }
}
