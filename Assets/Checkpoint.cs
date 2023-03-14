using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private UnityEvent OnCheckpointReached;
    public bool isReached = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isReached) {
            isReached = true;
            OnCheckpointReached.Invoke();
        }
    }
}
