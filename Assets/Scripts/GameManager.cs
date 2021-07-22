using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//パーティーの状態を保持
//要クラス名変更＆分解
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //パーティーメンバー数
    private int partyMenberNum;
    public int PartyMenberNum() { return this.partyMenberNum; }

    //パーティメンバーの状態登録用：今はHPのUI変更くらいしか使い道なし
    private List<MobState> friendsStateList = new List<MobState>();

    //パーティの装備データを保持(使ってない)
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

    //パーティキャラクターを登録
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
