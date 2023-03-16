using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEvent _addPretzelScore;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] BoxCollider2D _boxCollider2D;
    [SerializeField] SpriteRenderer _spriteRenderer;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("musicVolume")) {
            _audioSource.volume = 1;
        } else if (PlayerPrefs.HasKey("musicVolume")) {
            _audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _addPretzelScore.Invoke();
            // AudioSource.PlayClipAtPoint(_audioSource.clip, GameObject.Find("Main Camera").transform.position);
            _audioSource.PlayOneShot(_audioSource.clip,1);
            _boxCollider2D.enabled = false;
            _spriteRenderer.enabled = false;
            StartCoroutine(DestroyAfterSeconds(5));
        }
    }

    private IEnumerator DestroyAfterSeconds(int second) {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }
}
