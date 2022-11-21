using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    public void Play()
    {
        GameObject.FindObjectOfType<ScenesController>().LoadGameScene();
        GetComponent<AudioSource>().Play();
    }

    public void Rate() => Debug.LogWarning("Rate() not implemented");

    public void Score() => Debug.LogWarning("Score() not implemented");
}
