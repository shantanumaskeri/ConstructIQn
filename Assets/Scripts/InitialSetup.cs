using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSetup : MonoBehaviour
{
    
    private void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Application"));
        SceneManager.LoadScene("Screensaver");
    }

}
