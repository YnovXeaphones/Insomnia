using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class boxCollider : MonoBehaviour
{
    [SerializeField] private Collider2D Col;
    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // check if collision is below the box
            if (collision.contacts[0].normal.y > 0.5f)
            {
                Debug.Log("Player collided with box from below");
                Physics2D.IgnoreCollision(collision.collider, Col);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2.5f, ForceMode2D.Impulse);
            }
        }
    }
    IEnumerator OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player exited box");
            yield return new WaitForSeconds(0.2f);
            var PlayerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(PlayerCollider, Col, false);
        }
    }
}
