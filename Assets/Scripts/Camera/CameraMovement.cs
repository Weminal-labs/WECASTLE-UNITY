using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    [SerializeField]
    private float mapMinx, mapMaxx, mapMiny, mapMaxy;

    private Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {
        PanCam();
        if (Input.mouseScrollDelta.y < 0)
        {
            ZoomIn();
        }
        else if(Input.mouseScrollDelta.y > 0)
        {
            ZoomOut();
        }
    }

    private void PanCam()
    {
        if(Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 differ = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = camPos(cam.transform.position + differ);
        }
    }

    private void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        cam.transform.position = camPos(cam.transform.position);
    }

    private void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = camPos(cam.transform.position);
    }

    private Vector3 camPos(Vector3 target)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize*cam.aspect;

        float minX = mapMinx + camWidth;
        float maxX = mapMaxx - camWidth;
        float minY = mapMiny + camHeight;
        float maxY = mapMaxy - camHeight;

        float newX = Mathf.Clamp(target.x, minX, maxX);
        float newY = Mathf.Clamp(target.y, minY, maxY);

        return new Vector3(newX, newY, target.z);
    }
}
