using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public Slot[] slots; // 👈 im Inspector setzen
    public GameObject[] backgrounds; // 👈 im Inspector setzen

    public void ResetAll()
    {
        // 🔹 Slots leeren
        foreach (Slot slot in slots)
        {
            slot.currentPiece = null;
        }

        // 🔹 Alle Schnipsel finden & resetten
        AudioPiece[] allPieces = FindObjectsOfType<AudioPiece>();

        foreach (AudioPiece piece in allPieces)
        {
            piece.ResetPiece();
        }

        // 🔹 Farben resetten
        foreach (GameObject bg in backgrounds)
        {
            bg.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void OnMouseDown()
    {
        ResetAll();
    }
}
