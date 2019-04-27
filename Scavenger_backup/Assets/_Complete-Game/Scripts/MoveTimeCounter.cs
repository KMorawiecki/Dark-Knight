using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MoveTimeCounter
{

    private static readonly Lazy<MoveTimeCounter> lazy = new Lazy<MoveTimeCounter>(() => new MoveTimeCounter());
    float startTime = 0f;
    public float turnTime = 7f; // Users maximum time for move 

    private MoveTimeCounter()
    {
    }

    public static MoveTimeCounter Instance
    {
        get
        {
            return lazy.Value;
        }
    }

    public void Reset()
    {
        startTime = Time.time;
    }

    public bool IsTimeOver()
    {
        return Time.time - startTime < turnTime ? false : true;
    }
}
