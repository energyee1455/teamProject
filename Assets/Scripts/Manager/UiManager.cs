using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI�Ǘ��N���X
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Text stageName;//

    //HP�\��
    public List<Text> hpText = new List<Text>();

    //���b�Z�[�W�̕\���̈�
    public GameObject messageObj;
    public Text messageText;

    //�C���X�^���X����
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
        //�X�e�[�W�̖��O��\��
        stageName.text = StageManager.instance.Stage().ToString();
    }

    //HP��\��
    public void SetHpUi(int num, string hp)
    {
        hpText[num].text = hp;
    }

    //���b�Z�[�W��\��
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageObj.SetActive(true);
    }

    //���b�Z�[�W���\��
    public void HideMessage()
    {
        messageObj.SetActive(false);
    }
}
