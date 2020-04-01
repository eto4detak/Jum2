using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject observation;

    private Camera cam;
    private float flyHeight = 120;
    private float minDistance = 10f;
    private float planetRadius = 100f;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!IsClose())
        {
            SoftFollow();
        }
        cam.transform.LookAt(observation.transform.position.normalized * planetRadius, observation.transform.position);
    }


    private bool IsClose()
    {
       return (observation.transform.position.normalized * flyHeight - cam.transform.position).magnitude < minDistance ;
    }

    private void Follow()
    {
        Vector3 pointUnderCharacter = observation.transform.position.normalized * planetRadius;
        Vector3 direc = pointUnderCharacter - observation.transform.forward.normalized * minDistance + pointUnderCharacter.normalized * minDistance;

        cam.transform.position = Vector3.Slerp(cam.transform.position, direc.normalized * flyHeight, Time.deltaTime);
    }

    private void SoftFollow()
    {
        Vector3 planet = observation.transform.position.normalized * flyHeight;
        cam.transform.position = Vector3.Slerp(cam.transform.position, planet.normalized * flyHeight, Time.deltaTime);
    }
}