using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    [Header("Grid Settings")]
    public GameObject tilePrefab;
    public int gridWidth = 5;
    public int gridHeight = 3;

    [Header("References")]
    public Transform playerTransform;

    private List<Transform> tiles = new List<Transform>();
    private Vector2 tileSize;

    void Start()
    {
        Transform platform = tilePrefab.transform.Find("Platform");
        if (platform == null)
        {
            return;
        }
        SpriteRenderer sr = platform.GetComponent<SpriteRenderer>();
        tileSize = sr.bounds.size;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                float xPos = (x - (gridWidth - 1) / 2.0f) * tileSize.x;
                float yPos = (y - (gridHeight - 1) / 2.0f) * tileSize.y;
                
                Vector3 position = new Vector3(xPos, yPos, 0);
                GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tiles.Add(newTile.transform);
            }
        }
    }

    void LateUpdate()
    {
        Vector3 playerPos = playerTransform.position;
        foreach (Transform tile in tiles)
        {
            Vector3 offset = tile.position - playerPos;

            if (Mathf.Abs(offset.x) > (gridWidth * tileSize.x) / 2.0f)
            {
                float direction = Mathf.Sign(offset.x);
                tile.position -= new Vector3(gridWidth * tileSize.x * direction, 0, 0);
            }

            if (Mathf.Abs(offset.y) > (gridHeight * tileSize.y) / 2.0f)
            {
                float direction = Mathf.Sign(offset.y);
                tile.position -= new Vector3(0, gridHeight * tileSize.y * direction, 0);
            }
        }
    }
}