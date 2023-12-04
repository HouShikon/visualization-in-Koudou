using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = System.Random;



public class Agent : MonoBehaviour
{
    [SerializeField] private int iD;
    [SerializeField] private int time;
    [SerializeField] private string activity = "";
    
    [SerializeField] private string location = "";
    
    [SerializeField] private string  behavior="normal";
    [SerializeField] private string profession ;
    [SerializeField] private Covid covid;
    private ReactiveProperty<Covid> reactiveCovid = new ReactiveProperty<Covid>();
    [SerializeField] private Risk risk;

    
    float lat;
    float lon;

    public Sprite green;
    public Sprite red;
    public Sprite yellow;

    public Agent Init(float lat, float lon, int iD, int time, string activity, string location, string behavior, string profession, Covid covid, Risk risk)
    {
        this.lat = lon;
        this.lon = lat;
        ID = iD;
        Time = time;
        Activity = activity;
        Location = location;
        Behavior = behavior;
        Profession = profession;
        Covid = covid;
        Risk = risk;
        return this;
    }
    public Agent()
    {

    }

    public int ID { get => iD; set => iD = value; }
    public int Time { get => time; set => time = value; }
    public string Activity { get => activity; set => activity = value; }
    public string Location { get => location; set => location = value; }
    public string Behavior { get => behavior; set => behavior = value; }
    public string Profession { get => profession; set => profession = value; }
    public Covid Covid { get => covid; set => covid = value; }
    
    public Risk Risk { get => risk; set => risk = value; }

    private void Start()
    {
        reactiveCovid.Where(cov=>cov==Covid.exposed).Subscribe(cov =>
        {
            Debug.Log(this.ID+"   "+ cov);
            if(this.location=="road")
                GameManager.Instance.CreateInfectionOnRoadEffect(this.transform.position);
            else
                GameManager.Instance.CreateInfectionInDoorEffect(this.transform.position);
        });
    }
    private void Update()
    {
        

    }

    public void SetInformation(float lat, float lon, int iD, int time, string activity, string location, string behavior, string profession, Covid covid, Risk risk)
    {
        this.lat = lat;
        this.lon =lon;
        this.time = time;
        this.transform.position = new Vector2(lon, lat);
        this.Activity = activity;
        
        this.Behavior = behavior;
        this.Profession = profession;
        this.Location = location;
        this.Risk = risk;

        if(this.Covid!= covid)
        {
            if (covid == Covid.asymptomatic|| covid ==Covid.exposed)
            {
                this.GetComponent<SpriteRenderer>().sprite = yellow;
                
            }
                
            else if(covid ==Covid.severe|| covid == Covid.symptomatic)
            {
                this.GetComponent<SpriteRenderer>().sprite = red;
                
            }
                
            else if (covid == Covid.recovered || covid == Covid.susceptible)
            {
                this.GetComponent<SpriteRenderer>().sprite =green;
                
            }
            this.Covid = covid;
            this.reactiveCovid.Value = this.Covid;
            //Debug.Log(this.ID );

        }
        
       
        

    }
}
