using UnityEngine;
using System.Collections;

public class TestPublishSubscribe : MonoBehaviour {

	void Start () {

        // Initialisation
        PubSubServer server = new PubSubServer();

        Subscriber subscriber1 = new Subscriber();
        subscriber1.server = server;
        subscriber1.Initialise();   // The subscriber will subscribe to the server

        Publisher publisher1 = new Publisher();
        publisher1.server = server;


        // Use
        publisher1.PublishMessage("CIAO1");
        publisher1.PublishMessage("CIAO2");



        // subscriber1.SubscribeToTopic("aaa");
    }
	
}
