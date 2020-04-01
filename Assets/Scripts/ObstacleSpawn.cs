using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstacles;
    public UnityEvent EventSpawn = new UnityEvent();
    private float delayTime = 2f;
    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(CreateObstacle(other.rigidbody.position));
    }

    private IEnumerator CreateObstacle(Vector3 obstPos)
    {
        EventSpawn?.Invoke();
        yield return new WaitForSeconds(delayTime);
        GameObject tempObstacle = obstacles[Random.Range(0, obstacles.Length - 1)];
        Instantiate(tempObstacle, obstPos, Random.rotation);
    }

}
