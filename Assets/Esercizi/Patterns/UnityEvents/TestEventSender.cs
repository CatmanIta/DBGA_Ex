using UnityEngine;
using UnityEngine.Events;



public class TestEventSender : MonoBehaviour {

    public UnityEvent testEvent;    // Istanza dell'evento che vogliamo 'sollevare'

    void Awake()
    {
        testEvent.AddListener(TestMethod);
    }

    void Update () {
        testEvent.Invoke();
    }

    void TestMethod()
    {
        Debug.Log("Test method!");
    }

}
