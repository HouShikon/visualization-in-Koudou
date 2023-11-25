using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Button StartButton;
    Button ChooseButton;
    void Start()
    {
        StartButton=transform.Find("Start").GetComponent<Button>();
        StartButton.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("start");
            GameManager.Instance.OnClickStart();
        });
        ChooseButton=transform.Find("Choose").GetComponent<Button>();
        ChooseButton.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("choose");
            GameManager.Instance.GetComponent<FileManager>().SelectFile();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
