using UnityEngine;
using System.Collections.Generic;

// Generates and manages the sequence of platforms
public class LevelGenerator : MonoBehaviour
{
    public GameObject stairUnitPrefab;
    private int maxPieces = 3; 

    private List<GameObject> activePieces = new List<GameObject>();
    private GameObject lastPiece;

    // Create the very first piece of the level
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

    // Generates a new level piece connected to the current platform
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