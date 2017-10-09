using UnityEngine;
using System.Collections;

// Classe di test per chiamare un delegato con 1 parametro
public class Delegates1Test : MonoBehaviour {

    // Delegate type declaration
    public delegate void TestDelegateType(int param1);

    // Delegate variable declaration
    public TestDelegateType testDelegate;


    void Start () {

        // Assign a matching method to the delegate
        testDelegate = TestImplementation2;

        // Call the method contained in the test delegate
        testDelegate(10);
	}

    // Declare a method that matches the delegate
    void TestImplementation(int param1)
    {
        Debug.Log("Test: " + param1);
    }

    void TestImplementation2(int param1)
    {
        Debug.Log("Test 2: " + param1);
    }
}
