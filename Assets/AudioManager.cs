using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
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
}
