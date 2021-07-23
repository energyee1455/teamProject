using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//敵キャラの制御クラス
public class EnemyConttoller : MobState
{
    public override void Attacked(int damageValue)
    { 
        int damageCut = 0;//ステータスから取得
        int damage = damageValue - damageCut;
        if (damage > 0)
        {
            state = State.Damaged;
            Hp = (Hp - damage);
            if(Hp <= 0)
            {
                state = State.Die;
            }
        }
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }



}
