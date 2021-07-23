using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;


//�X�e�[�W�ԍ��ƃv���C���[�̏����ʒu�̊Ǘ��N���X

public class StageManager : MonoBehaviour
{
    //�����ʒu�z��(StageNum * 2 + StairDirection =�@�X�e�[�W���Ƃ̊K�i�O�̍��W)
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


    //�C���X�^���X
    public static StageManager instance;

    //�C���X�^���X���쐬(�V���O���g��)
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

    //�X�e�[�W�ړ���̏������W��Ԃ�
    public int[] GetFirstPosition()
    {
        return firstPositions[(int)stage * 2 + (int)stair];
    }

    //���̃X�e�[�W�Ɉړ�
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
