using UnityEngine;

public struct RandomRange
{
    public int Max { get; private set; }
    public int Min { get; private set; }
    
    public RandomRange(int max, int min)
    {
        Max = max;
        Min = min;
    }

    public RandomRange(int max)
    {
        Max = max;
        Min = 0;
    }

    public int GetRandom()
    {
        return 0;
    }

    public int GetRandomExclusive()
    {
        return 0;
    }
}
