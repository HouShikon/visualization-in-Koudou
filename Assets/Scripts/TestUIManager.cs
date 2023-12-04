using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TestUIManager Instance { get; private set; }
    private AgentPanel panel;
    void Start()
    {
        panel=transform.Find("AgentPanel").GetComponent<AgentPanel>();
    }
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowAgentsPanel()
    {
        Debug.Log("show "+panel.name);
        panel.ShowPanel();
    }
}
