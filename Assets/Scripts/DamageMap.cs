using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ダメージを与えるマップオブジェクト
//ダメージ処理のサンプル
public class DamageMap : MonoBehaviour
{
    //ダメージ量
    public int damageValue = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MobState>().Attacked(damageValue);
        }
    }
}
