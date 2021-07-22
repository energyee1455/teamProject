using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;


//ステージ番号とプレイヤーの初期位置の管理クラス

public class StageManager : MonoBehaviour
{
    //初期位置配列(StageNum * 2 + StairDirection =　ステージごとの階段前の座標)
    private int[][] firstPositions = new int[4][]
    {
        new int[]{-7, -5},
        new int[]{8, 6},
        new int[]{7, 5},
        new int[]{2, 3}
    };

    public enum StageName
    {
        Floor1 = 0,
        Floor2 = 1,
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


    //インスタンス
    public static StageManager instance;

    //インスタンスを作成(シングルトン)
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

    //ステージ移動後の初期座標を返す
    public int[] GetFirstPosition()
    {
        return firstPositions[(int)stage * 2 + (int)stair];
    }

    //次のステージに移動
    public void MoveStage(StageName nextStage, StairDirection nextDirection)
    {
        stage = nextStage;
        stair = nextDirection;
        if(stage != StageName.None)
        {
            SceneManager.LoadScene(nextStage.ToString());
        }
    }
}
