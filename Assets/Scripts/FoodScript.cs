using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public enum State { Good, Bad};

    private bool attached = false;

    private GameObject player_follow;

    private State statefood;

    public Sprite goodfood;
    public Sprite badfood;

    private int maxhealth;
    bool collide; 

    // Start is called before the first frame update
    void Start()
    {
        collide = false;
        maxhealth = GameObject.Find("dragonhead").GetComponent<DragonAIScript>().health;

        int random = Random.Range(0, 3);
        
        if (random == 0)
        {
            statefood = State.Bad;
            GetComponent<SpriteRenderer>().sprite = badfood;

            
        }
        else
        {
            statefood = State.Good;
            GetComponent<SpriteRenderer>().sprite = goodfood;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(attached == true)
        {
            if(player_follow.GetComponent<DirectionScript>().ReturnDirection() == DirectionScript.GameObjectDirection.Left)
            {
                transform.position = player_follow.transform.position + new Vector3(-1.00f, -.30f, 0f);
            }
            else
            {
                transform.position = player_follow.transform.position + new Vector3(1.00f, -.30f, 0f);
            }
        }
    }

    public void AttachedtoPlayer(GameObject player)
    {
        attached = true;
        player_follow = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fire" || collision.gameObject.tag == "Dragon")
        {
            Destroy(transform.gameObject);

            if (collision.gameObject.tag == "Dragon" && collide == false)
            {
                collide = true;
                Transform temp = collision.gameObject.transform.parent;
                temp = temp.Find("dragonhead");

                if (statefood == State.Good)
                {
                    //Find DragonHead
                    if (temp)
                    {

                        if (temp.gameObject.GetComponent<DragonAIScript>().isFed == false)
                        {
                            temp.gameObject.GetComponent<DragonAIScript>().health -= 10;
                            GameObject.Find("DontDestroyOnLoad").GetComponent<ScoreLevelScript>().RaiseScore();
                            temp.gameObject.GetComponent<DragonAIScript>().PlayAudioClip("Bite");
                        }
                    }
                    else
                    {
                        Debug.Log("Did not find");
                        Debug.Log(collision.gameObject.name);
                       
                    }
                    
                }
                else
                {
                    if (maxhealth == GameObject.Find("dragonhead").GetComponent<DragonAIScript>().health)
                    {
                        temp.gameObject.GetComponent<DragonAIScript>().PlayAudioClip("Roar");
                    }
                    else
                    {
                        GameObject.Find("dragonhead").GetComponent<DragonAIScript>().health += 10;
                        GameObject.Find("DontDestroyOnLoad").GetComponent<ScoreLevelScript>().LowerScore();
                        temp.gameObject.GetComponent<DragonAIScript>().PlayAudioClip("Roar");
                    }
                       
                }
            }

            if (collision.gameObject.tag == "Fire")
            {
                collision.gameObject.GetComponent<AudioSource>().Play();
            }

        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        collide = false; 
    }

}
