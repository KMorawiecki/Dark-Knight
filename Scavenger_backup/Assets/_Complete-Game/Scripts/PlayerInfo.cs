using Completed;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo 
{
    private static readonly PlayerInfo instance = new PlayerInfo();
    public static string PlayerName { get;  private set; }
    public static int GameTime { get; private set; }
    public static int Level { get; private set; }
    private static float  gameStartTime;
    private static bool gameStarted=false;
    private static bool gameFinished=false;

    static PlayerInfo()
    {
        PlayerName = "none";
        GameTime = 0;
        gameStarted = false;

    }
    public static PlayerInfo Instance
    {
        get
        {
            return instance;
        }
    }

    public void StartGameCounter(string name)
    {
        Reset();
        PlayerName = name;
        gameStartTime = Time.time;
        gameStarted = true;
    }

    public float GameCurrentTime()
    {
        return Time.time - gameStartTime; 
    }

    public void EndOfGame(int level)
    {
        GameTime = (int)Math.Floor(GameCurrentTime());
        Level = level;
        gameFinished = true;
       
    }
    public void Reset()
    {
        PlayerName = "none";
        GameTime = 0;
        gameStarted = false;
        gameFinished = false;
    }
    public bool GameSuccesfullyFinished()
    {
        if (gameStarted && gameFinished)
            return true;
        else return false;
    }
}
