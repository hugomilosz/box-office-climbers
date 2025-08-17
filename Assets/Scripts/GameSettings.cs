using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    public enum GameMode { Worldwide, Domestic };
    public GameMode selectedGameMode;
}