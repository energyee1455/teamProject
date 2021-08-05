using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームの進行管理クラス
public class GameManager : MonoBehaviour
{
    //インスタンス化
    public static GameManager instance;

    //シングルトンパターン：ただ一つのインスタンス
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
    }

    //パーティメンバーの人数
    private int partyMenberNum;
    public int PartyMenberNum() { return partyMenberNum; }

    private List<MobState> friendsStateList = new List<MobState>();
    //リストに追加してパーティ内での番号を返す（プレイヤーは0番目固定）
    private bool didResisterPlayer = false;
    public int AddToFriendStateList(MobState state, bool isPlayer)
    {    
        if (isPlayer)
        {
            friendsStateList.Insert(0, state);
            didResisterPlayer = true;
            return 0;
        }
        else
        {
            partyMenberNum++;
            friendsStateList.Add(state);
            int num = friendsStateList.Count;
            if (didResisterPlayer)
            {
                return num - 1;
            }
            else
            {
                return num;
            }
        } 
    }


}
