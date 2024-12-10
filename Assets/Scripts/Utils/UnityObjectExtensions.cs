using UnityEngine;

public static class UnityObjectExtensions
{
    public static void SafeInvoke<T>(this T unityObject, System.Action<T> action) where T : Object
    {
        if (unityObject != null)
        {
            action(unityObject);
        }
    }
}
