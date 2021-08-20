using UnityEngine;

//ステージ移動のためのコライダー
public class StageChangeCollider: MonoBehaviour
{
    public StageManager.FloorName nextFloor;
    public StageManager.StairDirection nextDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.instance.PrepareToMoveStage(nextFloor, nextDirection);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.instance.Cancel();
        }
    }

}
