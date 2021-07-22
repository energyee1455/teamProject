using UnityEngine;

//ステージ上のマップ移動用当たり判定

public class StageChangeCollider: MonoBehaviour
{
    public StageManager.StageName nextStage;
    public StageManager.StairDirection nextDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.instance.MoveStage(nextStage, nextDirection);
        }
    }
}
