using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static PlayerInfo save = new PlayerInfo();

    public static void CreateNewSave()
    {
        save = PlayerInfo.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        bf.Serialize(file, SaveLoad.save);
        file.Close();
    }

    public static void Save()
    {
        save = PlayerInfo.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        bf.Serialize(file, SaveLoad.save);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            SaveLoad.save = (PlayerInfo)bf.Deserialize(file);
            PlayerInfo.current = new PlayerInfo();
            PlayerInfo.current = save;
            file.Close();
        }
    }
}
