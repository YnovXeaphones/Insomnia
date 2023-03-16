using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEvent _addPretzelScore;
    [SerializeField] AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("musicVolume")) {
            _audioSource.volume = 1;
        } else {
            _audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _addPretzelScore.Invoke();
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            Destroy(gameObject);
        }
    }
}
