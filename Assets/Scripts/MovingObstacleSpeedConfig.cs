using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SpeedConfig", menuName = "Create Moving Obstacle Speed Config")]
public class MovingObstacleSpeedConfig : ScriptableObject
{
    public float[] speeds; 
}
