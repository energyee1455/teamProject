using UnityEngine;

//キャラクターの基本クラス
public abstract class MobState : MonoBehaviour
{
    //状態
    public enum State{
        Stop,
        Normal,
        Damaged,
        Die
    }
    public State state;
    
    protected int Hp { set; get; }
    protected bool canAttack;
    protected float attackSpace{ set; get; } //攻撃間隔

    //継承クラスで実装s
    protected abstract void Attack();
    protected abstract void Move();

    //攻撃を受けた時の処理
    public abstract void Attacked(int damageValue);
}
