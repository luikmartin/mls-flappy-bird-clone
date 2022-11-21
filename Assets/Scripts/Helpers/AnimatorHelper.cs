using UnityEngine;

public class AnimatorHelper : MonoBehaviour
{
    public void HandleNextSceneLoad() => GameObject.FindObjectOfType<ScenesController>().HandleNextSceneLoad();
}
