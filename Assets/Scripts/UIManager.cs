using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inGameUIPanel;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private TextMeshProUGUI remainingBallText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI popUpScoreText;
    
    private GameObject _ball;

    private LevelManager _levelManager;
    private BasketManager _basketManager;
    
    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _basketManager = FindObjectOfType<BasketManager>();
        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        BasketManager.OnScored += SetScoreText;
        BasketManager.OnScored += SetPopUpScoreText;
        _levelManager.LoadFirstLevel();
    }
    
    private void Update()
    {
        DisableInputForMenuPanel();
    }
    

    void StartGame()
    {
        menuPanel.SetActive(false);
        inGameUIPanel.SetActive(true);
    }

    void ContinueGame()
    {
        continuePanel.SetActive(false);
        _levelManager.LoadNextLevel();
        SetScoreText(0);
        SetPopUpScoreText(0);
    }

    void QuitGame()
    {
        Debug.Log("ApplicationQuit");
        Application.Quit();
    }

    public void LoadContinuePanel()
    {
        continuePanel.SetActive(true);
    }

    public void LoadQuitPanel()
    {
        inGameUIPanel.SetActive(false);
        quitPanel.SetActive(true);
    }

    void DisableInputForMenuPanel()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        if(_ball == null) {return;}
        if (menuPanel.activeInHierarchy)
        {
            _ball.GetComponent<InputManager>().enabled = false;
        }
        else
        {
            _ball.GetComponent<InputManager>().enabled = true;
        }
    }
    
    public void SetRemainingBallText(int ballCount) => remainingBallText.text = ballCount.ToString();

    public void SetLevelText(int levelNum) => levelText.text = levelNum.ToString();

    public void SetScoreText(int score) => scoreText.text = score.ToString();

    public void SetPopUpScoreText(int score) =>  popUpScoreText.text = "Score: " + score.ToString();
}