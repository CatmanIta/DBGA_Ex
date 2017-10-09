using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[System.Serializable]
public class TestStringUnityEvent : UnityEvent<string> { }


namespace TestUnityEvents {


    public class Player : MonoBehaviour {

        public TestStringUnityEvent testStringEvent = new TestStringUnityEvent();

        public UnityEvent doingStuffEvent;
        public UnityEvent playerDeathEvent;


        void Update() {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DoStuff();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Die();
            }
        }


        void DoStuff()
        {
            // doing stuff
            //doingStuffEvent.Invoke();
            testStringEvent.Invoke("PEW!");
        }

        void Die()
        {
            // dying...
            playerDeathEvent.Invoke();
        }

    }

}
