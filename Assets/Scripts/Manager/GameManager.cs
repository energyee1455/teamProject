using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//?p?[?e?B?[????????????
//?v?N???X?????X??????
public class GameManager : MonoBehaviour
{
    //インスタンス化
    public static GameManager instance;


    //?p?[?e?B?[?????o?[??
    private int partyMenberNum;
    public int PartyMenberNum() { return this.partyMenberNum; }

    //?p?[?e?B?????o?[???????o?^?p?F????HP??UI???X???????????g????????
    private List<MobState> friendsStateList = new List<MobState>();

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
        partyMenberNum = 3;
    }

    //?p?[?e?B?L?????N?^?[???o?^
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
