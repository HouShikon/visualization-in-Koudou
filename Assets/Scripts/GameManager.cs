using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Value { get; set; } = 0;
    public GameObject InfectionInDoorPrefab;
    public GameObject InfectionOutDoorPrefab;

    private MapManager mapManager;
    private CSVHelper csvHelper;
    private AgentManager agentManager;
    private FileManager fileManager;
    private bool IsPause = false;
    private Action CompleteAction;

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
    private void Start()
    {
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ =>
        {

            OnClickLeft();

        });
        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Space)).Subscribe(_ =>
        {
            Pause();
        });
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
        UIManager.Instance.panel = GameObject.Find("AgentPanel").GetComponent<AgentPanel>();
        agentManager.timeText = GameObject.Find("Time").GetComponent<Text>();
    }


    IEnumerator LoadSceneIE()
    {
        UnityEngine.AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

        yield return asyncOperation;

        CompleteAction?.Invoke();
        
    }
    //OnClickStart()
    //{

    //    if (fileManager.IsSelected)
    //    {
    //        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);


    //        await Task.Run(() =>
    //        {
    //            while (!asyncLoad.isDone)
    //            {
    //                Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
    //            }
    //        });
    //        return true;

    //    }
    //    else
    //    {
    //        fileManager.informationText.text = "Please select CSV file";
    //        return false;
    //    }


    //}
    public void OnClickStart()
    {
        if (fileManager.IsSelected)
        {
            CompleteAction += () =>
            {
                fileManager.informationText.gameObject.SetActive(false);
                UIManager.Instance.ChooseButton.gameObject.SetActive(false);
                UIManager.Instance.StartButton.gameObject.SetActive(false);
            };
            Coroutine c = StartCoroutine(LoadSceneIE());
            //SceneManager.LoadScene(1);
            
            
        }
        else
        {
            fileManager.informationText.text = "Please select CSV file";
        }

    }
    private void OnClickLeft()
    {
        //Debug.Log("click");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            UIManager.Instance.ShowAgentsPanel();
            Agent agent = hit.collider.gameObject.GetComponent<Agent>();
            //rxID= new ReactiveProperty<string>(agent.ID.ToString());
            Text id = GameObject.Find("IDText").GetComponent<Text>();
            Text location = GameObject.Find("LocationText").GetComponent<Text>();
            Text activity = GameObject.Find("ActivityText").GetComponent<Text>();
            Text profession = GameObject.Find("ProfessionText").GetComponent<Text>();
            //rxID.SubscribeToText(id);
            id.text=agent.ID.ToString();
            location.text = agent.Location;
            activity.text = agent.Activity;
            profession.text = agent.Profession;
            Debug.Log(agent.Profession + " " + agent.ID);
        }
    }
    private void Pause()
    {
        IsPause = !IsPause;
        if (IsPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void CreateInfectionOnRoadEffect(Vector2 position)
    {
        GameObject infection = Instantiate(InfectionOutDoorPrefab, position, Quaternion.identity);
    }
    public void CreateInfectionInDoorEffect(Vector2 position)
    {
        GameObject infection = Instantiate(InfectionInDoorPrefab, position, Quaternion.identity);
    }
}
