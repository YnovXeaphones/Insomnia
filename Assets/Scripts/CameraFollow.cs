using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0,0 ,-10);
    [SerializeField] private Vector3 downLimit;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private bool isInDream = false;
    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isActive == true)
        {
            if (isInDream == false)
            {
                isInDream = true;
                Vector3 desiredPosition = player.transform.position + offset;
                transform.position = desiredPosition;
                downLimit.y += -12;
            }
            else
            {
                isInDream = false;
                Vector3 desiredPosition = player.transform.position + offset;
                transform.position = desiredPosition;
                downLimit.y -= -12;
            }
        }
    }

    void LateUpdate()
    {
        if (player.transform.position.y > downLimit.y && Input.GetKey(KeyCode.R) == false && isActive == true)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
