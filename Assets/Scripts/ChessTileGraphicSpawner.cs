using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ChessTileGraphicSpawner : MonoBehaviour
{


    public GameObject prefab;
    public Color tile1color = Color.white;
    public Color tile2color = Color.black;
    public float size = 1f;
    private string row = "ABCDEFGH";

    // Start is called before the first frame update
    void Start()
    {
        spawnChessBoard();
    }

    private void spawnChessBoard()
    {
        string name;

        for(int i = 0; i < 8 ; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                bool isLightSquare = (i + j) % 2 != 0;

                var squareColor = isLightSquare ? tile1color : tile2color;
                var position = new Vector2(j+size/2,i+size/2);
                name = row[i] + (j+1).ToString();

                spawnSquare(position, squareColor, name);


            }
        }
    }

    private void spawnSquare(Vector2 pos, Color color, string name)
    {
        GameObject tileObject = Instantiate(prefab, pos, Quaternion.identity);
        tileObject.GetComponent<SpriteRenderer>().color = color;
        tileObject.transform.SetParent(gameObject.transform, false);
        tileObject.name = name;
    }
}
