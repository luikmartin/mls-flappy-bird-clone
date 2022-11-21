using UnityEngine;

public class PipeBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == Constants.PLAYER_NAME)
        {
            GameObject.FindObjectOfType<PlayerController>().Kill();
        }
    }
}
