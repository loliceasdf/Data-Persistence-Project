using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuScript : MonoBehaviour
{
    public static MenuScript Instance;
    public string playerName;
    public string hiScorePlayerName;
    public int score;
    public int hiScore;
    public Text playerNameInput;
    [System.Serializable]
    public class SaveData
    {
        public string playerName;
        public int score;
    }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
       
    }
   public void deleteData()
    {
        string path = Application.persistentDataPath + "SaveData.json";
       
        
            SaveData data = new SaveData();
            data.playerName = "";
            data.score = 0;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        Debug.Log("deleted data");
        }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            deleteData();
        }
    }
    public void StartGame()
    {

        LoadGame();
         playerName = playerNameInput.text;
        

        Debug.Log(playerNameInput.text+"is the name");


        SceneManager.LoadScene(1);
        
    }
    public void SaveGame()
    {
        string path = Application.persistentDataPath + "SaveData.json";
        if (score > hiScore)
        {
            SaveData data = new SaveData();
            data.playerName = playerName;
            data.score = score;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }
        
   
        
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "SaveData.json";
        if (File.Exists(path))
        {
           string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            hiScore = data.score;
            hiScorePlayerName = data.playerName;
            Debug.Log(hiScore + hiScorePlayerName);
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
