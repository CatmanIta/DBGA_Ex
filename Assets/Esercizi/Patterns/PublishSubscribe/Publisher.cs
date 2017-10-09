using UnityEngine;

public class Publisher {

    public PubSubServer server;

    // The publisher sends a message to the server
	public void PublishMessage (string s) {
        server.SendMessage(s, "gameplay");
	}
	
}
