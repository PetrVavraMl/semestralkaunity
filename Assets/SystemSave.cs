using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SystemSave
{
    public static void SavePlayer(movementScript ms)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dyck";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerSave psData = new PlayerSave(ms);
        formatter.Serialize(stream, psData);
        stream.Close();
        Debug.Log("Player SAVED! ");

    }

    public static PlayerSave LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dyck";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerSave dataSave = formatter.Deserialize(stream) as PlayerSave;
            stream.Close();
            Debug.Log("Player loaded! ");
            return dataSave;
        }
        else
        {
            Debug.Log("Savefile not found! " + path);
            return null;
        }

    }
}
