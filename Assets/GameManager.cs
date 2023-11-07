using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Value { get; set; } = 0;
    

    private MapManager mapManager;
    private CSVHelper csvHelper;
    private AgentManager agentManager;
    private FileManager fileManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        mapManager = GetComponent<MapManager>();
        csvHelper = GetComponent<CSVHelper>();
        agentManager = GetComponent<AgentManager>();
        fileManager = GetComponent<FileManager>();  
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void UpdateCSVPath(string path)
    {
        csvHelper.CSVPath = path;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            StartReplay();
        }
        
    }
    public void StartReplay()
    {
        mapManager.InitializeMap();
        csvHelper.StartReadCSV();
        agentManager.timeText = GameObject.Find("Time").GetComponent<Text>();
    }
    public void OnClickStart()
    {
        if(fileManager.IsSelected)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            fileManager.informationText.text = "Please select CSV file";
        }
        
    }
}
