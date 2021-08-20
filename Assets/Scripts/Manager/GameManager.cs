using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Initialize();
        }
    }

    //パーティメンバーの人数
    public int partyMenberNum;

    //ステージ
    public enum Stage
    {
        RestArea,
        Field
    }
    public int currentStageNum;
    public int nextStageNum;
    public Stage stage;
    public bool isRising;

    private void Initialize()
    {
        stage = Stage.RestArea;
        currentStageNum = 0;
        nextStageNum = 1;
        isRising = true;
    }

    public void GotoNextStage()
    {
        stage = Stage.RestArea;
        nextStageNum = currentStageNum+1;
        SceneManager.LoadScene("RestPoint");
    }
    public void GotoPrevStage()
    {
        stage = Stage.RestArea;
        nextStageNum = currentStageNum;
        currentStageNum--;
        SceneManager.LoadScene("RestPoint");
    }

    public void GotoField(bool exitDirection)
    {
        if(exitDirection)
        {
            if (nextStageNum > currentStageNum)
            {
                currentStageNum++;
                isRising = true; //上った
            }
            else
            {
                currentStageNum--;
                isRising = false; //下がった
            }
        }
        else
        {
            if (nextStageNum > currentStageNum) isRising = false;　//上に行こうとして下に戻った
            else isRising = true; //下に行こうとして上に戻った
        }

        //最初の場合は除外
        if(currentStageNum <= 0)
        {
            currentStageNum = 0;
            return;
        }
        stage = Stage.Field;
        SceneManager.LoadScene("Stage" + currentStageNum.ToString());
    }

    //味方の生成
    public GameObject friendObject;
    public void AddPartyMember()
    {
        Debug.Log(partyMenberNum);
        for (int i=0; i<partyMenberNum; i++)
        {
            Debug.Log("instantiate");
            GameObject friend;
            friend = Instantiate(friendObject, new Vector3(-13f, -10f, 0), Quaternion.identity);
            friend.GetComponent<FriendController>().friendNum = i;
        }  
    }


}
