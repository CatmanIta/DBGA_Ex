using UnityEngine;

public class Subscriber  {

    public PubSubServer server;

    public void Initialise()
    {
        server.SubscribeToTopic("gameplay", this);
    }
	
    // Method called when a message is received
    public void ReceiveMessage(string s)
    {
        // do stuff with the message
        Debug.Log("Subscriber received message: " + s);
    }

}
