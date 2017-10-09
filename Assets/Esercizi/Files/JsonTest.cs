using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.IO;

namespace TestFile
{
    public struct SaveData
    {
        public string saveName;
        public int level;
        public string playerName;
        public List<int> equipment;
    }

    public class JsonTest : MonoBehaviour
    {

        void Start()
        {
            // Deserialization tests
            string myJsonString = "[\"gatto\", [ 1, 2, 3 ] ]";
            List<object> parsedRoot = (List<object>)Json.Deserialize(myJsonString);
            string animal = (string)parsedRoot[0];
            List<object> innerList = (List<object>)parsedRoot[1];
            long animal_strength = (long)innerList[0];
            long animal_intelligence = (long)innerList[1];
            long animal_dexterity = (long)innerList[2];


            string myJsonString2 = "{ \"gatto\" : [ 1, 2, 3 ] ,  \"cane\" : [ 2, 4, 5 ] }";
            Dictionary<string,object> parsedRoot2 = (Dictionary<string,object>)Json.Deserialize(myJsonString2);
            foreach(KeyValuePair<string,object> pair in parsedRoot2)
            {
                string animal2 = pair.Key;
                List<object> innerList2 = (List<object>)pair.Value;

                long animal2_strength = (long)innerList2[0];
                long animal2_intelligence = (long)innerList2[1];
                long animal2_dexterity = (long)innerList2[2];

                Debug.Log(animal2_dexterity);
                // Debug.Log(pair);
            }

            // Serialization tests
            List<object> saveDataList = new List<object>();
            for(int i = 0; i < 4; i++)
            {
                SaveData saveData;
                saveData.saveName = "Save " + i;
                saveData.level = Random.Range(1,10);
                saveData.playerName = "gattino";
                saveData.equipment = new List<int>() { 4, 6, 1, 9, 8 };

                // Create a serializable representation
                Dictionary<string, object> saveDataDict = new Dictionary<string, object>();
                saveDataDict["saveName"] = saveData.saveName;
                saveDataDict["level"] = saveData.level;
                saveDataDict["playerName"] = saveData.playerName;
                List<object> equipmentJson = new List<object>();
                saveDataDict["equipment"] = equipmentJson;
                foreach (var equipId in saveData.equipment)
                {
                    equipmentJson.Add(equipId);
                }

                saveDataList.Add(saveDataDict);
            }

            string allSaveDataJson = Json.Serialize(saveDataList);
            File.WriteAllText(Application.persistentDataPath + "/allsaves.json", allSaveDataJson);

            // Create a directory with all save data
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
            foreach(object saveDataObject in saveDataList)
            {
                Dictionary<string, object> saveDataDict = (Dictionary<string, object>)saveDataObject;
                string saveDataJson = Json.Serialize(saveDataDict);
                File.WriteAllText(Application.persistentDataPath + "/SaveData" + "/" + (string)saveDataDict["saveName"],  saveDataJson);
            }

            // Check what save data files we have
            string[] saveFileNames = Directory.GetFiles(Application.persistentDataPath + "/SaveData");
            foreach(string fileName in saveFileNames)
            {
                Debug.Log("Found save: " + fileName);
            }
        }

    }

}