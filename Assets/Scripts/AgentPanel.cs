using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AgentPanel : MonoBehaviour
{
    private Button Close;
    public bool isIn=false;
    private RectTransform panelTransform;
    private Tweener panelTweener;
    void Start()
    {
        Close = transform.Find("Close").GetComponent<Button>();
        panelTransform = GetComponent<RectTransform>();
        
        Close.OnClickAsObservable().Subscribe(_ =>
        {
            ClosePanel();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void ShowPanel()
    {
        if(!isIn)
        {
            Debug.Log("show");
            isIn = true;
            panelTweener = panelTransform.DOLocalMove(new Vector3(0, 0, 0), 0.2f);
            panelTweener.SetAutoKill(false);
            panelTweener.SetUpdate(true);
        }
        
    }
    public void ClosePanel()
    {
        if(isIn) 
        {
            Debug.Log("close");
            isIn = false;
            panelTransform.DOPlayBackwards();
        }
    }    
}
