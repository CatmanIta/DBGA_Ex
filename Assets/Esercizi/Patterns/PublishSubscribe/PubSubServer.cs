using UnityEngine;
using System.Collections.Generic;

public class PubSubServer {

    Dictionary<string, List<Subscriber>> subscribersDict = new Dictionary<string, List<Subscriber>>();

    // Subscribers will call this to subscribe to specific topics
    public void SubscribeToTopic(string topic, Subscriber sub)
    {
        if (!subscribersDict.ContainsKey(topic))
        {
            subscribersDict[topic] = new List<Subscriber>();
        }

        subscribersDict[topic].Add(sub);
    }

    // Publishers will call this to send messages about a topic
    public void SendMessage(string s, string topic)
    {
        if (!subscribersDict.ContainsKey(topic))
        {
            return;
        }

        foreach (Subscriber sub in subscribersDict[topic])
        {
            sub.ReceiveMessage(s);
        }
    }

}
