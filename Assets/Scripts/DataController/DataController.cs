using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Main;
    [SerializeField]
    private TextAsset[] charFileResources;
    public UserDataContainer UserDataContainer;
    public CharFileData charFileData;

    public void Awake()
    {
        Main = this;
        charFileData = new CharFileData();
        //UserDataContainer = LoadData<UserDataContainer>("userData");

        CheckCSV();
        charFileData.originalText = OpenCSV(PathForUserDataFile("originalText", ".csv"));
        charFileData.keyText = openCSVrows(PathForUserDataFile("riddleText", ".csv"));
    }

    private void OnDisable()
    {
        //SaveConfigData(UserDataContainer, "userData");
    }

    private void OnApplicationQuit()
    {
        OnDisable();
    }

    public T LoadData<T>(string fileName)
    {
        return LoadDataFromPath<T>(PathForUserDataFile(fileName));
    }

    public T LoadDataFromPath<T>(string filePath)
    {
        T obj = default;

        try
        {
            string text = File.ReadAllText(filePath);
            obj = JsonUtility.FromJson<T>(text);
            if (obj == null) obj = default;
        }
        catch (Exception)
        {
            Debug.LogError("0x010  --  Cannot load show data for " + filePath + "!");
        }

        return obj;
    }

    public void SaveConfigData<T>(T saveData, string fileName)
    {
        SaveDataFromPath(saveData, PathForUserDataFile(fileName));
    }

    public void SaveDataFromPath<T>(T saveData, string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }
            string text = JsonUtility.ToJson(saveData, true);
            if (File.ReadAllText(filePath) != text)
            {
                File.WriteAllText(filePath, text);
            }
        }
        catch (Exception)
        {
            Debug.LogError("0x011  --  Cannot save show data for " + filePath);
        }
    }

    private void CheckCSV()
    {
        if (!File.Exists(PathForUserDataFile("ToriginalText", ".csv")))
        {
            File.WriteAllText(PathForUserDataFile("originalText", ".csv"), charFileResources[0].text);
            File.WriteAllText(PathForUserDataFile("riddleText", ".csv"), charFileResources[1].text);
        }
    }

    private Dictionary<string, string> openCSVrows(string path)
    {
        string rawText = File.ReadAllText(path, System.Text.Encoding.UTF8);
        string[] lines = rawText.Split('\n');
        Dictionary<string, string> text = new Dictionary<string, string>();
        foreach (string line in lines)
        {
            if (line.Contains(";"))
            {
                string[] keyValuePair = line.Split(';');

                if (keyValuePair[0] != "" && keyValuePair[1] != "")
                    text.Add(keyValuePair[0].Replace("$", "\n"), keyValuePair[1].Replace("$", "\n"));
            }
        }

        return text;
    }

    private List<string> OpenCSV(string path)
    {
        string rawText = File.ReadAllText(path);
        string[] lines = rawText.Split('\n');
        List<string> text = new List<string>();
        foreach (string line in lines)
        {
            string[] keyValuePair = line.Split(';');
            if (keyValuePair[0] != "")
                text.Add(keyValuePair[0].Replace("$", Environment.NewLine));
        }

        return text;
    }


    public static string FileNameClean(string name)
    {
        return name.Replace("\n", "#").Replace(" ", "_");
    }

    public static string PathForUserDataFile(string filename, string type = ".json")
    {
        string path;
        if (Application.platform == RuntimePlatform.Android) path = Application.persistentDataPath;
        else path = Application.dataPath;
        return path.Substring(0, path.LastIndexOf('/')) + "/" + filename + type;
    }
}