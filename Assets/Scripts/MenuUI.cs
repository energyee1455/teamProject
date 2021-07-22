using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���j���[UI��ύX
public class MenuUI : MonoBehaviour
{
    public static MenuUI instance;

    public Text stageName;//�X�e�[�W�̖��O

    //HP��UI�e�L�X�g
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

    //HP��UI�e�L�X�g�ɔ��f
    public void SetHpUi(int num, string hp)
    {
        hpText[num].text = hp;
    }
}
