using UnityEngine;
using System.Collections;

public class SingletonMonoTest : MonoBehaviour {

    private static SingletonMonoTest instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SingletonMonoTest GetInstance()
    {
        return instance;
    }
	
	void Update () {
        Debug.Log("UPDATE");
	}

}
