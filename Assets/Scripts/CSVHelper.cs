using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVHelper : MonoBehaviour
{
    public float timer = 0f;
    public float frequent = 0f;
    private agent_manager manager;
    private string behavior;
    private Covid covid;
    private Risk risk;
    
    private void Awake()
    {
        manager = transform.GetComponent<agent_manager>();
        StartCoroutine(CSVRead());
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

    IEnumerator CSVRead()
    {
        var reader = new StreamReader(File.OpenRead("Assets/CSV/location.csv"));
         
        bool isFirst = true;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
            if (isFirst)
            {
                int number = int.Parse(values[9]);
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
            
            
            if (values[6] == "sus")
                covid = Covid.susceptible;
            else if (values[6] == "exp")
                covid = Covid.exposed;
            else if (values[6] == "asy")
                covid = Covid.asymptomatic;
            else if (values[6] == "rec")
                covid = Covid.recovered;
            else if (values[6] == "sym")
                covid = Covid.symptomatic;
            else if (values[6] == "sev")
                covid = Covid.severe;

            string location = values[7];
            if (values[8] == "H")
                risk = Risk.High;
            else if (values[8] == "L")
                risk = Risk.Low ;
            else if (values[8] == "M")
                risk = Risk.Medium;
            //Debug.Log(id+"  "+lat+", "+lon);
            if (manager.TimeStep != time)
            {
                yield return new WaitForSeconds(frequent);
                //transform.GetComponent<agent_manager>().agents[id].GetComponent<Agent>().Set_lat_lon(lat, lon);
                manager.TimeStep = time;
            }
            manager.UpdateAgentInformation(id, lat, lon, time, activity, behavior, covid, location, risk);




        }
        
        Debug.Log("reading finished");
        reader.Dispose();
    }
}
