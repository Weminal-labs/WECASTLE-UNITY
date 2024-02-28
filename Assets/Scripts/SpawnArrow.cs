using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject[] waypoints;
    public void spawnArrow()
    {
        arrow.GetComponent<Arrow>().SetWaypoints(waypoints);
        arrow.transform.localScale = gameObject.transform.localScale;
        Instantiate(arrow, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(0, 0, -60 * gameObject.transform.localScale.x));
    }
}
