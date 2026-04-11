using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class AudioPiece : MonoBehaviour
{
    public AudioSource audioSource;
    public int songID;     // welches Lied
    public int orderIndex; // Position im Refrain (0–9)
    private bool isDragging = false;
    private Vector3 offset;
    private Transform currentSlot = null;
    private bool isSnapping = false;
    private Vector3 targetPosition;
    private Vector3 spawnPosition;
    public float snapSpeed = 2f;
    private Transform previousSlot;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Stop();
        audioSource.Play();
    }

    void OnMouseDown()
    {
        PlaySound();

        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        //startPosition = transform.position;

        if (previousSlot != null)
        {
            previousSlot.GetComponent<Slot>().currentPiece = null;
            previousSlot = null;
        }
        currentSlot = null;

        GetComponent<SpriteRenderer>().sortingOrder = 10;
    }

    void OnMouseUp()
    {
        isDragging = false;
        /*if (currentSlot != null)
        {
            targetPosition = currentSlot.position;
            isSnapping = true;
        }*/
        if (currentSlot != null)
        {
            Slot slotScript = currentSlot.GetComponent<Slot>();

            if (slotScript.currentPiece == null)
            {
                // Slot frei → rein
                slotScript.currentPiece = gameObject;
                

                targetPosition = currentSlot.position;
                isSnapping = true;
                previousSlot = currentSlot;
            }
            else
            {
                // Slot belegt → zurück
                targetPosition = spawnPosition;
                isSnapping = true;
            }
        }
        else
        {
            // Kein Slot → zurück
            targetPosition = spawnPosition;
            isSnapping = true;
        }
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position =  GetMouseWorldPos() + offset;
        }

        if (isSnapping)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                snapSpeed * Time.deltaTime
            );

            // Wenn fast angekommen → stoppen
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isSnapping = false;
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger erkannt mit: " + collision.name);
        if (collision.CompareTag("Slot"))
        {
            currentSlot = collision.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Slot"))
        {
            currentSlot = null;
        }
    }
    public void SetSpawnPosition(Vector3 pos)
    {
        spawnPosition = pos;
    }
    public void ResetPiece()
    {
        // Bewegung stoppen
        isDragging = false;
        isSnapping = false;

        // Slot freigeben
        if (previousSlot != null)
        {
            previousSlot.GetComponent<Slot>().currentPiece = null;
            previousSlot = null;
        }

        currentSlot = null;

        // Position zurücksetzen
        transform.position = spawnPosition;

        // Optional: Velocity resetten
        /*Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }*/
    }
}
