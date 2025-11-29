using UnityEngine;

public enum SoundID
{
    ButtonClick,
    GameOver,
    ScoreUpLow,
    ScoreUpHigh
}

[System.Serializable]
public class SoundEntry
{
    public SoundID soundID;
    public AudioClip clip;
}