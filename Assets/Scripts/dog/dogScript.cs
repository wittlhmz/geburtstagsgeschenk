using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class dogScript : MonoBehaviour
{

    public float speed = 5f;
    public float barkRadius = 5f;
    private Rigidbody2D rb;
    private Animator an;
    private SpriteRenderer sr;
    public AudioSource audioSource;
    public AudioClip[] barkClips;

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

            // Checken, ob Bewegung stattfindet
        bool walking = movement.sqrMagnitude > 0;
        an.SetBool("isWalking", walking);

            // Sprite drehen
        if (movement.x > 0)
        {
            sr.flipX = true; // nach rechts schauen
        }
        else if (movement.x < 0)
        {
            sr.flipX = false; // nach links schauen
        }

            // Bellen mit Leertaste
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bark();
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = movement.normalized * speed;
        rb.velocity = velocity;
    }

    void Bark()
    {
        int randomIndex = Random.Range(0, barkClips.Length);

        audioSource.Stop();
        audioSource.clip = barkClips[randomIndex];
        audioSource.Play();
        // Alle Collider in der Nähe finden
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, barkRadius);

        foreach (Collider2D hit in hits)
        {
            Sheep sheep = hit.GetComponent<Sheep>();

            if (sheep != null)
            {
                sheep.RunAway(transform.position);
            }
        }
    }
}

