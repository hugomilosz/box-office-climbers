using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject stairUnitPrefab;
    private int maxPieces = 3; 

    private List<GameObject> activePieces = new List<GameObject>();
    private GameObject lastPiece;

    void Awake()
    {
        GameObject startPiece = Instantiate(stairUnitPrefab, transform.position, Quaternion.identity, transform);
        activePieces.Add(startPiece);
        lastPiece = startPiece;
    }

    public GameObject GetStartingPiece()
    {
        return activePieces[0];
    }

    public GameObject GenerateNextPiece(Transform startFrom)
    {
        Vector3 startPointLocalPos = stairUnitPrefab.transform.Find("StartPoint").localPosition;
        Vector3 spawnPosition = startFrom.position - startPointLocalPos;
        GameObject newPiece = Instantiate(stairUnitPrefab, spawnPosition, Quaternion.identity, transform);
        activePieces.Add(newPiece);
        lastPiece = newPiece;

        if (activePieces.Count > maxPieces)
        {
            Destroy(activePieces[0]);
            activePieces.RemoveAt(0);
        }

        return newPiece;
    }
}