using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;

public static class Extensions
{
    /// <summary>
    /// The squared distance between two points on the X-Z plane.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static float SqrHorizontalDistanceTo(this Vector3 origin, Vector3 destination)
    {
        Vector2 origin2D = new Vector2(origin.x, origin.z);
        Vector2 destination2D = new Vector2(destination.x, destination.z);
        return Vector2.SqrMagnitude(destination2D - origin2D);
    }

    public static float HorizontalDistanceTo(this Vector3 origin, Vector3 destination)
    {
        Vector2 origin2D = new Vector2(origin.x, origin.z);
        Vector2 destination2D = new Vector2(destination.x, destination.z);
        return Vector2.Distance(destination2D, origin2D);
    }

    /// <summary>
    /// Returns a Vector3 projected onto the X-Z plane. Y component is zeroed out.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 HorizontalVector3(this Vector3 vector)
    {
        vector.y = 0;
        return vector;
    }

    /// <summary>
    /// The X-Z component of a Vector3.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns>Vector2 containing X and Z values of input Vector3.</returns>
    public static Vector2 HorizontalVector2(this Vector3 vector)
    {
        return new(vector.x, vector.z);
    }


    /// <summary>
    /// Enable a canvas group. The group will fade in using the specified fade in time.
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="fadeInTime"></param>
    public static void EnableCanvasGroup(this CanvasGroup canvasGroup, float fadeInTime)
    {
        canvasGroup.DOFade(1, fadeInTime);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    /// <summary>
    /// Disable a canvas group. The group will fade out using the specified fade out time.
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="fadeInTime"></param>
    public static void DisableCanvasGroup(this CanvasGroup canvasGroup, float fadeInTime)
    {
        canvasGroup.DOFade(0, fadeInTime);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public static void DisableInteractions(this CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    public static void EnableInteractions(this CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
