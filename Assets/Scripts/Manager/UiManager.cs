using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI???
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Text stageName;//?X?e?[?W?????O

    //HP????(????)
    public List<Text> hpText = new List<Text>();

    //????????
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
        //????????
        stageName.text = StageManager.instance.Stage().ToString();
    }

    //HP???
    public void SetHpUi(int num, string hp)
    {
        hpText[num].text = hp;
    }
}
