using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/jordi.radev";

    public static void SaveData(UITests myBehaviour)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        DataManagerTest saveData = new DataManagerTest(myBehaviour);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static DataManagerTest LoadData()
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            DataManagerTest loadData = formatter.Deserialize(stream) as DataManagerTest;

            stream.Close();
            return loadData;
        }
        else
        {
            Debug.Log("ERROR LOADING DATA");
            return null;
        }
    }
}
