using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクター状態の汎用クラス
public abstract class MobState : MonoBehaviour
{
    //キャラクター状態
    public enum State{
        Alive,
        Damaged,
        Die
    }

    //共通パラメータ
    int HP;
    bool canAttack;
    float coolTime;

    protected abstract void Attack();
    protected abstract void Move();


    //ダメージを受ける
    void Damaged(int damageValue, int damageCut)
    {
        int damage = damageValue - damageCut;
        if (damage < 0) damage = 0;
        HP -= damage;
    }
}
