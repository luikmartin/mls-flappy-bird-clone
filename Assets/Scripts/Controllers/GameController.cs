using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _pointClip;
    [SerializeField]
    private AudioClip _dieClip;
    [Space(10)]
    [SerializeField]
    private Animator _groundAnimator;

    private GameUIController _gameUIController;
    private AudioSource _audioSource;

    private int _points;
    private int _maxPoints;

    private void Awake()
    {
        _gameUIController = GameObject.FindObjectOfType<GameUIController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() => _maxPoints = PlayerPrefs.GetInt(Constants.TOP_SCORE_KEY);

    public void Play() => GameObject.FindObjectOfType<PipeSpawner>().StartSpawning();

    public void Pause() => Time.timeScale = 0;

    public void Resume() => Time.timeScale = 1;

    public void GameOver()
    {
        _groundAnimator.enabled = false;

        foreach (var pipe in GameObject.FindObjectsOfType<PipesBehaviour>())
        {
            pipe.SetIsMoving(false);
        }
        var isNewHighScore = _points > _maxPoints;

        if (isNewHighScore)
        {
            _maxPoints = _points;
            PlayerPrefs.SetInt(Constants.TOP_SCORE_KEY, _maxPoints);
        }
        GameObject.FindObjectOfType<PipeSpawner>().StopSpawning();
        GameObject.FindObjectOfType<GameUIController>().ShowGameOverView(_points, getMedal(_points), _maxPoints, isNewHighScore);

        _audioSource.clip = _dieClip;
        _audioSource.Play();
    }

    public void AddPoint()
    {
        _points++;
        _gameUIController.SetScore(_points);
        _audioSource.Play();
    }

    private Medal getMedal(int points)
    {
        if (points < 10)
        {
            return Medal.None;
        }
        else if (points >= 10 && points < 20)
        {
            return Medal.Bronze;
        }
        else if (points >= 20 && points < 30)
        {
            return Medal.Silver;
        }
        else if (points >= 30 && points < 40)
        {
            return Medal.Gold;
        }
        return Medal.Platinum;
    }
}
