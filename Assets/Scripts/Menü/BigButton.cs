using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigButton : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject red;
    public GameObject green;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.dogGameWon)
        {
            left.SetActive(false);
        }

        if (GameManager.instance.musicGameWon)
        {
            right.SetActive(false);
        }

        if (GameManager.instance.dogGameWon && GameManager.instance.musicGameWon)
        {
            red.SetActive(false);
            green.SetActive(true);
        }
    }
}
