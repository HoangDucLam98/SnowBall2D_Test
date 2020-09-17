using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SavingDataWithJson
{

    /// <summary>
    ///     <para> Save Data Type T With Json File </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"> Data Type T </param>
    /// <param name="path"></param>
    public static void SaveData<T>(T data, string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath,path);
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            streamWriter.Write(jsonString);
        }
    }

    public static void SaveDataWithEncrypt<T>(T data, string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            var s = EncryptHelper.EncryptString(jsonString);
            streamWriter.Write(s);
        }
    }

    public static void SaveAsset<T>(T data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    /// <summary>
    ///     <para> Load Data From Json File </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns> Object T Type </returns>
    public static T LoadData<T>(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        using (StreamReader streamReader = File.OpenText(newPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<T>(jsonString);
        }
    }

    public static T LoadDataWithEncrypt<T>(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        using (StreamReader streamReader = File.OpenText(newPath))
        {
            string jsonString = streamReader.ReadToEnd();
            var s = EncryptHelper.DecryptString(jsonString);
            return JsonUtility.FromJson<T>(s);
        }
    }

    /// <summary>
    ///     <para> Load List Data With Json File </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns> List Data Type T </returns>
    public static List<T> LoadListData<T>(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        
            using (StreamReader streamReader = File.OpenText(newPath))
            {
                string jsonString = streamReader.ReadToEnd();
                return JsonHelper.FromJson<T>(jsonString);
            }
       
    }

    public static List<T> LoadListData<T>(string path, List<T> defaultValue)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(newPath))
        {
            using (StreamReader streamReader = File.OpenText(newPath))
            {

                string jsonString = streamReader.ReadToEnd();
                return JsonHelper.FromJson<T>(jsonString);
            }
        }
        else
        {
            SaveListData(defaultValue, path);
            return new List<T>();
        }
    }

    /// <summary>
    ///     <para> Save List Data With Json File </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="path"></param>
    public static void SaveListData<T>(List<T> data, string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        string jsonString = JsonHelper.ToJson(data);
        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            streamWriter.Write(jsonString);
        }
    }

    /// <summary>
    ///     <para> Save list data after encrypt </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="path"></param>
    public static void SaveListDataWithEncrypt<T>(List<T> data, string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        string jsonString = JsonHelper.ToJson(data);
        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            var encryptData = EncryptHelper.EncryptString(jsonString);
            streamWriter.Write(encryptData);
        }
    }

    public static List<T> LoadListDataWithDecrypt<T>(string path, List<T> defaultValue)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(newPath))
        {
            using (StreamReader streamReader = File.OpenText(newPath))
            {

                string jsonString = streamReader.ReadToEnd();
                var decode = EncryptHelper.DecryptString(jsonString);
                return JsonHelper.FromJson<T>(decode);
            }
        }
        else
        {
            SaveListData(defaultValue, path);
            return new List<T>();
        }
    }

}
