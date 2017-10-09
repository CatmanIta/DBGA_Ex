using UnityEngine;
using System.Collections;

public class TestSender : MonoBehaviour {

    public GameObject targetGo;

	void Start () {

        // manda un messaggio al GO selezionato
        //targetGo.SendMessage("DoStuff", SendMessageOptions.DontRequireReceiver);

        targetGo.BroadcastMessage("DoStuff", SendMessageOptions.DontRequireReceiver);
    }
	
}
