using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ダメージマップの実装
//ダメージを与える実装サンプル
public class DamageMap : MonoBehaviour
{
    //与えるダメージ
    public int damageValue = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MobState>().Damaged(damageValue);
        }
    }
}
