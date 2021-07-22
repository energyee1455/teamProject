using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//メニューUIを変更
public class MenuUI : MonoBehaviour
{
    public static MenuUI instance;

    public Text stageName;//ステージの名前

    //HPのUIテキスト
    public List<Text> hpText = new List<Text>();

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
        stageName.text = StageManager.instance.Stage().ToString();

    }

    //HPをUIテキストに反映
    public void SetHpUi(int num, string hp)
    {
        hpText[num].text = hp;
    }
}
