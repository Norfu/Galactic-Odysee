using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHole : MonoBehaviour
{
    public Vector3 targetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the target position
            other.transform.position = new Vector3(targetPosition.x, targetPosition.y, other.transform.position.z);
        }
    }
}
