using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVHelper : MonoBehaviour
{
    public float timer = 0f;
    public float frequent = 0f;
    [SerializeField] private string cSVPath;
    private AgentManager manager;
    private string behavior;
    private Covid covid;
    private Risk risk;
    private Dictionary<string,Covid> covidDict=new Dictionary<string, Covid>();
    private Dictionary<string, Risk> riskDict=new Dictionary<string, Risk>();
    public string CSVPath { get => cSVPath; set => cSVPath = value; }

    private void Awake()
    {
        //CSVPath = "";
        covidDict.Add("asy",Covid.asymptomatic );
        covidDict.Add("sus",Covid.susceptible );
        covidDict.Add("exp",Covid.exposed);
        covidDict.Add("sym",Covid.symptomatic );
        covidDict.Add("rec", Covid.recovered);
        covidDict.Add("sev", Covid.severe);
        riskDict.Add("L", Risk.Low);
        riskDict.Add("M",Risk.Medium);
        riskDict.Add ("H", Risk.High);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.frequent += 0.05f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            this.frequent -= 0.05f;
    }
    
    public void StartReadCSV()
    {
        manager = transform.GetComponent<AgentManager>();
        StartCoroutine(CSVRead());
    }

    IEnumerator CSVRead()
    {
        var reader = new StreamReader(File.OpenRead(CSVPath));
         
        bool isFirst = true;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
            if (isFirst)
            {
                int number = int.Parse(values[10]);
                manager.CreateAgents(number);
                isFirst = false;
                continue;
            }
            int time = int.Parse(values[0]);
            int id = int.Parse(values[1]);
            float lat = (float.Parse(values[2]) - 36) * 800;
            float lon = (float.Parse(values[3]) - 140) * 800;
            string activity = values[4];

            if (values[5] == "n")
                behavior = "normal";
            else if (values[5] == "e")
                behavior = "evacuate";


            covid = covidDict[values[6]];

            string location = values[7];
            risk = riskDict[values[9]];
            string profession= values[8];
            if (manager.TimeStep != time)
            {
                yield return new WaitForSeconds(frequent);
                //transform.GetComponent<agent_manager>().agents[id].GetComponent<Agent>().Set_lat_lon(lat, lon);
                manager.TimeStep = time;
            }
            manager.UpdateAgentInformation(  lat, lon, id, time, activity,  location, behavior, profession , covid,risk);




        }
        
        Debug.Log("reading finished");
        reader.Dispose();
    }
}
