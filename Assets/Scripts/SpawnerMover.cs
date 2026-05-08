using UnityEngine;

public class SpawnerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}