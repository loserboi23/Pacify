using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    public int health;

    public RectTransform HungerBar;

    public GameObject FoodPrefab;

    private int max_x, max_y, min_x, min_y;

    private List<Vector3> InvalidPositions = new List<Vector3>();

    private GameObject[] Dragon;
    private GameObject[] Food;
        
    private DragonAIScript DragonAIScript_;

    private int max_health;

    private readonly float width = 800;

    private GameObject Transition;
    private SceneTransitionScript SceneTransition;

    public GameObject dragonhead;
    public Vector3 Respawn; 

    
    void Start()
    {
        max_x = 7;
        max_y = 3;
        min_x = -8;
        min_y = -3;

        DragonAIScript_ = GameObject.Find("dragonhead").GetComponent<DragonAIScript>();

        Transition = GameObject.Find("TransitionCanvas");
        SceneTransition = Transition.GetComponent<SceneTransitionScript>();

        max_health = DragonAIScript_.health;

        InvokeRepeating("SpawnFood", 2f, 2f);

        StartCoroutine("StartLevel");

    }


    // Update is called once per frame
    void Update()
    {
        UpdateHungerUI();

    }


    private void RefreshInvalidPositions()
    {
        Dragon = GameObject.FindGameObjectsWithTag("Dragon");
        Food = GameObject.FindGameObjectsWithTag("Food");
        InvalidPositions.Clear();

        for (int i = 0; i < Dragon.Length; i++)
        {
            InvalidPositions.Add(Dragon[i].transform.position);
        }

        for (int i = 0; i < Food.Length; i++)
        {
            InvalidPositions.Add(Food[i].transform.position);
        }

        InvalidPositions.Add(GameObject.FindGameObjectWithTag("Player").transform.position);



    }


    private void SpawnFood()
    {
        Food = GameObject.FindGameObjectsWithTag("Food");

        if (Food.Length < 5)
        {
            RefreshInvalidPositions();
            float x = Random.Range(min_x, max_x + 1);
            float y = Random.Range(min_y, max_y + 1);
            x += 0.5f;
            y += 0.5f;
            Instantiate(FoodPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
        else
        {

        }

    }

    private void UpdateHungerUI()
    {
        health = DragonAIScript_.health;
        float percentage = health / (float)max_health;
        float x = width * percentage;
        HungerBar.localPosition = new Vector3(0, -x, 0);


        if (health == 0)
        {
            //StartCoroutine("EndLevel");
            DragonAIScript_.isFed = true;

            int temp1 = max_health;
            int temp2 = DragonAIScript_.dragonlength;

            GameObject obj = Instantiate(dragonhead, Respawn, Quaternion.identity);
            obj.name = "dragonhead";
            DragonAIScript_ = obj.GetComponent<DragonAIScript>();
            DragonAIScript_.dragonlength = temp2 + 1;
            DragonAIScript_.health = temp1 + 10;   
            max_health = DragonAIScript_.health;
            health = DragonAIScript_.health;

            GameObject.Find("DontDestroyOnLoad").GetComponent<ScoreLevelScript>().RaiseLevel();

        }

        if (Input.GetKeyDown("k"))
        {
            DragonAIScript_.health = 0;
        }
    }

    public void EndGame()
    {
        StartCoroutine("Lose");
    }


    //Corrutines



    IEnumerator StartLevel()
    {
        SceneTransition.TransitionOut();
        yield return null; 
    }


    IEnumerator Lose()
    {
        SceneTransition.TransitionIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LoserScene");
    }


}
