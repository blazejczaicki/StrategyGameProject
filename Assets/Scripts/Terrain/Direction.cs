using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Directions : MonoBehaviour
{




    //DirectionClass getDirectionNorS(float x)
    //{
    //    return (x < 0) ? new North() : new South();  
    //}
}






public abstract class DirectionClass : MonoBehaviour
{
    protected int x=0, y=0;
}

public class North : DirectionClass
{
    public North()
    {
        y = 1;
    }
}

public class South : DirectionClass
{
    public South()
    {
        y = -1;
    }
}

public class East : DirectionClass
{
    public East()
    {
        x = 1;
    }
}

public class West : DirectionClass
{
    public West()
    {
        x =-1;
    }
}
