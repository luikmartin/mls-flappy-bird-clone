using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private Animator _uiAnimator;
    [Space(10)]
    [SerializeField]
    private NumbersBehaviour _score;
    [SerializeField]
    private NumbersBehaviour _scoreCardScore;
    [SerializeField]
    private NumbersBehaviour _scoreCardHighScore;
    [Space(10)]
    [SerializeField]
    private GameObject _bronzeMedal;
    [SerializeField]
    private GameObject _silverMedal;
    [SerializeField]
    private GameObject _goldMedal;
    [SerializeField]
    private GameObject _platinumMedal;
    [Space(10)]
    [SerializeField]
    private GameObject _newHighScoreTag;
    [Space(10)]
    [SerializeField]
    private GameObject _gameView;
    [SerializeField]
    private GameObject _pausedView;

    private GameController _gameController;
    private PlayerController _playerController;

    private bool _isTutorialVisible = true;

    private void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void SetScore(int value) => _score.SetValue(value);

    public void Flap()
    {
        if (_isTutorialVisible)
        {
            _isTutorialVisible = false;
            _uiAnimator.Play("HideGetReady", -1, 0);

            GameObject.FindObjectOfType<GameController>().Play();
        }
        _playerController.Flap();
    }

    public void Pause()
    {
        if (_isTutorialVisible)
        {
            return;
        }
        _gameView.SetActive(false);
        _pausedView.SetActive(true);

        _gameController.Pause();
    }

    public void Resume()
    {
        _gameView.SetActive(true);
        _pausedView.SetActive(false);

        _gameController.Resume();
    }

    public void QuitToMainMenu() => GetComponent<ScenesController>().LoadMenuScene();

    public void ShowGameOverView(int score, Medal medal = Medal.None, int maxScore = -1, bool isNewMaxScore = false)
    {
        _gameView.SetActive(false);

        _scoreCardScore.SetValue(score);
        _scoreCardHighScore.SetValue(maxScore);
        _newHighScoreTag.SetActive(isNewMaxScore);

        ShowMedalIfApplicable(medal);

        _uiAnimator.Play("GameOver", -1, 0);
    }

    public void Share() => Debug.LogWarning("Share() not implemented");

    private void ShowMedalIfApplicable(Medal medal)
    {
        _bronzeMedal.SetActive(medal.Equals(Medal.Bronze));
        _silverMedal.SetActive(medal.Equals(Medal.Silver));
        _goldMedal.SetActive(medal.Equals(Medal.Gold));
        _platinumMedal.SetActive(medal.Equals(Medal.Platinum));
    }
}
