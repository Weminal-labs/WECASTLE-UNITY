/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Waypoint wayPointFollower;

    // Start is called before the first frame update
    void Start()
    {
        wayPointFollower = GetComponent<Waypoint>();

    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite(wayPointFollower.GetNextWaypoint().transform);
    }
    public void FlipSprite(Transform transform)
    {
        bool shouldFlip = transform.position.x < this.transform.position.x;

        // Flip the entire object
        this.transform.localScale = new Vector3((shouldFlip ? -1 : 1), 1, 1);
    }
}
*/