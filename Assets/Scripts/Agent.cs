using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;



public class Agent : MonoBehaviour
{
    [SerializeField] private int iD;
    [SerializeField] private int time;
    [SerializeField] private string activity = "";
    [SerializeField] private string location = "";
    [SerializeField] private string  behavior;
    [SerializeField] private Covid covid;
    [SerializeField] private Risk risk;

    
    float lat;
    float lon;

    public Sprite green;
    public Sprite red;
    public Sprite yellow;

    public int ID { get => iD; set => iD = value; }
    public int Time { get => time; set => time = value; }
    public string Activity { get => activity; set => activity = value; }
    public string Location { get => location; set => location = value; }
    public string Behavior { get => behavior; set => behavior = value; }

    private void Start()
    {
        
    }
    private void Update()
    {
        

    }
    public void SetInformation(float lat, float lon,int time,string activity,string  behavior,Covid covid,string location,Risk risk)
    {
        this.lat = lat;
        this.lon = lon;
        this.time = time;
        this.transform.position = new Vector2(lat, lon);
        this.Activity = activity;
        this.Behavior = behavior;
        
        this.Location = location;
        this.risk = risk;
        if(this.covid!=covid)
        {
            if (covid == Covid.asymptomatic||covid==Covid.exposed)
            {
                this.GetComponent<SpriteRenderer>().sprite = yellow;
                
            }
                
            else if(covid==Covid.severe||covid== Covid.symptomatic)
            {
                this.GetComponent<SpriteRenderer>().sprite = red;
                
            }
                
            else if (covid == Covid.recovered || covid == Covid.susceptible)
            {
                this.GetComponent<SpriteRenderer>().sprite =green;
                
            }
                
            
        }
        this.covid = covid;


    }
}
