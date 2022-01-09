using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GazeInput : MonoBehaviour 
{
	
	public static GazeInput Instance;

	public Transform reticle;
	public Image fillerCrosshair;
	public GameManager gameManager;

	private bool isActive = false;

	private void Start()
	{
		Instance = this;

		StartCoroutine(Initialize());
	}
		
	public IEnumerator Initialize()
	{
		yield return new WaitForSeconds(3.0f);

		isActive = true;
	}

	private void Update() 
	{
		CheckRaycastCollisions();
	}

	private void CheckRaycastCollisions()
	{
		Ray ray = new Ray (reticle.position, -reticle.forward);
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast (ray, out hit, Mathf.Infinity))
		{
			if (isActive)
			{
				if (fillerCrosshair.fillAmount < 1.0f)
				{
					fillerCrosshair.fillAmount += 0.01f;

					AudioPlayer.Instance.PlayGazeAudio();
				}
				else
				{
					gameManager.SubmitQuizAnswer(hit.collider.gameObject.name);
					
					fillerCrosshair.fillAmount = 0.0f;
					isActive = false;

					AudioPlayer.Instance.StopGazeAudio();
					AudioPlayer.Instance.PlaySelectionAudio();
					AudioPlayer.Instance.ResetAudio();
				}
			}
			else
			{
				fillerCrosshair.fillAmount -= 0.01f;

				if (fillerCrosshair.fillAmount <= 0.0f)
				{
					AudioPlayer.Instance.CheckForGazeAudio();
				}
			}
		}
		else
		{
			fillerCrosshair.fillAmount -= 0.01f;

			if (fillerCrosshair.fillAmount <= 0.0f)
			{
				AudioPlayer.Instance.CheckForGazeAudio();
			}
		}
	}

}
