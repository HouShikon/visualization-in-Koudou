using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using System.Xml;

public class MapManager : MonoBehaviour
{
    public Material defult_material;

    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
    [SerializeField]
    private float boundsX=36;
    [SerializeField]
    private float boundsY=140;
    private List<Node> nodes = new List<Node>();
    private List<Way> ways = new List<Way>();
    private XmlDocument doc = new XmlDocument();
    private List<Transform> wayObjects = new List<Transform>();
    [SerializeField] private string mapName = "Tx-To-TU.osm";


    // Start is called before the first frame update
    private void Awake()
    {
        doc.Load(new XmlTextReader("Assets/osm_file/" + mapName));
        XmlNodeList elemList = doc.GetElementsByTagName("node");
        for (int i = 0; i < elemList.Count; i++)
        {
            nodes.Add(new Node(long.Parse(elemList[i].Attributes["id"].InnerText),
                float.Parse(elemList[i].Attributes["lat"].InnerText),
                float.Parse(elemList[i].Attributes["lon"].InnerText)
            ));
        }
        XmlNodeList wayList = doc.GetElementsByTagName("way");
        int ct = 0;
        foreach (XmlNode wayNode in wayList)
        {
            XmlNodeList wayNodes = wayNode.ChildNodes;
            ways.Add(new Way(long.Parse(wayNode.Attributes["id"].InnerText)));
            foreach (XmlNode node in wayNodes)
            {
                if (node.Attributes[0].Name == "ref")
                {
                    ways[ct].nodes.Add(long.Parse(node.Attributes["ref"].InnerText));

                }
            }
            ct++;
        }
        for (int i = 0; i < ways.Count; i++)
        {
            wayObjects.Add(new GameObject("wayObject" + ways[i].id).transform);
            LineRenderer line=　wayObjects[i].gameObject.AddComponent<LineRenderer>();
            Material material = new Material(defult_material);
            line.material = material;
            line.startWidth = 0.04f;
            line.endWidth = 0.04f;
            line.startColor = Color.gray;
            line.startColor = Color.grey;
            line.positionCount= ways[i].nodes.Count;
            for (int j = 0; j < ways[i].nodes.Count; j++)
            {
                foreach (Node nod in nodes)
                {
                    if (nod.id == ways[i].nodes[j])
                    {
                        x = nod.lat;
                        y = nod.lon;

                    }
                }
                wayObjects[i].GetComponent<LineRenderer>().SetPosition(j, new Vector3((x - boundsX) * 800, (y - boundsY) * 800));
                
            }
        }
    }
    void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
