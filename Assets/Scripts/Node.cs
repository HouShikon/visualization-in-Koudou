using System.Collections;
using System.Collections.Generic;


public class Node 
{
    public long id;
    public float lat, lon;

    public Node(long id, float lat, float lon)
    {
        this.id = id;
        this.lat = lat;
        this.lon = lon;
    }
}
