using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;

    [SerializeField] private float speed = 10f;
    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypoints.Length)
            {
                Destroy(gameObject);
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * 5f);
    }

    public GameObject GetNextWaypoint() { return waypoints[currentWayPointIndex]; }
    public void SetWaypoints(GameObject[] waypoints)
    {
        this.waypoints = waypoints;
    }
}
