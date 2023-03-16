using UnityEngine;
using UnityEngine.Events;

public class Spike : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlayerDeath;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet en contact est le joueur
        {
            _onPlayerDeath.Invoke(); // Appelle l'événement
        }
    }
}
