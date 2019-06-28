using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreLevelScript : MonoBehaviour
{
    public int level = 1;
    public int score = 0;

    private static ScoreLevelScript instance;



    private TextMeshProUGUI Score;
    private TextMeshProUGUI LevelIndicator; 



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; 
        }

        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LoserScene"))
        {
            GameObject.Find("Score").GetComponent<TextMeshProUGUI>().SetText(score.ToString());

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
            {
                GameObject.Find("LevelIndicator").GetComponent<TextMeshProUGUI>().SetText(level.ToString());
            }
           
        }
    }

    public void RaiseLevel()
    {
        level += 1;
    }

    public void RaiseScore()
    {
        score += 100; 
    }

    public void LowerScore()
    {
        score -= 50;
    }
    
    public void ResetScore()
    {
        level = 1;
        score = 0; 
    }
}
