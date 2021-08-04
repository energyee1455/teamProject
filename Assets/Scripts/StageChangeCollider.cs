using UnityEngine;

//??????????????
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
