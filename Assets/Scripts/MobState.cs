using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�L�����N�^�[��Ԃ̔ėp�N���X
public abstract class MobState : MonoBehaviour
{
    //�L�����N�^�[���
    public enum State{
        Alive,
        Damaged,
        Die
    }

    //���ʃp�����[�^
    int HP;
    bool canAttack;
    float coolTime;

    protected abstract void Attack();
    protected abstract void Move();


    //�_���[�W���󂯂�
    void Damaged(int damageValue, int damageCut)
    {
        int damage = damageValue - damageCut;
        if (damage < 0) damage = 0;
        HP -= damage;
    }
}
