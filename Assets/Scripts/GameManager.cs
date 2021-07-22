using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�p�[�e�B�[�̏�Ԃ�ێ�
//�v�N���X���ύX������
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //�p�[�e�B�[�����o�[��
    private int partyMenberNum;
    public int PartyMenberNum() { return this.partyMenberNum; }

    //�p�[�e�B�����o�[�̏�ԓo�^�p�F����HP��UI�ύX���炢�����g�����Ȃ�
    private List<MobState> friendsStateList = new List<MobState>();

    //�p�[�e�B�̑����f�[�^��ێ�(�g���ĂȂ�)
    public List<EquipmentData.Data> equipmentDataList = new List<EquipmentData.Data>();

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this);
    }

    private void Start()
    {
        partyMenberNum = 3;
    }

    //�p�[�e�B�L�����N�^�[��o�^
    public int AddToFriendStateList(MobState state, bool isPlayer)
    {
        if (isPlayer)
        {
            friendsStateList.Insert(0, state);
            return 0;
        }
        else
        {
            friendsStateList.Add(state);
            return friendsStateList.Count;
        }
        
    }


}
