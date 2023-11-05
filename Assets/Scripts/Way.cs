using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class Way 
{
    public long id;
    public List<long> nodes;
    public Way(long id)
    {
        this.id = id;
        nodes = new List<long>();
    }
}
