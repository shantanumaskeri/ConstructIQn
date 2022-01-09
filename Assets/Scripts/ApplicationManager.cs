using UnityEngine;

public class ApplicationManager : MonoBehaviour
{

    [HideInInspector]
    public int gameScore = 0;
    [HideInInspector]
    public int totalQuestions = 0;
    [HideInInspector]
    public int totalLevels = 0;
    [HideInInspector]
    public float levelTime = 0.0f;
    [HideInInspector]
    public string result = "";

    private string gameMode;
    
    private void Start()
    {
        SetGameMode(PlayerPrefs.GetString("difficulty"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetGameMode(string mode)
    {
        gameMode = mode;
        PlayerPrefs.SetString("difficulty", gameMode);
    }

    public string GetGameMode()
    {
        if (PlayerPrefs.GetString("difficulty") == "")
        {
            gameMode = "easy";
        }
        else
        {
            gameMode = PlayerPrefs.GetString("difficulty");
        }
        
        return gameMode;
    } 

}
