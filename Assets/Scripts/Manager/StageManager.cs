using System;
using System.Collections;
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
    public enum FloorName
    {
        Floor1 = 0,
        Floor2 = 1,
        Floor3 = 2,

        NextStage,
        PrevStage,
        None
    }

    //階段の向き
    public enum StairDirection
    {
        Up = 1,
        Down = 0
    }
    //現在のフロア/向きと次のフロア/向き
    
    private FloorName floor;
    private StairDirection stair;
    private FloorName nextFloor;
    private StairDirection nextDirection;


    //ステージにおけるプレイヤーとカメラの位置設定用
    private PlayerController playerCon;
    private CameraController cameraCon;
    private GameManager gameManager;

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
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraCon = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        //休憩エリアでなければ初期位置を指定
        if (gameManager.stage != GameManager.Stage.RestArea)
        {
            //次のステージに進んだ場合 
            if (gameManager.isRising)
            {
                floor = FloorName.Floor1;
                stair = StairDirection.Down;
            }
            //前のステージに戻った場合
            else
            {
                floor = FloorName.Floor3;
                stair = StairDirection.Up;
            }
            
            playerCon.GotoFirstPosition(firstPositions[(int)floor * 2 + (int)stair]);
            SetFirstCameraPotision();
        }       
    }

    private void Update()
    {
        //Mキーでフロア，ステージを移動
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (getMoveKey) MoveStage();
        }
    }


    //フロアの初期位置を取得
    public int[] GetFirstPlayerPosition()
    {
        return firstPositions[(int)floor * 2 + (int)stair];
    }

    private void SetFirstCameraPotision()
    {
        cameraCon.MoveFloor(cameraLimitaiton[(int)floor]);
    }

    //フロアの移動準備
    public void PrepareToMoveStage(FloorName floorName, StairDirection stairDirection)
    {
        getMoveKey = true;
        UiManager.instance.ShowMessage("Press [M]");
        nextFloor = floorName;
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
        floor = nextFloor;
        stair = nextDirection;

        if (floor == FloorName.None) return;
        //playerCon.StopMove();
        if(gameManager.stage == GameManager.Stage.RestArea)
        {
            if (floor == FloorName.NextStage)
            {
                gameManager.GotoField(true);
            }
            else if (floor == FloorName.PrevStage)
            {
                gameManager.GotoField(false);
            }
        }
        else
        {
            if (floor == FloorName.NextStage)
            {
                gameManager.GotoNextStage();
            }
            else if (floor == FloorName.PrevStage)
            {
                gameManager.GotoPrevStage();
            }
            else
            {
                playerCon.GotoFirstPosition(firstPositions[(int)floor * 2 + (int)stair]);
                cameraCon.MoveFloor(cameraLimitaiton[(int)floor]);
            }
        }
        
        
        //StartCoroutine(StartFloor(3f));
    }

    IEnumerator StartFloor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerCon.StartMove();
    }

    
}
