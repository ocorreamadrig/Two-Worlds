using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class DataBase
{
    //public List<string> BD_Aiders = new List<string>();
    public List<float> BD_AiderA = new List<float>();
    public List<float> BD_AiderB = new List<float>();
    public List<float> BD_AiderC = new List<float>();
}

public class DataManager : MonoBehaviour {

    public List<float> AiderA = new List<float>();
    public List<float> AiderB = new List<float>();
    public List<float> AiderC = new List<float>();

    // Use this for initialization
    void Start () {
        if (File.Exists(Application.persistentDataPath + "/Profile.dat")) Load();
        //else
        //{
        //    Save();
        //}
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Profile.dat");
        DataBase data = new DataBase();

        AiderA = GameManager.manager.HelperSuccess_A;
        AiderB = GameManager.manager.HelperSuccess_B;
        AiderC = GameManager.manager.HelperSuccess_C;

        data.BD_AiderA = AiderA;
        data.BD_AiderB = AiderB;
        data.BD_AiderC = AiderC;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Data Saved!!");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Profile.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Profile.dat", FileMode.Open);
            DataBase data = (DataBase)bf.Deserialize(file);
            file.Close();

            AiderA = data.BD_AiderA;
            AiderB = data.BD_AiderB;
            AiderC = data.BD_AiderC;

            GameManager.manager.HelperSuccess_A = data.BD_AiderA;
            GameManager.manager.HelperSuccess_B = data.BD_AiderB;
            GameManager.manager.HelperSuccess_C = data.BD_AiderC;

            GameManager.manager.currentSuccess();
            GameManager.manager.UpdateAssets();

            Debug.Log("Data Loaded!!");
        }
    }
}
