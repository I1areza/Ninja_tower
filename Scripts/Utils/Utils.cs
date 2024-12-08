using System.Linq;
using Godot.Collections;
using Array = System.Array;


public static class Utils
{
    public static T[] GetObjectsOfType<T>(this Array array)
    {
        return array.OfType<T>().ToArray();
    }
}