using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet en contact est le joueur
        {
            // Tuer le joueur
            Destroy(other.gameObject); // Détruire l'objet (dans ce cas, le joueur)
        }
    }
}
