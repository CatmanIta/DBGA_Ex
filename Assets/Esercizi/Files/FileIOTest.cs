using UnityEngine;
using System.Collections;
using System.IO;

public class FileIOTest : MonoBehaviour {

    void Start() {

        //WriteTextFile();
        //ReadTextFile();

        ReadCSV();
    }

    void WriteTextFile()
    {
        var path = Application.persistentDataPath + "/ciao.txt";
        StreamWriter sw = File.CreateText(path);
        sw.WriteLine("Ciao");
        sw.Write("Bello");
        sw.Close();
    }

    void ReadTextFile()
    {
        var path = Application.persistentDataPath + "/ciao.txt";
        if (File.Exists(path))
        {
            StreamReader sr = File.OpenText(path);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                Debug.Log(s);
            }
            sr.Close();
        }
    }

    void WriteBinaryFile()
    {
        var path = Application.persistentDataPath + "/binary.pew";
        FileStream fs = File.Create(path);
        byte[] bytes = new byte[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
        fs.Write(bytes, 0, 8);
        fs.Close();
    }

    void ReadBinaryFile()
    {
        var path = Application.persistentDataPath + "/binary.pew";
        FileStream fs = File.OpenRead(path);
        byte[] bytes = new byte[8];
        fs.Read(bytes, 0, 8);
        fs.Close();
    }


    void WriteBinaryFileWithText()
    {
        var path = Application.persistentDataPath + "/binarytext.txt";
        FileStream fs = File.Create(path);
        var bytes = System.Text.Encoding.ASCII.GetBytes("Buongiorno");
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();

        // This is equivalent
        //File.WriteAllText(path, "Buongiorno", System.Text.Encoding.ASCII);
    }

    void ReadBinaryFileWithText()
    {
        var path = Application.persistentDataPath + "/binarytext.txt";
        string result = File.ReadAllText(path, System.Text.Encoding.ASCII);
    }


    void WriteTransformPositionToBinaryFile()
    {
        string path = Application.persistentDataPath + "/savedposition.pos";

        Vector3 pos = transform.position;
        FileStream fs = File.Create(path);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(pos.x);
        bw.Write(pos.y);
        bw.Write(pos.z);
        bw.Close();
        fs.Close();
    }

    void ReadTransformPositionFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/savedposition.pos";
        Vector3 pos = transform.position;
        FileStream fs = File.OpenRead(path);
        BinaryReader br = new BinaryReader(fs);
        pos.x = br.ReadSingle();
        pos.y = br.ReadSingle();
        pos.z = br.ReadSingle();
        br.Close();
        fs.Close();
        transform.position = pos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            WriteTransformPositionToBinaryFile();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            ReadTransformPositionFromBinaryFile();
        }
    }


    public struct Animal
    {
        public string name;
        public int strength;
        public int life;

        public override string ToString()
        {
            return name + " s" + strength + " l" + life;
        }
    }


    void ReadCSV()
    {
        string path = Application.persistentDataPath + "/animali.csv";
        StreamReader sr = new StreamReader(path);

        System.Collections.Generic.List<Animal> animals = new System.Collections.Generic.List<Animal>();
        string s;
        bool firstLine = true;
        while((s = sr.ReadLine()) != null)
        {
            if (firstLine)
            {
                firstLine = false;
                continue;
            }

            string[] cells = s.Split(',');
            Animal animal;
            animal.name = cells[0];
            animal.strength = int.Parse(cells[1]);
            animal.life = int.Parse(cells[2]);

            animals.Add(animal);
        }
        sr.Close();

        // Log the result
        animals.ForEach(x => Debug.Log(x));
    }
	
}
