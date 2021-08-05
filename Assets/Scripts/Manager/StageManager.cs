using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;


//ステージ管理クラス
public class StageManager : MonoBehaviour
{
    //フロアの初期位置
    private int[][] firstPositions = new int[6][]
    {
        new int[]{-7, -5}, 
        new int[]{8, 6},
        new int[]{8, 21},
        new int[]{-7, 11},
        new int[]{-7, 30},
        new int[]{8, 37}
    };
    //各フロアのカメラ移動制限
    private float[][] cameraLimitaiton = new float[3][]
    {
        new float[]{0, 0, -2, 2},
        new float[]{0, 0, 15, 19},
        new float[]{0, 0, 32, 36}
    };
    //各フロアの名前
    public enum StageName
    {
        Floor1 = 0,
        Floor2 = 1,
        Floor3 = 2,
        None
    }
    //階段の向き
    public enum StairDirection
    {
        Up = 1,
        Down = 0
    }
    //現在のステージ，向きと次のステージ，向き
    private StageName stage;
    private StairDirection stair;
    private StageName nextStage;
    private StairDirection nextDirection;
    public StageName Stage() { return this.stage; }

    //ステージにおけるプレイヤーとカメラの位置設定用
    private PlayerController playerCon;
    private CameraController cameraCon;

    //フロア移動のキー入力を受け取るかどうか
    private bool getMoveKey;

    //インスタンス
    public static StageManager instance;
    //インスタンスの生成
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

    private void Start()
    {
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraCon = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        SetFirstCameraPotision();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (getMoveKey) MoveStage();
        }
    }

    //フロアの初期位置を取得
    public int[] GetFirstPosition()
    {
        return firstPositions[(int)stage * 2 + (int)stair];
    }

    private void SetFirstCameraPotision()
    {
        cameraCon.MoveFloor(cameraLimitaiton[(int)stage]);
    }

    //フロアの移動準備
    public void PrepareToMoveStage(StageName stageName, StairDirection stairDirection)
    {
        getMoveKey = true;
        UiManager.instance.ShowMessage("Press [M]");
        nextStage = stageName;
        nextDirection = stairDirection;
    }
    //フロア移動のキャンセル
    public void Cancel()
    {
        getMoveKey = false;
        UiManager.instance.HideMessage();
    }
    //フロアの移動
    private void MoveStage()
    {
        stage = nextStage;
        stair = nextDirection;
        if (stage != StageName.None)
        {
            playerCon.GotoFirstPosition(firstPositions[(int)stage * 2 + (int)stair]);
            cameraCon.MoveFloor(cameraLimitaiton[(int)stage]);
        }
    }

    
}
