using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Transform player;
    public List<Transform> roads;

    public float roadLength;

    void Start()
    {
        Renderer r = roads[0].GetComponentInChildren<Renderer>();
        if (r != null)
        {
            roadLength = r.bounds.size.z;
        }
        else
        {
            Debug.LogError("Renderer bulunamadı!");
        }
    }

    void Update()
    {
        if (roads.Count == 0) return;

        Transform firstRoad = roads[0];
        Transform lastRoad = roads[roads.Count - 1];

        if (player.position.z > firstRoad.position.z + roadLength)
        {
            firstRoad.position = new Vector3(
                firstRoad.position.x,
                firstRoad.position.y,
                lastRoad.position.z + roadLength
            );

            roads.RemoveAt(0);
            roads.Add(firstRoad);
        }
    }
}