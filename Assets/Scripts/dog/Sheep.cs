using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed = 7f;
    public float runTime = 3f;

    private Rigidbody2D rb;
    private Animator an;
    private SpriteRenderer sr;

    private Vector2 runDirection;
    private float runTimer = 0f;
    Vector2 lastPosition;
    private bool isInGoal = false;
    public AudioSource audioSource;
    public AudioClip[] sheepClips;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void RunAway(Vector2 dogPosition)
    {
        if(isInGoal == false)
        {
            int randomIndex = Random.Range(0, sheepClips.Length);

            audioSource.Stop();
            audioSource.clip = sheepClips[randomIndex];
            audioSource.Play();
            // Richtung vom Hund weg berechnen
            runDirection = (transform.position - (Vector3)dogPosition).normalized;

            runTimer = runTime;   
        }
        
    }
    void FixedUpdate()
    {
        if (runTimer > 0)
        {
            rb.velocity = runDirection * runSpeed;
            runTimer -= Time.fixedDeltaTime;

        // echte Bewegung berechnen
        float distanceMoved = Vector2.Distance(rb.position, lastPosition);

        if (distanceMoved < 0.001f)
            {
                ChangeDirection();
            }
        lastPosition = rb.position;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ChangeDirection()
    {
        runDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;

        // Animation
        bool walking = velocity.sqrMagnitude > 0.1f;
        an.SetBool("isWalking", walking);

        // Sprite drehen
        if (velocity.x > 0.1f)
        {
            sr.flipX = false;
        }
        else if (velocity.x < -0.1f)
        {
            sr.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            EnterGoal();
        }
    }

    void EnterGoal()
    {
        // Schaf stoppen
        rb.velocity = Vector2.zero;
        isInGoal = true;

        // Zufällige Bewegung setzen
        float up = Random.Range(4f, 7f);
        float side = Random.Range(0f, 2f);

        // zufällig links oder rechts
        if (Random.value < 0.5f)
            side *= -1;

        Vector2 randomDir = new Vector2(side, up).normalized;
        runSpeed = 5f;
        rb.velocity = randomDir * runSpeed;

        bool finish = true;
        an.SetBool("isFinish", finish );

        // GameManager informieren
        dogManager.instance.SheepArrived();
    }
}
