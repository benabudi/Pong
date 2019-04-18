using System;
using UnityEngine;


public static class ScreenUtil
{
    public enum SnapPosition
    {
        Top,
        Bottom,
        Left,
        Right
    }

    
    public static Rect ScreenPhysicalBounds { get; private set; }

    static ScreenUtil()
    {
        
        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }

        
        var topLeftBound = mainCamera.ViewportToWorldPoint(Vector3.zero);
        var bottomRightBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var delta = bottomRightBound - topLeftBound;
        ScreenPhysicalBounds = new Rect(topLeftBound.x, topLeftBound.y, delta.x, delta.y);
    }

    
    /// <param name="toSnap">The collider to snap</param>
    /// <param name="positionToSnap">Which side of the screen to snap the collider to</param>
    /// <param name="relativeSnapOffset">The relative offset to snap the collider (Calculated by the collider's size)</param>
    public static void SnapCollider(Collider toSnap, SnapPosition positionToSnap, Vector2 relativeSnapOffset)
    {
        
        var actualOffset = new Vector3(toSnap.bounds.size.x * relativeSnapOffset.x, toSnap.bounds.size.y * relativeSnapOffset.y);
        Vector3 newPosition;

        
        switch (positionToSnap)
        {
            case SnapPosition.Top:
            {
                newPosition = new Vector3(0, ScreenPhysicalBounds.yMin + actualOffset.y);
                break;
            }
            case SnapPosition.Bottom:
            {
                newPosition = new Vector3(0, ScreenPhysicalBounds.yMax - actualOffset.y);
                break;
            }
            case SnapPosition.Left:
            {
                newPosition = new Vector3(ScreenPhysicalBounds.xMin + actualOffset.x, 0);
                break;
            }
            case SnapPosition.Right:
            {
                newPosition = new Vector3(ScreenPhysicalBounds.xMax - actualOffset.x, 0);
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException("positionToSnap");
            }
        }

        
        toSnap.transform.position = newPosition;
    }
}