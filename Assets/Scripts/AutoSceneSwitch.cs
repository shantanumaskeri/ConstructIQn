using System.Collections;
using UnityEngine;

public class AutoSceneSwitch : MonoBehaviour
{

    public LevelLoader levelLoader;
    public float delay = 3.0f;
    
    private void Start()
    {
        StartCoroutine(MoveToNextScreen(delay));
    }

    private IEnumerator MoveToNextScreen(float delay)
    {
        yield return new WaitForSeconds(delay);

        DontDestroyOnLoad(GameObject.Find("Application"));

        StopCoroutine(MoveToNextScreen(delay));

        levelLoader.FadeToLevel();
    }
    
}
