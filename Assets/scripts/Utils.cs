using System.Linq;

public static class Utils
{
    public static int GetRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public static int GetNumberAverage(int x, int y)
    {
        var average = new int[2] { x, y }.Average();
        return System.Convert.ToInt32(average);
    }
}