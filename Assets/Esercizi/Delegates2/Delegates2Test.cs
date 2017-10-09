using UnityEngine;
using System.Collections;

// This will be the Observer
public class Delegates2Test : MonoBehaviour {

    void Start () {

        Subject sub = new Subject();

        // Subscribe to the event of the subject
        sub.testEvent += TestImplementation;


        sub.Trigger();
	}

    void TestImplementation(int param1)
    {
        Debug.Log("Test: " + param1);
    }
}

public class Subject
{
    // Declare an event type and an event
    //public delegate void EventType(int param1);
    public System.Action<int> testEvent;

    public void Trigger()
    {
        testEvent(5);
    }

}
