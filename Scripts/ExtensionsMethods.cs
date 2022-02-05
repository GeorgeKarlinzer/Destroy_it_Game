using UnityEngine;

public static class ExtensionsMethods
{
    public static string ToShortString(this int value)
    {
        int dim = (int)Mathf.Log10(value);

        switch(dim)
        {
            case 3:
                return $"{value / 1000}.{(value % 1000) / 100}k";
            case 4:
                return $"{value / 1000}k";
            case 5:
                return $"{value / 1000}k";
            case 6:
                return $"{value / 1000000}.{value % 1000000 / 100000}m";
            case 7:
                return $"{value / 1000000}M";
            case 8:
                return $"{value / 1000000}M";
            default:
                return $"{value}";
        }
    }
}
