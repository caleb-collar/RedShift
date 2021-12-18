using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy Pathing | Caleb A. Collar | 10.7.21
public class EnemyPathing : MonoBehaviour
{
    private float moveSpd = 5f;
    private WaveConfig waveConfig;
    private int waypointIndex = 0;
    private List<Transform> waypoints;
    
    void Start()
    {
        moveSpd = waveConfig.GetMoveSpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = moveSpd * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            //Debug.Log("Moving to " + targetPosition);
            if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y) waypointIndex++; //Ignore Z
        }
        else
        {
           Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
