using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class XMLParser : MonoBehaviour
{

	public GameObject randomNumber;
	public TextAsset inFile;

    [HideInInspector]
	public List<Dictionary<string,string>> game;
	
	private Dictionary<string,string> quizDetails;
	private GameObject application;

	private void Start()
	{
		game = new List<Dictionary<string, string>>();
		application = GameObject.Find("Application");

		LoadXML();
	}

	private void LoadXML()
    {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(inFile.text);

		ParseXML(xmlDoc);
	}

	private void ParseXML(XmlDocument document)
	{
		XmlNodeList questionnaireList = document.GetElementsByTagName("questionnaire");
	
		foreach (XmlNode questionnaireInfo in questionnaireList)
		{
			XmlNodeList quiz = questionnaireInfo.ChildNodes;
			quizDetails = new Dictionary<string, string>();

			foreach (XmlNode quizItems in quiz)
			{
				quizDetails.Add(quizItems.Name, quizItems.InnerText);
			}

			game.Add(quizDetails);
		}

		application.GetComponent<ApplicationManager>().totalQuestions = application.GetComponent<ApplicationManager>().totalLevels = game.Count;
		//Debug.Log(application.GetComponent<ApplicationManager>().totalLevels + " : " + application.GetComponent<ApplicationManager>().totalQuestions);

		/*XmlNodeList levelList = document.GetElementsByTagName("levels");
		foreach (XmlNode levelInfo in levelList)
		{
			XmlNodeList totalLevels = levelInfo.ChildNodes;

			foreach (XmlNode levelItem in totalLevels)
			{
				application.GetComponent<ApplicationManager>().totalLevels = int.Parse(levelItem.InnerText);
			}
		}*/

		XmlNodeList timeList = document.GetElementsByTagName("time");
		foreach (XmlNode timeInfo in timeList)
		{
			XmlNodeList totalTime = timeInfo.ChildNodes;

			foreach (XmlNode timeItem in totalTime)
			{
				application.GetComponent<ApplicationManager>().levelTime = float.Parse(timeItem.InnerText);
			}
		}
		
		randomNumber.transform.SendMessage("Init");
	}

}