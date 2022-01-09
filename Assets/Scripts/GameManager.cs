using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public GameObject game;
	public LevelLoader levelLoader;
	public XMLParser xmlParser;
	public RandomNumberGenerator randomNumberGenerator;

	public Text questionText;
	public Image answer1Img;
	public Image answer2Img;
	public Image answer3Img;
	public Image answer4Img;
	public Text answer1Text;
	public Text answer2Text;
	public Text answer3Text;
	public Text answer4Text;
	public Text timerText;
	
	private float timer;
	private bool levelCompleted;
	private string question;
	private string answer1;
	private string answer2;
	private string answer3;
	private string answer4;
	private string correctAnswer;
	private int actualLevel;
	private int numberOfCorrectAnswers;
	private GameObject application;
	private Text selectedAnswer;

	private void Start()
    {
		levelCompleted = false;
		
		application = GameObject.Find("Application");
		application.GetComponent<ApplicationManager>().gameScore = 0;
	}

    private void Init()
    {
		ResetQuiz();
		StartCoroutine(CreateLevel(0.0f));
    }

    private void Update()
    {
		if (!levelCompleted)
		{
			timer -= Time.deltaTime;

			if (timer < 0.0f)
			{
				timer = 0.0f;

				levelCompleted = true;

				MoveToNextLevel();
			}

			ShowTimer();
		}
    }

    private IEnumerator CreateLevel(float delay)
	{
		yield return new WaitForSeconds(delay);

		ResetQuiz();
		
		xmlParser.game[randomNumberGenerator.inGameLevels[actualLevel]].TryGetValue("question", out question);
		xmlParser.game[randomNumberGenerator.inGameLevels[actualLevel]].TryGetValue("answer1", out answer1);
		xmlParser.game[randomNumberGenerator.inGameLevels[actualLevel]].TryGetValue("answer2", out answer2);
		xmlParser.game[randomNumberGenerator.inGameLevels[actualLevel]].TryGetValue("answer3", out answer3);
		xmlParser.game[randomNumberGenerator.inGameLevels[actualLevel]].TryGetValue("answer4", out answer4);
		xmlParser.game[randomNumberGenerator.inGameLevels[actualLevel]].TryGetValue("correctAnswer", out correctAnswer);

		questionText.text = question;
		answer1Text.text = answer1;
		answer2Text.text = answer2;
		answer3Text.text = answer3;
		answer4Text.text = answer4;

		answer1Img.color = answer2Img.color = answer3Img.color = answer4Img.color = Color.white;
		answer1Text.color = answer2Text.color = answer3Text.color = answer4Text.color = new Color(0.255f, 0.255f, 0.255f);

		AdjustLayoutByNumberOfAnswers(answer3Text);
		AdjustLayoutByNumberOfAnswers(answer4Text);

		StartCoroutine(GazeInput.Instance.Initialize());
	}

	private void AdjustLayoutByNumberOfAnswers(Text answer)
	{
		if (answer.text == "")
		{
			answer.rectTransform.parent.gameObject.SetActive(false);
		}
		else
		{
			answer.rectTransform.parent.gameObject.SetActive(true);
		}
	}

	private void ShowTimer()
    {
		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer - minutes * 60);
		string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

		timerText.text = formattedTime;
	}

	public void SubmitQuizAnswer(string objectName)
	{
		switch (objectName)
		{
			case "Answer Bg 1":
				selectedAnswer = answer1Text;
				break;

			case "Answer Bg 2":
				selectedAnswer = answer2Text;
				break;

			case "Answer Bg 3":
				selectedAnswer = answer3Text;
				break;

			case "Answer Bg 4":
				selectedAnswer = answer4Text;
				break;
		}

        if (!levelCompleted)
        {
			if (selectedAnswer.text == correctAnswer)
			{
				selectedAnswer.color = Color.white;
				selectedAnswer.rectTransform.parent.gameObject.GetComponent<Image>().color = Color.green;
				
				levelCompleted = true;

				numberOfCorrectAnswers++;

				IncrementScore();
			}
			else
			{
				selectedAnswer.color = Color.white;
				selectedAnswer.rectTransform.parent.gameObject.GetComponent<Image>().color = Color.red;
				
				RevealCorrectAnswer(answer1Text, correctAnswer);
				RevealCorrectAnswer(answer2Text, correctAnswer);
				RevealCorrectAnswer(answer3Text, correctAnswer);
				RevealCorrectAnswer(answer4Text, correctAnswer);

				levelCompleted = true;
			}

			MoveToNextLevel();
		}		
	}

	private void RevealCorrectAnswer(Text answerText, string answer)
	{
		if (answerText.text == answer)
		{
			answerText.color = Color.white;
			answerText.rectTransform.parent.gameObject.GetComponent<Image>().color = Color.green;
		}
	}

	private void IncrementScore()
    {
		switch (application.GetComponent<ApplicationManager>().GetGameMode())
		{
			case "easy":
				application.GetComponent<ApplicationManager>().gameScore += Mathf.FloorToInt(timer) * 20;
				break;

			case "medium":
				application.GetComponent<ApplicationManager>().gameScore += Mathf.FloorToInt(timer) * 40;
				break;

			case "hard":
				application.GetComponent<ApplicationManager>().gameScore += Mathf.FloorToInt(timer) * 60;
				break;
		}
	}

    private void MoveToNextLevel()
    {
		actualLevel++;
		
		if (actualLevel < application.GetComponent<ApplicationManager>().totalLevels)
        {
			StartCoroutine(CreateLevel(1.0f));
		}
		else
		{
			CheckTotalCorrectAnswers();
		}
	}

    private void CheckTotalCorrectAnswers()
    {
		if (numberOfCorrectAnswers == application.GetComponent<ApplicationManager>().totalLevels)
        {
			StartCoroutine(EndGame(1.0f, "Win"));
		}
        else
        {
			StartCoroutine(EndGame(1.0f, "Lose"));
		}
    }

	private IEnumerator EndGame(float delay, string status)
    {
		yield return new WaitForSeconds(delay);

		application.GetComponent<ApplicationManager>().result = status;

		ResetQuiz();
		levelCompleted = true;

		levelLoader.FadeToLevel();
	}

    private void ResetQuiz()
    {
		levelCompleted = false;

        switch (application.GetComponent<ApplicationManager>().GetGameMode())
        {
			case "easy":
				timer = application.GetComponent<ApplicationManager>().levelTime;
				break;

			case "medium":
				timer = application.GetComponent<ApplicationManager>().levelTime * 0.5f;
				break;

			case "hard":
				timer = application.GetComponent<ApplicationManager>().levelTime * 0.25f;
				break;
		}

		questionText.text = "";
		answer1Text.text = "";
		answer2Text.text = "";
		answer3Text.text = "";
		answer4Text.text = "";

		answer1Img.color = answer2Img.color = answer3Img.color = answer4Img.color = Color.white;
	}

}
