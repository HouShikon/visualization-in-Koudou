
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    string defaultPath = "";
    private bool isSelected = false;
    public Text informationText;

    public bool IsSelected { get => isSelected; set => isSelected = value; }

    void Start()
    {
        //defaultPath = UnityEngine.Application.dataPath + "/GameAssets/";
    }
   

    public void SelectFile()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.InitialDirectory = "file://"+ UnityEngine.Application.dataPath;
        ofd.Filter = "CSV file(*.csv)|*.csv";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            informationText.text = ofd.FileName;
            GameManager.Instance.UpdateCSVPath(ofd.FileName);
            IsSelected= true;
        }
    }

    void Update()
    {
        
    }
}
