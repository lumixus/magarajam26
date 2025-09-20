using UnityEngine;

[CreateAssetMenu(fileName = "New Day", menuName = "Days/New Day")]
public class SO_Day : ScriptableObject
{
    public int DayIndex = 0;
    public int EventCount = 1;

    public int MinimumCallCount = 5;
    public int MaximumCallCount = 10;

    public bool GetRandomCount = false;

    public int GetRandomCallCount()
    {
        int randomCount = Random.Range(MinimumCallCount, MaximumCallCount + 1);

        return randomCount;
    }


}
