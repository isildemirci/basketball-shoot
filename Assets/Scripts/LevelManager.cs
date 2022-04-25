using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;
    public List<GameObject> Levels => levels;
    private GameObject _currentLevel;
    private int _currentLevelIndex;
    public int CurrentLevelIndex => _currentLevelIndex;
    private int _levelNum;
    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    void LoadLevel(int levelIndex)
    {
        DeleteLevel();
        _currentLevel = Instantiate(levels[levelIndex]);
        _currentLevelIndex = levelIndex;
        SetLevelNumber();
    }
    
    public void LoadFirstLevel()
    {
        LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        _currentLevelIndex = _currentLevelIndex + 1 == levels.Count ? 0 : _currentLevelIndex + 1;
        LoadLevel(_currentLevelIndex);
    }
    
    void DeleteLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }
    }
    
    void SetLevelNumber()
    {
        _levelNum = _currentLevelIndex + 1;
        _uiManager.SetLevelText(_levelNum);
    }
    
}
