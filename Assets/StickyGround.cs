using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGround : MonoBehaviour
{
    GameObject Player;
    PlayerController PC;
    private void Start() {
        Player = GameObject.FindWithTag("Player");
        PC = Player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        PC.isGrounded = false;
        PC.doubleJump = false;
    }
    
    private void OnCollisionStay2D(Collision2D other) {
        PC.isGrounded = false;
        PC.doubleJump = false;
    }
}
