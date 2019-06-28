using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public DirectionScript GameObjectDir;

    private SpriteRenderer sr;

    public Sprite fire; 

    public bool alive;

    private bool carryingitem;

    private GameObject attachedfood = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObjectDir = gameObject.AddComponent<DirectionScript>();
        GameObjectDir.SetDirection(DirectionScript.GameObjectDirection.Right);
        sr = GetComponent<SpriteRenderer>();
        alive = true;
        carryingitem = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            InputManagement();
        }
        if (attachedfood == null)
        {
            carryingitem = false;
        }
    }

    private void InputManagement()
    {
        if(Input.GetKey("w"))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0f, 1.0f, 0), 0.25f);

        }
        else if (Input.GetKey("a"))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-1.00f, 0.0f, 0), 0.25f);
            GameObjectDir.DirectionLeft(gameObject);
        }
        else if (Input.GetKey("s"))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0f, -1.0f, 0), 0.25f);
        }
        else if (Input.GetKey("d"))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1.0f, 0.0f, 0), 0.25f);
            GameObjectDir.DirectionRight(gameObject);
        }
        else
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive)
        {
            alive = false;
            sr.sprite = fire;
            GetComponent<AudioSource>().Play();
            GameObject.Find("Game_Manager").GetComponent<GameManagerScript>().EndGame();
            GameObject.Find("Fire").GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(alive)
        {
            if (collision.gameObject.tag == "Food")
            {
                if (carryingitem == false)
                {
                    carryingitem = true;
                    collision.gameObject.GetComponent<FoodScript>().AttachedtoPlayer(transform.gameObject);
                    attachedfood = collision.gameObject;
                    collision.isTrigger = false; 
                }
                else
                {

                }
            }
        }
    }

    
}
