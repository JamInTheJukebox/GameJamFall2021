using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float vel = 50f;
    public Collision2D col;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveProjectile(Vector2 playerPosition)
    {

        Vector2 inital_position = transform.position;
        Vector2 final_position = playerPosition;
        Vector2 targetPosition = final_position - inital_position;
        targetPosition = targetPosition.normalized;
        rb.velocity = targetPosition*vel;
        Destroy(gameObject, 10);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // spawn particle effect
        
        if (col.gameObject.CompareTag("Player"))
        {
            //do something here
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}