using UnityEngine;

public class AudioPlayer : MonoBehaviour 
{
	
	public static AudioPlayer Instance;

	public AudioClip gazeSfx;
	public AudioClip selectionSfx;

	private AudioSource audioSrc;

	private bool isPlayingGazeSfx;
	private bool isPlayingSelectionSfx;

	private void Start() 
	{
		Instance = this;

		audioSrc = GetComponent<AudioSource>();

		ResetAudio();
	}
	
	public void CheckForGazeAudio()
	{
		if (Instance != null)
		{
			StopGazeAudio ();	
		}	
	}

	public void PlayGazeAudio()
	{
		if (!isPlayingGazeSfx)
		{
			audioSrc.clip = gazeSfx;
			audioSrc.Play();

			isPlayingGazeSfx = true;
		}
	}

	public void StopGazeAudio()
	{
		if (isPlayingGazeSfx)
		{
			audioSrc.Stop ();

			isPlayingGazeSfx = false;
		}
	}

	public void CheckForSelectionAudio()
	{
		if (Instance != null)
		{
			StopSelectionAudio ();	
		}	
	}

	public void PlaySelectionAudio()
	{
		if (!isPlayingSelectionSfx)
		{
			audioSrc.clip = selectionSfx;
			audioSrc.Play();

			isPlayingSelectionSfx = true;
		}
	}

	public void StopSelectionAudio()
	{
		if (isPlayingSelectionSfx)
		{
			audioSrc.Stop();

			isPlayingSelectionSfx = false;
		}
	}

	public void ResetAudio()
	{
		isPlayingGazeSfx = isPlayingSelectionSfx = false;
	}

}
