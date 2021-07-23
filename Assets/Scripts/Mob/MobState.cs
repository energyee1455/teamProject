using UnityEngine;

//??????????????
public abstract class MobState : MonoBehaviour
{
    //????????
    public enum State{
        Normal,
        Damaged,
        Die
    }
    public State state;
    //?????
    protected int Hp { set; get; }
    protected bool canAttack;
    protected float attackSpace{ set; get; } //????


    protected abstract void Attack();
    protected abstract void Move();

    //???????
    public abstract void Attacked(int damageValue);
}
