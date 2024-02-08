using Unity.VisualScripting;
using UnityEngine;

public class KillPlayerTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().StateMachine.ChangeState(other.GetComponent<Player>().DeathFallState);
        }
    }
}
