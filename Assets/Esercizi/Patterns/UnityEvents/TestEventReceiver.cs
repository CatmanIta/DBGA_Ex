using UnityEngine;
using System.Collections;

public class TestEventReceiver : MonoBehaviour {

    public TestEventSender sender;

    void Awake()
    {
        sender.testEvent.AddListener(this.HandleTestEvent);
    }

	public void HandleTestEvent () {
        Debug.Log("TEST EVENT RECEIVED");
	}

}
