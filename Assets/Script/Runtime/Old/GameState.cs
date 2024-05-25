using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance;

    public static GameState Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private bool PlayerCanMove=true;


    public bool getPlayerCanMove()
    {
        return PlayerCanMove;
    }
    public void setPlayerCantMove()
    {
        PlayerCanMove=false;
    }
    public void setPlayerCanMove()
    {
        PlayerCanMove = true;
    }
}
