using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI管理クラス
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Text stageName;//

    //HP表示
    public List<Text> hpText = new List<Text>();

    //メッセージの表示領域
    public GameObject messageObj;
    public Text messageText;

    //インスタンス生成
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }

    private void Start()
    {
        //ステージの名前を表示
        stageName.text = StageManager.instance.Stage().ToString();
    }

    //HPを表示
    public void SetHpUi(int num, string hp)
    {
        hpText[num].text = hp;
    }

    //メッセージを表示
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageObj.SetActive(true);
    }

    //メッセージを非表示
    public void HideMessage()
    {
        messageObj.SetActive(false);
    }
}
