using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameValues : ScriptableObject{

    public float startingSlopeLimit;
    public float startingMoveSpeed;
    public float startingSprintSpeed;
    public float slopeLimitIncrease;
    public float moveSpeedIncrease;
    public float sprintSpeedIncrease;
    public float startingJumpHeight;
    public float startingGravity;

    [Tooltip("Goal offset in y direction")]
    public int offsetGoalY;

    [Tooltip("Goal offset in z direction")]
    public int offsetGoalZ;

    [Tooltip("The y value the goal will be spawned in")]
    public int yGoal;

    [Tooltip("Decides how much longer the level will be")]
    public int levelIncrease;

    [Tooltip("Tracks how many stages have been cleared")]
    public int stageLevel;

    [Tooltip("How wide the stage will be in x direction")]
    public int xSize;

    [Tooltip("How long the stage will be in z direction")]
    public int zSize;

    [Tooltip("Scales the nosie\nlower=less peaks\nhigher=more peaks")]
    public float noiseScalar;

    [Tooltip("Generates increasingly larger peaks\nhigher=higher peaks faster")]
    public float yScalar;

    [Tooltip("lower value = more enemies\nhigher value = less enemies")]
    public int enemySpawnRate;

    [Tooltip("lower = more power ups")]
    public int powerUpMin;

    [Tooltip("higher = less power ups")]
    public int powerUpMax;

    [Tooltip("The lowest point powerups spawn from the ground")]
    public int powerUpYmin;

    [Tooltip("The highest point powerups spawn from the ground")]
    public int powerUpYmax;

}
