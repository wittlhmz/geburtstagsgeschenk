using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Slot[] slots; // Zielscheibe_1 bis Zielscheibe_10 hier reinziehen
    public musicManager musicManager;

    public void PlayAll()
    {
        //StartCoroutine(PlaySequenceCoroutine());
    }
    void OnMouseDown()
    {
        PlayAll();
    }

    /*IEnumerator PlaySequenceCoroutine()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject piece = slots[i].currentPiece;

            if (piece != null)
            {
                AudioSource audio = piece.GetComponent<AudioSource>();

                if (audio != null && audio.clip != null)
                {
                    audio.Play();

                    // Warten bis Clip fertig ist
                    yield return new WaitForSeconds(audio.clip.length);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        musicManager.CheckOrder();
    }*/
    
}
