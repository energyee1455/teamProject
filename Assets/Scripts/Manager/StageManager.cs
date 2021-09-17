using System;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;


//?X?e?[?W?????N???X
public class StageManager : MonoBehaviour
{
    //?t???A?????????u
    private int[][] firstPositions = new int[6][]
    {
        new int[]{-7, -5}, 
        new int[]{8, 6},
        new int[]{8, 21},
        new int[]{-7, 11},
        new int[]{-7, 30},
        new int[]{8, 37}
    };

    //?e?t???A???J????????????
    private float[][] cameraLimitaiton = new float[3][]
    {
        new float[]{0, 0, -2, 2},
        new float[]{0, 0, 15, 19},
        new float[]{0, 0, 32, 36}
    };

    //?e?t???A?????O
    public enum FloorName
    {
        Floor1 = 0,
        Floor2 = 1,
        Floor3 = 2,

        NextStage,
        PrevStage,
        Clear,
        None
    }

    //?K?i??????
    public enum StairDirection
    {
        Up = 1,
        Down = 0
    }
    //???????t???A/???????????t???A/????
    
    private FloorName floor;
    private StairDirection stair;
    private FloorName nextFloor;
    private StairDirection nextDirection;


    //?X?e?[?W?????????v???C???[???J?????????u?????p
    private PlayerController playerCon;
    private CameraController cameraCon;
    private GameManager gameManager;

    //?t???A???????L?[??????????????????????
    private bool getMoveKey;

    //?C???X?^???X
    public static StageManager instance;
    //?C???X?^???X??????
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
        //?x?e?G???A?????????????????u???w??
        if (gameManager.stage != GameManager.Stage.RestArea)
        {
            //?????X?e?[?W???i???????? 
            if (gameManager.isRising)
            {
                floor = FloorName.Floor1;
                stair = StairDirection.Down;
            }
            //?O???X?e?[?W????????????
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
        //M?L?[???t???A?C?X?e?[?W??????
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (getMoveKey) MoveStage();
        }
    }


    //?t???A?????????u??????
    public int[] GetFirstPlayerPosition()
    {
        return firstPositions[(int)floor * 2 + (int)stair];
    }

    private void SetFirstCameraPotision()
    {
        cameraCon.MoveFloor(cameraLimitaiton[(int)floor]);
    }

    //?t???A??????????
    public void PrepareToMoveStage(FloorName floorName, StairDirection stairDirection)
    {
        getMoveKey = true;
        UiManager.instance.ShowMessage("Press [M]");
        nextFloor = floorName;
        nextDirection = stairDirection;
    }
    //?t???A???????L?????Z??
    public void Cancel()
    {
        getMoveKey = false;
        UiManager.instance.HideMessage();
    }
    //?t???A??????
    private void MoveStage()
    {
        floor = nextFloor;
        stair = nextDirection;

        if(floor == FloorName.Clear)
        {
            SceneManager.LoadScene("Clear");
        }

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
