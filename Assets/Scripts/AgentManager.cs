using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private int number = 700;
    public Text timeText;
    private int day;
    private int hour;
    private int minute;
    [SerializeField] private int timeStep=-1;
    public GameObject agentPrefab;
    public  List<GameObject> agents = new List<GameObject>();
    

    public int TimeStep { get => timeStep; set => timeStep = value; }

    private void Awake()
    {
        
        
    }
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        day = TimeStep / (24 * 3600);
        hour = (TimeStep / 3600)%24;
        minute= (TimeStep / 60)%60;
        if(timeText!=null)
            timeText.text="Day"+day+" "+hour+":"+minute;  
    }
    public void CreateAgents(int number)
    {

        this.number = number;
        for (int i = 0; i < number; i++)
        {
            GameObject agent = Instantiate(agentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            
            agent.name = "Agent" + i;
            agent.GetComponent<Agent>().ID = i;
            agent.GetComponent<Agent>().IDProperty.Value = i;
            agents.Add(agent);
        }
    }
    public void UpdateAgentInformation(int id,float lat,float lon,int time, string activity, string behavior, Covid health, string location, Risk risk)
    {
        agents[id].GetComponent<Agent>().SetInformation(lat, lon,time,activity,behavior,health,location,risk);
    }
    public void CheckAgents()
    {
        
    }
}
