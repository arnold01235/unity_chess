using UnityEngine;

public class ChessGraphicSpawner : MonoBehaviour {
    public GameObject prefab;
    public Color tile1color = Color.white;
    public Color tile2color = Color.black;
    public float size = 1f;
    private string row = "ABCDEFGH";

    private GameObject[,] spawnedTiles = new GameObject[8, 8];

    void Start()
    {
        SpawnChessBoard();
    }

    private void OnValidate()
    {
        if (spawnedTiles[0, 0] != null)
        {
            UpdateTileColors();
        }
    }

    private void SpawnChessBoard()
    {
        string name;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var position = new Vector2(j + size / 2, i + size / 2);
                name = row[i] + (j + 1).ToString();

                spawnedTiles[i, j] = SpawnObject(position, name, gameObject, -5);
            }
        }

        UpdateTileColors();
    }

    private GameObject SpawnObject(Vector2 pos, string name, GameObject parent, int sortingOrder)
    {
        GameObject tileObject = Instantiate(prefab, pos, Quaternion.identity);
        tileObject.transform.SetParent(parent.transform, false);
        tileObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        tileObject.name = name;
        return tileObject;
    }

    private void UpdateTileColors()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                bool isLightSquare = (i + j) % 2 != 0;
                var squareColor = isLightSquare ? tile1color : tile2color;
                spawnedTiles[i, j].GetComponent<SpriteRenderer>().color = squareColor;
            }
        }
    }
}
