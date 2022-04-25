using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 position;
    [SerializeField] private Transform parent;
    [SerializeField] private int ballCountInLevel;
    [SerializeField] private float timeToShowPopUpScreen = 4f;
    [SerializeField] private float timeToShowQuitScreen = 4f;
    [SerializeField] BallType ballType;
    
    private GameObject[] _objs;
    private bool _boolCheckForPopup = true;
    private int _activeBallIndex = 0;
    private int _remainingBallCount;

    private UIManager _uiManager;
    private LevelManager _levelManager;
    void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        SpawnBall(ballCountInLevel);
        _objs[_activeBallIndex].SetActive(true);
        _activeBallIndex++;
    }

    private void FixedUpdate()
    {
        if (_activeBallIndex < _objs.Length)
        {
            ActivateBall();
        }
        CalculateRemainingBallCount();
    }

    void SpawnBall(int ballCount)
    {
        for (int i = 0; i < ballCount; i++) 
        { 
            Instantiate(ballPrefab, position, Quaternion.identity, parent); ;
        }
        _objs = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in _objs)
        {
            ball.SetActive(false);
            ball.GetComponent<MeshRenderer>().material = ballType.ballColor; 
            ball.GetComponent<TrailRenderer>().material = ballType.ballColor; 
            ball.GetComponent<Transform>().localScale = ballType.ballVolume; 
            ball.GetComponent<Rigidbody>().mass = ballType.ballMass; 
        }
    }

    void ActivateBall()
    {
        if(_objs[_activeBallIndex - 1] == null) return;
        if (_objs[_activeBallIndex - 1].CompareTag("Spawned") && _objs[_activeBallIndex - 1].transform.position.z > 0 || _objs[_activeBallIndex - 1].transform.position.x != 0)
        {
            _objs[_activeBallIndex].SetActive(true);
            _activeBallIndex++;
        }
    }

    void CalculateRemainingBallCount()
    {
        _remainingBallCount = _objs.Length - _activeBallIndex + 1;
        
        if((_objs[ballCountInLevel - 1].gameObject.GetComponent<InputManager>().IsTouchEnded))
        {
            _remainingBallCount = 0;
            if (_levelManager.CurrentLevelIndex == 9 &&  _remainingBallCount == 0)
            {
                Invoke("ShowQuitScreen", timeToShowQuitScreen);
            }
            else
            {
                Invoke("ShowPopUpScreen", timeToShowPopUpScreen);
            }
        }
        _uiManager.SetRemainingBallText(_remainingBallCount);
    }
    
    void ShowPopUpScreen()
    {
        foreach (GameObject ball in _objs)
        {
            if (!ball.activeInHierarchy && _boolCheckForPopup)
            {
                _uiManager.LoadContinuePanel();
                _boolCheckForPopup = false;
            }
        }
    }

    void ShowQuitScreen()
    {
        _uiManager.LoadQuitPanel();
    }
}
