using UnityEngine;
using System;

[Serializable]
public class RandomRange
{
    public Double Min;
    public Double Max;
    public bool IntOnly;

    public RandomRange(Double min = 0, Double max = 1, bool intOnly = true)
    {
        Min = min;
        Max = max;
        IntOnly = intOnly;
    }

    // returns a random float in range
    public float Get()
    {
        return IntOnly ? GetInt() : UnityEngine.Random.Range((float)Min, (float)Max);
    }

    // Returns a random int in range, recommended for use even when min and max should be integers
    public int GetInt()
    {
        return UnityEngine.Random.Range((int)Mathf.Ceil((float)Min), (int)Mathf.Floor((float)Max) + 1);
    }
}
