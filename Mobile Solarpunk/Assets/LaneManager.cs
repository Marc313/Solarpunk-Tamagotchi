using UnityEngine;

public class LaneManager : Singleton<LaneManager>
{
    public enum Lane
    {
        Left = 0,
        Middle = 1,
        Right = 2
    }

    public float laneSize;
    public Lane currentLane = Lane.Middle;

    private void Awake()
    {
        Instance= this;
    }

    // Returns offset to currentPosition
    public Vector3 ToLeft()
    {
        if (currentLane == Lane.Left) return Vector3.zero;

        currentLane -= 1;
        return -Vector3.right * laneSize;
    }

    public Vector3 ToRight()
    {
        if (currentLane == Lane.Right) return Vector3.zero;

        currentLane += 1;
        return Vector3.right * laneSize;
    }
}
