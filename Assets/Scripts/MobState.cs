using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクター状態の抽象クラス
//プレイヤー，味方，敵のそれぞれで実装
public abstract class MobState : MonoBehaviour
{
    //キャラクター状態
    public enum State{
        Normal,
        Damaged,
        Die
    }

    //共通パラメータ
    protected int Hp { set; get; }
    protected bool canAttack;
    protected float coolTime { set; get; }


    protected abstract void Attack();
    protected abstract void Move();

    //ダメージを受ける
    public abstract void Damaged(int damageValue);
}
