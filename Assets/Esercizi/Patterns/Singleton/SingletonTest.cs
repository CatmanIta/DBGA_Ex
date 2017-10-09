
public class SingletonTest {

    private static SingletonTest instance = null;

    private SingletonTest()
    {
    }

    public static SingletonTest GetInstance()
    {
        if (instance == null)
        {
            instance = new SingletonTest();
            UnityEngine.Debug.Log("Instance created.");
        }
        return instance;
    }

    public void DoStuff()
    {
        UnityEngine.Debug.Log("SINGLETON TEST STUFF");
    }

}

