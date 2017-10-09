using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace TestFile
{

    public class XMLTest : MonoBehaviour
    {

        void Start()
        {
            string xmlString = "<root>  <tag version=\"1.0\"> ciao </tag>   <tag> gatto <a> gattino1 </a> <b> gattino2 </b>   </tag>   </root>";

            // Read the XML as a text, piece by piece
            XmlReader xmlReader = XmlReader.Create(new StringReader(xmlString));
            while (xmlReader.Read())
            {
                string s1 = "";
                s1 += (xmlReader.Name) + "\n";
                s1 += (xmlReader.NodeType) + "\n";
                s1 += (xmlReader.Depth) + "\n";
                s1 += ("Has attr: " + xmlReader.HasAttributes) + "\n";
                s1 += ("Has value: " + xmlReader.HasValue) + "\n";
                if (xmlReader.HasAttributes) s1 += (xmlReader.GetAttribute(0)) + "\n";
                s1 += (xmlReader.Value) + "\n";
                //Debug.Log(s1);
            }

            // Read the XML as a tree structure
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            if (xmlDocument.HasChildNodes)
            {
                RecursiveReadNode(xmlDocument.FirstChild);
            }


            // Write an XML file
            XmlDocument xmlDocument2 = new XmlDocument();

            XmlNode rootNode = xmlDocument2.CreateNode(XmlNodeType.Element, "saveslist", "");
            xmlDocument2.AppendChild(rootNode);

            for (int i = 0; i < 4; i++)
            {
                SaveData saveData;
                saveData.saveName = "Save " + i;
                saveData.level = Random.Range(1, 10);
                saveData.playerName = "gattino";
                saveData.equipment = new List<int>() { 4, 6, 1, 9, 8 };

                XmlNode saveDataNode = xmlDocument2.CreateNode(XmlNodeType.Element, "savedata", "");
                XmlAttribute nameAttribute = xmlDocument2.CreateAttribute("name");
                nameAttribute.Value = saveData.saveName;
                saveDataNode.Attributes.Append(nameAttribute);
                rootNode.AppendChild(saveDataNode);

                XmlNode levelNode = xmlDocument2.CreateNode(XmlNodeType.Element, "level", "");
                XmlNode textNode = xmlDocument2.CreateNode(XmlNodeType.Text, "text", "");
                textNode.Value = saveData.level.ToString();
                levelNode.AppendChild(textNode);
                saveDataNode.AppendChild(levelNode);

                XmlNode playerNode = xmlDocument2.CreateNode(XmlNodeType.Element, "player", "");
                textNode = xmlDocument2.CreateNode(XmlNodeType.Text, "text", "");
                textNode.Value = saveData.playerName.ToString();
                playerNode.AppendChild(textNode);
                saveDataNode.AppendChild(playerNode);

                XmlNode equipmentNode = xmlDocument2.CreateNode(XmlNodeType.Element, "equipment", "");
                saveDataNode.AppendChild(equipmentNode);
                foreach(int id in saveData.equipment)
                {
                    XmlNode equipNode = xmlDocument2.CreateNode(XmlNodeType.Element, "equip", "");
                    textNode = xmlDocument2.CreateNode(XmlNodeType.Text, "text", "");
                    textNode.Value = id.ToString();
                    equipNode.AppendChild(textNode);
                    equipmentNode.AppendChild(equipNode);
                }

            }

            Debug.Log(xmlDocument2.InnerXml);
            File.WriteAllText(Application.persistentDataPath + "/allsaves.xml", xmlDocument2.InnerXml);

        }

        void RecursiveReadNode(XmlNode node)
        {
            XmlAttributeCollection attributes = node.Attributes;
            if (attributes != null)
            {
                foreach (XmlAttribute a in attributes)
                {
                    Debug.Log(a.Name + ": " + a.Value);
                }
            }


            if (node.HasChildNodes)
            {
                XmlNodeList children = node.ChildNodes;
                foreach(XmlNode child in children)
                {
                    RecursiveReadNode(child);
                }
            }
            else
            {
                Debug.Log(node.Value);
            }
        }


    }

}