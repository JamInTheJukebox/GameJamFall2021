using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Projectile : StoneInteractable
{
    private Rigidbody2D rb;
    public float vel = 50f;
    public Collision2D col;

    public Gradient Orb_Gradient;
    public SpriteRenderer OrbSpriteRenderer;
    public float MaxTime;

    public ParticleSystem ProjectileDestructionEffect;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        MaxTime = SecondsToInteract;
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
        if (col.CompareTag(Tags.PLAYER))
        {
            FindObjectOfType<Boss_State_Machine>().CatchKitchi();
            //do something here
            DestroyParticle();
        }
        if (col.CompareTag(Tags.SUNSTONE))
        {

        }
        else
        {
            DestroyParticle();
        }
        
    }

    public void DestroyParticle()
    {
        Instantiate(ProjectileDestructionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tags.SUNSTONE)
        {
            InteractWithStone();
            if (MaxTime != 0)
            {
                OrbSpriteRenderer.color = Orb_Gradient.Evaluate(SecondsToInteract / MaxTime);
            }
        }
    }
}