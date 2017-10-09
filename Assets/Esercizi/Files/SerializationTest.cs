using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Collections.Generic;

namespace TestFile
{
    [System.Serializable]
    public class InnerData
    {
        public int inner_v;
    }

    [System.Serializable]
    public class TestSaveData
    {
        public int v;
        public float f;
        public string s;

        [System.NonSerialized]
        private int p_v;

        public int[] intarray;

        public InnerData innerData;

        //public Component go;

        public TestSaveData otherSaveData;

        //public List<int> intlist;
        //public List<List<List<int>>> intlistlistlist;
        
        //public Dictionary<string, int> dict;

        public TestSaveData(int _v)
        {
            v = _v;
            f = 0;
            s = "";
            p_v = 5;
            intarray = new int[] { 1, 2, 3, 4 };
            innerData = new InnerData();
            innerData.inner_v = 5;
            //go = new GameObject("Test").AddComponent<SpriteRenderer>();
            //otherSaveData = new TestSaveData(2);
            /*intlist = new List<int>() { 1, 2, 3, 4 };
            intlistlistlist = new List<List<List<int>>>()
            {
                new List<List<int>>()
                {
                    new List<int>()
                    {
                        1,2,3,4,5
                    }
                }
            };*/
            //dict = new Dictionary<string, int>();
            //dict["ciao"] = 10;
            //dict["gatto"] = 17;
        }

        public override string ToString()
        {
            return "v: " + v +
                "\n f: " + f +
                "\n s: " + s +
                "\n p_v: " + p_v +
                "\n array: " + intarray[0] + "," + intarray[1]
                //+ "\n list:" + intlist[0] + "," + intlist[1]
                // + "\n intlistlistlist: " + intlistlistlist[0][0][0] + "," + intlistlistlist[0][0][1]
                //+ "\n disct: " + dict["ciao"] + "," + dict["gatto"]
                + "\n innerdata v: " + innerData.inner_v
                //+ "\n go: " + go.name;
                ;
        }
    }

    public class SerializationTest : MonoBehaviour
    {
        public TestSaveData inspectorSaveData;

        public static void Save()
        {
            object graph = null;

            // Define what we will save
            TestSaveData sd = new TestSaveData(10);
            sd.f = 5.23f;
            sd.s = "ciao";
            graph = sd;

            // Save the graph object to a file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/testsavedata.sb");
            formatter.Serialize(fs, graph);
            fs.Close();

            Debug.Log("SAVED");
        }

        public static void Load()
        {
            // Load the graph object from a file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/testsavedata.sb");
            object graph = formatter.Deserialize(fs);
            fs.Close();

            //Debug.Log(graph.ToString());

            // Retrieve the data we saved
            TestSaveData sd = (TestSaveData)graph;

            Debug.Log(sd.ToString());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S)) Save();
            if (Input.GetKeyDown(KeyCode.L)) Load();
        }

    }

}