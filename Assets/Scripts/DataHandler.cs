using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;
    public string bestPlayer;
    public int bestScore;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestPlayer();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveBestPlayer()
    {
        Debug.Log("Going SAVEBESTPLAYER");
        
           SaveData data2 = new SaveData();
     //      if (bestPlayer == "")
           {
            data2.playerName = bestPlayer;
            data2.playerScore = bestScore;
          }
    //    else

        Debug.Log("Best Player - " + data2.playerName + " " + data2.playerScore);
        string json = JsonUtility.ToJson(data2);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("SaveBestPlayer completed");
    }

    public void LoadBestPlayer()
   {
          string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.playerName;
            bestScore = data.playerScore;
        }
        else
        {
            bestPlayer = "";
            bestScore = 0;
        }
        Debug.Log("Loaded " + bestPlayer + " " + bestScore);

  }
}
