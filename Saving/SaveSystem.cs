using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void ClearAll()
    {
        string path = Application.persistentDataPath + "/player.save";
        File.Delete(path);
        path = Application.persistentDataPath + "/backpack.save";
        File.Delete(path);
        path = Application.persistentDataPath + "/base.save";
        File.Delete(path);
    }

    public static void SaveAll()
    {
        SaveSystem.SavePlayer();
        SaveSystem.SaveBackpack();
        SaveSystem.SaveBase();
    }

    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static PlayerData CurrentLoadedPlayerData = null;
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            if (CurrentLoadedPlayerData == null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                CurrentLoadedPlayerData = data;

                return data;
            }
            else
            {
                return CurrentLoadedPlayerData;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveBackpack()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/backpack.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        BackpackData data = new BackpackData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static BackpackData CurrentLoadedBackpackData = null;
    public static BackpackData LoadBackpack()
    {
        string path = Application.persistentDataPath + "/backpack.save";
        if (File.Exists(path))
        {
            if (CurrentLoadedBackpackData == null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                BackpackData data = formatter.Deserialize(stream) as BackpackData;

                CurrentLoadedBackpackData = data;

                return data;
            }
            else
            {
                return CurrentLoadedBackpackData;
            }
        }
        else
        {
            Debug.LogError("Backpack save file not found in " + path);
            return null;
        }
    }

    public static void SaveBase()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/base.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        BaseData data = new BaseData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static BaseData CurrentLoadedBaseData = null;
    public static BaseData LoadBase()
    {
        string path = Application.persistentDataPath + "/base.save";
        if (File.Exists(path))
        {
            if (CurrentLoadedBaseData == null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                BaseData data = formatter.Deserialize(stream) as BaseData;

                CurrentLoadedBaseData = data;

                return data;
            }
            else
            {
                return CurrentLoadedBaseData;
            }
        }
        else
        {
            Debug.LogError("Base save file not found in " + path);
            return null;
        }
    }

    public static float[] GetPositionFloatArray(Vector3 pos)
    {
        return new float[3] { pos.x, pos.y, pos.z };
    }

    public static Vector3 GetPositionVectorFromFloat(float[] arr)
    {
        return new Vector3(arr[0], arr[1], arr[2]);
    }
}
