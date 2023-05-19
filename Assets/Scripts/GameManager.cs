using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    [SerializeField] private LevelManager[] Levels;
    private int currentLevel;
    [SerializeField] private TextMeshProUGUI level_text;
    private int count_state = 0;
    private int count_lose = 0;
    public int max_count_state;
    private bool is_load;
    public LevelManager current_level;

    public LevelManager CurrentLevel => current_level;
    private void Awake()
    {

        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        LoadBeginLevel();
        level_text.text = "LEVEL " + currentLevel.ToString();
        max_count_state = PathManager.Instance.GetMaxLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(count_state == max_count_state && count_lose != 0)
        {
            Debug.Log("game over");
        }
        else if(count_state == max_count_state && count_lose == 0 && !is_load)
        {
            Debug.Log("vao dat");
            Debug.Log("win");
            LoadNextLevel();
             is_load = true;
            ResetCount();
            max_count_state = PathManager.Instance.GetMaxLine();
        }
    }
    private void UpdateLevelText()
    {
        level_text.text = "LEVEL " + PlayerPrefs.GetInt("CurrentLevel").ToString();
    }
    void LoadNextLevel()
    {
        currentLevel++;
        Destroy(current_level.gameObject);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        Debug.Log("set level: " + currentLevel);
        UpdateLevelText();
/*        Levels[currentLevel - 1].gameObject.SetActive(true);
        Levels[currentLevel - 2].gameObject.SetActive(false);*/
        
        ResetCount();
        PathManager.Instance.Reset();
        current_level = Instantiate(Levels[currentLevel - 1]);

        max_count_state = PathManager.Instance.GetMaxLine();
        current_level.gameObject.SetActive(true);

        current_level.drawController.Init();
    }
    public void AddCountState()
    {
        count_state++;
    }
    public void AddCountLose()
    {
        count_lose++;
    }
    private void ResetCount()
    {
        count_lose = 0;
        count_state = 0;
        is_load = false;
    }
    private void LoadBeginLevel()
    {
        Debug.Log("curent level: " + PlayerPrefs.GetInt("CurentLevel"));
        current_level = Instantiate(Levels[currentLevel - 1]);
        current_level.gameObject.SetActive(true);

       // current_level.drawController.Init();

    }
    
}
