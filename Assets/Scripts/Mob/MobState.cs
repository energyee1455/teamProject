using UnityEngine;

//�L�����N�^�[�̊�{�N���X
public abstract class MobState : MonoBehaviour
{
    //���
    public enum State{
        Stop,
        Normal,
        Damaged,
        Die
    }
    public State state;
    
    protected int Hp { set; get; }
    protected bool canAttack;
    protected float attackSpace{ set; get; } //�U���Ԋu

    //�p���N���X�Ŏ���s
    protected abstract void Attack();
    protected abstract void Move();

    //�U�����󂯂����̏���
    public abstract void Attacked(int damageValue);
}
