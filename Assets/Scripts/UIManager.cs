using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public  Button StartButton;
    public Button ChooseButton;
    public AgentPanel panel;
    public static UIManager Instance { get; private set; }

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
    void Start()
    {
        StartButton=transform.Find("Start").GetComponent<Button>();
       
        StartButton.OnClickAsObservable().Subscribe(_=>
        {
            Debug.Log("start");

          
            GameManager.Instance.OnClickStart();


            //GameManager.Instance.OnClick();
        });
        ChooseButton=transform.Find("Choose").GetComponent<Button>();
        ChooseButton.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("choose");
            GameManager.Instance.GetComponent<FileManager>().SelectFile();
        });
    }

    public void ShowAgentsPanel()
    {
        Debug.Log("show " + panel.name);
        panel.ShowPanel();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
