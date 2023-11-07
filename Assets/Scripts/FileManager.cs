
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
    //private void SelectFolder()
    //{
    //    DirectoryInfo mydir = new DirectoryInfo(defaultPath);
    //    if (!mydir.Exists)
    //    {
    //        MessageBox.Show("请先创建资源文件夹");
    //        return;
    //    }

    //    try
    //    {
    //        FolderBrowserDialog fbd = new FolderBrowserDialog();
    //        fbd.Description = "选择要打包的资源文件夹";
    //        fbd.ShowNewFolderButton = false;
    //        fbd.RootFolder = Environment.SpecialFolder.MyComputer;//设置默认打开路径
    //        fbd.SelectedPath = defaultPath;  //默认打开路径下的详细路径

    //        if (fbd.ShowDialog() == DialogResult.OK)
    //        {
    //            defaultPath = fbd.SelectedPath;
    //            //selectDir.text = fbd.SelectedPath;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("打开错误：" + e.Message);
    //        return;
    //    }
    //}

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
