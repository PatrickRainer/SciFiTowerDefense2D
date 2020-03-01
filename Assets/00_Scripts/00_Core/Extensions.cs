using UnityEngine;
using System.Collections;

public static class Extensions
{

}

public static class GameObjectExtension
{
    /// <summary>
    /// Returns the World Position of the gameObject
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static Vector3 GetPosition(this GameObject go)
    {
         return go.transform.position;       //BUG: What to return if the game object does not exist anymore?
    }
}
