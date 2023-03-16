using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0,0 ,-10);
    [SerializeField] private Vector3 downLimit;
    private Vector3 downLimitOG;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private bool isInDream = false;
    public bool isActive = true;
    [SerializeField] private float dimensionJump;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        dimensionJump = player.GetComponent<PlayerController>().dimensionJump;
        downLimitOG = downLimit;
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
                downLimit.y += dimensionJump;
            }
            else
            {
                isInDream = false;
                Vector3 desiredPosition = player.transform.position + offset;
                transform.position = desiredPosition;
                downLimit.y -= dimensionJump;
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
        if (Input.GetKeyDown(KeyCode.R) && isActive == true)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            transform.position = desiredPosition;
        }
    }

    public void ResetCamera()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = desiredPosition;
        downLimit.y = downLimitOG.y;
        isInDream = false;
    }
}
