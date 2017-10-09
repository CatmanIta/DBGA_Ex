using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This will use anonymous methods
public class Delegates3Test : MonoBehaviour {

    public delegate string TestDelegateType();
    public delegate void TestDelegateType1(string param1);
    public TestDelegateType testDelegate;
    public TestDelegateType1 testDelegate1;

    void Start()
    {
        //testDelegate = delegate(int param1) { Debug.Log("Test: " + param1); };
        //testDelegate = (x) => Debug.Log("Test: " + x);

        //testDelegate = delegate() { return "CIAO"; };
        //testDelegate = () => "CIAO";
        //testDelegate1 = (x) => Debug.Log("Test: " + x);

        //testDelegate1(testDelegate());


        List<string> myListaDellaSpesa = new List<string>();
        myListaDellaSpesa.Add("Pollo");
        myListaDellaSpesa.Add("Patate");
        myListaDellaSpesa.Add("Gattini (bianco)");
        myListaDellaSpesa.Add("Gattini (2)");

        myListaDellaSpesa.Exists( (s) => s.Contains("Gattini") );


        myListaDellaSpesa.Sort((s1, s2) => s1.CompareTo(s2));

        List<TestStruct> listStructs = new List<TestStruct>();
        listStructs.Sort((s1, s2) => s1.a1 - s2.a1 );
    }

    public struct TestStruct
    {
        public int a1;
        public  int a2;
    }

    /*public bool CheckGattini(List<string> list)
    {
        foreach (string s in list)
        {
            if (ContainsGattini(s))
            {
                return true;
            }
        }
        return false;
    }*/

    /*public bool ContainsGattini(string s)
    {
        return s.Contains("Gattini");
    }*/

}
