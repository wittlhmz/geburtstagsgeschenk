using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> pieces; // deine 50 Schnipsel
    public float startX = -10.5f;
    public float startY = 0.5f;

    public float xSpacing = 2.2f;
    public float ySpacing = 1.5f;

    public int columns = 10;
    public int rows = 5;

    void Start()
    {
        SpawnPieces();
    }

    void SpawnPieces()
    {
        List<Vector2> positions = new List<Vector2>();

            // 🔹 Grid erstellen
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                float posX = startX + x * xSpacing;
                float posY = startY - y * ySpacing;

                positions.Add(new Vector2(posX, posY));
            }
        }

            // 🔀 Positionen mischen
        Shuffle(positions);

            // 🧩 Prefabs spawnen
        for (int i = 0; i < pieces.Count; i++)
        {
            GameObject piece = Instantiate(pieces[i], positions[i], Quaternion.identity);
            // 🔥 Spawnposition im Script speichern
            piece.GetComponent<AudioPiece>().SetSpawnPosition(positions[i]);
        }
    }

    void Shuffle(List<Vector2> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);

            Vector2 temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
