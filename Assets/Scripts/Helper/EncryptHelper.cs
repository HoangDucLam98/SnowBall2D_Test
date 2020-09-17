using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class EncryptHelper
{
    private static string key = "GreenD";

    public static string EncryptInt(int number)
    {
        string s = number.ToString();
        var v_secret_buffer = Encoding.UTF8.GetBytes(s);

        for (int i = 0; i < v_secret_buffer.Length; i++)
        {
            v_secret_buffer[i] ^= (byte)key[i % key.Length];
        }

        return Encoding.UTF8.GetString(v_secret_buffer);
    }

    public static string EncryptFloat(float number)
    {
        string s = number.ToString();
        var v_secret_buffer = Encoding.UTF8.GetBytes(s);

        for (int i = 0; i < v_secret_buffer.Length; i++)
        {
            v_secret_buffer[i] ^= (byte)key[i % key.Length];
        }

        return Encoding.UTF8.GetString(v_secret_buffer);
    }

    public static string EncryptString(string s)
    {
        var v_secret_buffer = Encoding.UTF8.GetBytes(s);

        for (int i = 0; i < v_secret_buffer.Length; i++)
        {
            v_secret_buffer[i] ^= (byte)key[i % key.Length];
        }

        return Encoding.UTF8.GetString(v_secret_buffer);
    }

    public static int DecryptInt(string s)
    {
        var v_secret_buffer = Encoding.UTF8.GetBytes(s);

        for (int i = 0; i < v_secret_buffer.Length; i++)
        {
            v_secret_buffer[i] ^= (byte)key[i % key.Length];
        }

        return int.Parse(Encoding.UTF8.GetString(v_secret_buffer));
    }

    public static float DecryptFloat(string s)
    {
        var v_secret_buffer = Encoding.UTF8.GetBytes(s);

        for (int i = 0; i < v_secret_buffer.Length; i++)
        {
            v_secret_buffer[i] ^= (byte)key[i % key.Length];
        }

        return float.Parse(Encoding.UTF8.GetString(v_secret_buffer));
    }

    public static string DecryptString(string s)
    {
        var v_secret_buffer = Encoding.UTF8.GetBytes(s);

        for (int i = 0; i < v_secret_buffer.Length; i++)
        {
            v_secret_buffer[i] ^= (byte)key[i % key.Length];
        }

        return Encoding.UTF8.GetString(v_secret_buffer);
    }

    public static void SaveInt(string path, int value)
    {
        string s = EncryptInt(value);
        SavingDataWithJson.SaveData(s, path);
        var newPath = Path.Combine(Application.persistentDataPath, path);
        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            streamWriter.Write(s);
        }
    }

    public static int LoadInt(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);

        using (StreamReader streamReader = File.OpenText(newPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return DecryptInt(jsonString);
        }
    }

    public static void SaveFloat(string path, float value)
    {
        string s = EncryptFloat(value);
        SavingDataWithJson.SaveData(s, path);
        var newPath = Path.Combine(Application.persistentDataPath, path);
        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            streamWriter.Write(s);
        }
    }

    public static float LoadFloat(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);

        using (StreamReader streamReader = File.OpenText(newPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return DecryptFloat(jsonString);
        }
    }

    public static void SaveList<T>(string path, List<T> list)
    {
        string s = JsonHelper.ToJson(list);
        SavingDataWithJson.SaveData(s, path);
        var newPath = Path.Combine(Application.persistentDataPath, path);
        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            streamWriter.Write(EncryptString(s));
        }
    }

    public static List<T> LoadList<T>(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);

        using (StreamReader streamReader = File.OpenText(newPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonHelper.FromJson<T>(DecryptString(jsonString));
        }
    }

    public static T LoadObject<T>(string path)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        using (StreamReader streamReader = File.OpenText(newPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<T>(DecryptString(jsonString));
        }
    }

    public static void SaveObject<T>(string path, T data)
    {
        var newPath = Path.Combine(Application.persistentDataPath, path);
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(newPath))
        {
            var s = EncryptHelper.EncryptString(jsonString);
            streamWriter.Write(s);
        }
    }

    public static int GetInt(string path)
    {
        var s = PlayerPrefs.GetString(path);
        return DecryptInt(s);
    }

    public static void SetInt(string path, int value)
    {
        var s = EncryptInt(value);
        PlayerPrefs.SetString(path, s);
    }

    public static float GetFloat(string path)
    {
        var s = PlayerPrefs.GetString(path);
        return DecryptFloat(s);
    }

    public static void SetFloat(string path, float value)
    {
        var s = EncryptFloat(value);
        PlayerPrefs.SetString(path, s);
    }

    public static string GetString(string path)
    {
        var s = PlayerPrefs.GetString(path);
        return DecryptString(s);
    }

    public static void SetString(string path, string value)
    {
        var s = EncryptString(value);
        PlayerPrefs.SetString(path, s);
    }

}
