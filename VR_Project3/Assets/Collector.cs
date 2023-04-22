using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collector : MonoBehaviour
{
    public int score = 0; // The player's score
    private List<GameObject> collectedSpheres = new List<GameObject>(); // List of collected spheres
    public TextMeshPro scoreText; 

    // This function is called when a collider enters the trigger collider of the cube object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere")) // Check if the collider belongs to a sphere object
        {
            if (!collectedSpheres.Contains(other.gameObject)) // Check if the sphere has not been collected before
            {
                // Add 1 to the player's score
                score += 1;

                // Add the sphere to the list of collected spheres
                collectedSpheres.Add(other.gameObject);

                // Disable the rigidbody component of the sphere so it stays in place inside the cube
                Rigidbody rb = other.GetComponent<Rigidbody>();

                Destroy(other);

                scoreText.text = "Score: " + score.ToString();
            }
        }
    }
}


