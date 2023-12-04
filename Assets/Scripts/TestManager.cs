using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance { get; private set; }
    private CSVHelper csvHelper;
    private AgentManager agentManager;
    private ReactiveProperty<string> rxID;
    private bool IsPause=false;
    
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
        csvHelper = GetComponent<CSVHelper>();
        csvHelper.CSVPath = "Assets/CSV/location.csv";
        csvHelper.StartReadCSV();
        agentManager = GetComponent<AgentManager>();
        agentManager.timeText = GameObject.Find("Time").GetComponent<Text>();
        
        
    }
    void Start()
    {
        
        Observable.EveryUpdate().Where(_=> Input.GetMouseButtonDown(0)).Subscribe(_ =>
        {
            //Debug.Log("click");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                TestUIManager.Instance.ShowAgentsPanel();
                Agent agent=hit.collider.gameObject.GetComponent<Agent>();
                //rxID= new ReactiveProperty<string>(agent.ID.ToString());
                Text id = GameObject.Find("IDText").GetComponent<Text>();
                Text location = GameObject.Find("LocationText").GetComponent<Text>();
                Text activity = GameObject.Find("ActivityText").GetComponent<Text>();
                //rxID.SubscribeToText(id);
                
                Debug.Log(agent.Location+" "+agent.ID);
            }
            

        });
        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Space)).Subscribe(_ =>
        {
            Pause();
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
