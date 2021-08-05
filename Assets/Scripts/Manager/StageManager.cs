using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;


//�X�e�[�W�Ǘ��N���X
public class StageManager : MonoBehaviour
{
    //�t���A�̏����ʒu
    private int[][] firstPositions = new int[6][]
    {
        new int[]{-7, -5}, 
        new int[]{8, 6},
        new int[]{8, 21},
        new int[]{-7, 11},
        new int[]{-7, 30},
        new int[]{8, 37}
    };
    //�e�t���A�̃J�����ړ�����
    private float[][] cameraLimitaiton = new float[3][]
    {
        new float[]{0, 0, -2, 2},
        new float[]{0, 0, 15, 19},
        new float[]{0, 0, 32, 36}
    };
    //�e�t���A�̖��O
    public enum StageName
    {
        Floor1 = 0,
        Floor2 = 1,
        Floor3 = 2,
        None
    }
    //�K�i�̌���
    public enum StairDirection
    {
        Up = 1,
        Down = 0
    }
    //���݂̃X�e�[�W�C�����Ǝ��̃X�e�[�W�C����
    private StageName stage;
    private StairDirection stair;
    private StageName nextStage;
    private StairDirection nextDirection;
    public StageName Stage() { return this.stage; }

    //�X�e�[�W�ɂ�����v���C���[�ƃJ�����̈ʒu�ݒ�p
    private PlayerController playerCon;
    private CameraController cameraCon;

    //�t���A�ړ��̃L�[���͂��󂯎�邩�ǂ���
    private bool getMoveKey;

    //�C���X�^���X
    public static StageManager instance;
    //�C���X�^���X�̐���
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

    //�t���A�̏����ʒu���擾
    public int[] GetFirstPosition()
    {
        return firstPositions[(int)stage * 2 + (int)stair];
    }

    private void SetFirstCameraPotision()
    {
        cameraCon.MoveFloor(cameraLimitaiton[(int)stage]);
    }

    //�t���A�̈ړ�����
    public void PrepareToMoveStage(StageName stageName, StairDirection stairDirection)
    {
        getMoveKey = true;
        UiManager.instance.ShowMessage("Press [M]");
        nextStage = stageName;
        nextDirection = stairDirection;
    }
    //�t���A�ړ��̃L�����Z��
    public void Cancel()
    {
        getMoveKey = false;
        UiManager.instance.HideMessage();
    }
    //�t���A�̈ړ�
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
