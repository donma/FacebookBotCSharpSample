using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookBotCSharpSample
{
    // for send 

    public class SendModel
    {
        public recipient recipient { get; set; }

        public message message { get; set; }
    }

    public class recipient
    {
        public string id { get; set; }
    }

    public class message
    {
        public string text { get; set; }
    }

    // for receive
    public class Sender
    {
        public string id { get; set; }
    }

    public class Recipient
    {
        public string id { get; set; }
    }

    public class Message
    {
        public string mid { get; set; }
        public int seq { get; set; }
        public string text { get; set; }
    }

    public class Messaging
    {
        public Sender sender { get; set; }
        public Recipient recipient { get; set; }
        public long timestamp { get; set; }
        public Message message { get; set; }
    }

    public class Entry
    {
        public string id { get; set; }
        public long time { get; set; }
        public List<Messaging> messaging { get; set; }
    }

    public class ReceiveBotModel
    {
        public string @object { get; set; }
        public List<Entry> entry { get; set; }
    }
}