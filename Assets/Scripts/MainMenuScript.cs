using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    private GameObject Transition;
    private SceneTransitionScript SceneTransition;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        Transition = GameObject.Find("TransitionCanvas");
        SceneTransition = Transition.GetComponent<SceneTransitionScript>();
        SceneTransition.TransitionOut();



        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("IntroScene"))
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentresolution = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentresolution = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentresolution;
            resolutionDropdown.RefreshShownValue();
        }
    }


    public void StartLevel()
    {
        StartCoroutine("StartGame");
    }

    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        SceneTransition.TransitionIn();
        yield return new WaitForSeconds(1f);
        GameObject.Find("DontDestroyOnLoad").GetComponent<ScoreLevelScript>().ResetScore();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
