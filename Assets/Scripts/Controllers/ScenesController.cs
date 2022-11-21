using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private string _nextSceneToLoad;

    private void Start()
    {
        Time.timeScale = 1;

        PlayLoadedAnimation();
    }

    public void LoadGameScene()
    {
        _nextSceneToLoad = "GameScene";
        _animator.Play("Load", -1, 0);
    }

    public void LoadMenuScene()
    {
        _nextSceneToLoad = "MenuScene";
        _animator.Play("Load", -1, 0);
    }

    public void PlayLoadedAnimation() => _animator.Play("Loaded", -1, 0);

    public void HandleNextSceneLoad() => StartCoroutine(LoadScene(_nextSceneToLoad));

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
