using UnityEngine;
using System.Collections;

public class SingletonClient : MonoBehaviour {

	void Start () {

        SingletonTest mySingleton = SingletonTest.GetInstance();
        mySingleton.DoStuff();

        SingletonMonoTest myMonoSingleton = SingletonMonoTest.GetInstance();

	}
	
	void Update () {
	
	}
}
