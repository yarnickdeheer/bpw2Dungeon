using System;

[Serializable]
public class IntRange
{
    public int m_Min;       // min value
    public int m_Max;       // max value
    private int x;          // random value between min and max

    public IntRange(int min,int max)
    {
        m_Min = min;    //set m_min 
        m_Max = max;    //set m_max

    }
    public int Random // first random 
    {
       
        get {

            x = UnityEngine.Random.Range(m_Min, m_Max); //set x value 
        
            return x; }
        
        
    }
    public int Random2
    {

        get
        {

            x = UnityEngine.Random.Range(m_Min, m_Max);//set x value 

            return x;
        }


    }

}
