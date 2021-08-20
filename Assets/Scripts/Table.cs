using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public GameObject Menu;
    public GameObject decisionButton;
    private int memberNum = 0;
    public Text numText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Menu.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Menu.SetActive(false);
            GameManager.instance.partyMenberNum = memberNum;
        }
    }

    public void OnMemberNumButton()
    {
        memberNum++;
        numText.text = memberNum.ToString() + "êl";
    }


}
