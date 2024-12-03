using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private string playerLayerName = "Player";
    [SerializeField] private string groundLayerName = "Ground";
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2 (xVelocity, yVelocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            collision.gameObject.GetComponent<Player>().Knockback(transform.position.x);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            Destroy(gameObject);
        }
    }
}
