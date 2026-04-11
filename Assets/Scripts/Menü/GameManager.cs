using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool dogGameWon = false;
    public bool musicGameWon = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 🔥 bleibt über Szenen hinweg
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
