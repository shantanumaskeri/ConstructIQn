using UnityEngine;

public class QuizResult : MonoBehaviour
{

    public GameObject Win;
    public GameObject Lose;
    public TextMesh winScore;
    public TextMesh winMessage;
    public TextMesh loseScore;
    public TextMesh loseMessage;

    private GameObject application;

    private void Start()
    {
        application = GameObject.Find("Application");

        DisplayResult();    
    }

    private void DisplayResult()
    {
        if (application.GetComponent<ApplicationManager>().result == "Win")
        {
            Win.SetActive(true);
            winMessage.text = "You answered all " + application.GetComponent<ApplicationManager>().totalLevels + " questions correctly!";
            winScore.text = "YOUR SCORE IS " + application.GetComponent<ApplicationManager>().gameScore + ".";
        }
        else
        {
            Lose.SetActive(true);
            loseMessage.text = "Tough Luck!";
            loseScore.text = "YOUR SCORE IS " + application.GetComponent<ApplicationManager>().gameScore + ".";
        }
    }

}
