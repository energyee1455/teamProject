using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public enum MobType
    {
        Friend = 0,
        Enemy = 1
    }
    MobType type;
    public float detectRange = 10f;
    CircleCollider2D detectionArea;
    bool canAttack;
    Transform target;

    MobState controller;
    

    private void Start()
    {
        detectionArea.radius = detectRange;
        controller = GetComponentInParent<MobState>();
        canAttack = false;
    }

    public Transform SetTarget()
    {
        if (canAttack)
        {
            return target;
        }
        else return null;
    }


    //感知範囲の敵をターゲットに指定
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(type.ToString()))
        {
            canAttack = true;
            target = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(type.ToString()))
        {
            canAttack = false;
        }
    }


}
