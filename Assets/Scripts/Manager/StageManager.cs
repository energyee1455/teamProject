using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;


//?????????
public class StageManager : MonoBehaviour
{
    //???????????[i]????[i+1]???
    private int[][] firstPositions = new int[6][]
    {
        new int[]{-7, -5}, 
        new int[]{8, 6},
        new int[]{8, 21},
        new int[]{-7, 11},
        new int[]{-7, 30},
        new int[]{8, 37}

    };

    public enum StageName
    {
        Floor1 = 0,
        Floor2 = 1,
        Floor3 = 2,
        None
    }
    public enum StairDirection
    {
        Up = 1,
        Down = 0
    }

    private StageName stage;
    private StairDirection stair;
    public StageName Stage() { return this.stage; }

    //?????????
    public static StageManager instance;

    //??????????
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            stage = StageName.Floor1;
            stair = StairDirection.Down;
        }
        else Destroy(this);
    }

    //???????
    public int[] GetFirstPosition()
    {
        return firstPositions[(int)stage * 2 + (int)stair];
    }

    //?????? ????????????????????
    public void MoveStage(StageName nextStage, StairDirection nextDirection)
    {
        stage = nextStage;
        stair = nextDirection;
        if(stage != StageName.None)
        {
            PlayerController playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerCon.GotoFirstPosition(firstPositions[(int)stage * 2 + (int)stair]);
        }
    }
}
