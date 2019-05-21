using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo 
{
    private static readonly PlayerInfo instance = new PlayerInfo();
    public static string PlayerName { get;  private set; }
    public static bool CurrentlyPlaying { get; private set; }
    public static float GameTime { get; private set; }
    public static int Level { get; private set; }

    private static float  gameStartTime;

    static PlayerInfo()
    {
        CurrentlyPlaying = false;
        PlayerName = "none";
        GameTime = 0;
    }
    public static PlayerInfo Instance
    {
        get
        {
            return instance;
        }
    }

    public void StartGameTime(string name)
    {
        PlayerName = name;
        gameStartTime = Time.time;
        CurrentlyPlaying = true;
    }

    public float GameCurrentTime()
    {
        return Time.time - gameStartTime; 
    }

    public void EndOfGame(int level)
    {
        GameTime = GameCurrentTime();
        CurrentlyPlaying = false;
        Level = level;
    }
}
