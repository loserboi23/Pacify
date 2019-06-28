using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAIScript : MonoBehaviour
{
    public int dragonlength;
    public int health;
    public GameObject dragonbody;
    public GameObject dragontail;

    public Sprite Sprite_dragon_head;
    public Sprite Sprite_dragonbody;
    public Sprite Sprite_dragoncurve;
    public Sprite Sprite_dragoncurve_1;
    public Sprite Sprite_dragontail;


    private DirectionScript GameObjectDir;


    public List<GameObject> dragonlist = new List<GameObject>();


    private List<Sprite> spriteclone = new List<Sprite>();
    private List<Vector3> scaleclone = new List<Vector3>();
    private List<Vector3> eulerclone = new List<Vector3>();

    private List<DirectionScript.GameObjectDirection> directionclone = new List<DirectionScript.GameObjectDirection>();

    private List<Vector3> newpositions = new List<Vector3>();
    private Vector3 target_offset;

    public bool isFed;

    private Transform List;

    public AudioClip Roar, Bite;

    private bool colorchange = false;


    // Start is called before the first frame update
    void Start()
    {
        isFed = false;
        GameObjectDir = GetComponent<DirectionScript>();
        GameObjectDir.SetDirection(DirectionScript.GameObjectDirection.Right);
        dragonlist.Add(gameObject);



        GameObject empty;
        empty = new GameObject("DragonList");
        gameObject.transform.parent = empty.transform;
        List = empty.transform;

        CreateDragon();
        StartCoroutine(PositionUpdating());


    }

    // Update is called once per frame
    void Update()
    {
        if (isFed)
        {
            if (colorchange == false)
            {
                foreach (GameObject dragonpart in dragonlist)
                {
                    dragonpart.GetComponent<SpriteRenderer>().color = new Color(150, 25, 25);
                }
                colorchange = true;
            }

            if (transform.position.y > 35f || transform.position.y < -35f || transform.position.x > 35f || transform.position.x < -35f)
            {
                foreach (GameObject dragonpart in dragonlist)
                {
                    Destroy(dragonpart);
                }
            }
        }
    }

    void CreateDragon()
    {
        GameObject obj;
        for (int i = 0; i < dragonlength; i++)
        {
            if (i != dragonlength - 1)
                obj = Instantiate(dragonbody, transform.position + new Vector3(-1.0f * (i + 1f), 0f, 0f), Quaternion.identity);
            else
                obj = Instantiate(dragontail, transform.position + new Vector3(-1.0f * (i + 1f), 0f, 0f), Quaternion.identity);

            GameObjectDir = obj.GetComponent<DirectionScript>();
            GameObjectDir.SetDirection(DirectionScript.GameObjectDirection.Right);
            dragonlist.Add(obj);
            obj.transform.parent = List;
        }
    }

    DirectionScript.GameObjectDirection ChooseDirection()
    {
        int random = Random.Range(0, 2);

        if ((gameObject.GetComponent<DirectionScript>().ReturnDirection() == DirectionScript.GameObjectDirection.Left) || (gameObject.GetComponent<DirectionScript>().ReturnDirection() == DirectionScript.GameObjectDirection.Right))
        {
            if (transform.position.y > 5.0f)
            {
                return DirectionScript.GameObjectDirection.Down;
            }
            else if (transform.position.y < -5.0)
            {
                return DirectionScript.GameObjectDirection.Up;
            }
            else if (random == 0)
            {
                return DirectionScript.GameObjectDirection.Up;
            }
            else
            {
                return DirectionScript.GameObjectDirection.Down;
            }
        }
        else
        {
            if (transform.position.x > 9.0f)
            {
                return DirectionScript.GameObjectDirection.Left;
            }
            else if (transform.position.x < -9.0)
            {
                return DirectionScript.GameObjectDirection.Right;
            }
            else if (random == 0)
            {
                return DirectionScript.GameObjectDirection.Left;
            }
            else
            {
                return DirectionScript.GameObjectDirection.Right;
            }
        }
    }
    
    IEnumerator PositionUpdating()
    {
        while (true)
        {
            DirectionScript.GameObjectDirection next_dir = ChooseDirection();
            int random;

            if (!isFed)
            {
                random = Random.Range(4, 10);
            }
            else
            {
                random = 100;
            }

            if (next_dir == DirectionScript.GameObjectDirection.Left || next_dir == DirectionScript.GameObjectDirection.Right)
            {
                int x = 0;
                x = next_dir == DirectionScript.GameObjectDirection.Left ? -1 : 1;
                target_offset = new Vector3(x * 1.0f, 0.0f, 0);
            }
            else
            {
                int x = 0;
                x = next_dir == DirectionScript.GameObjectDirection.Down ? -1 : 1;
                target_offset = new Vector3(0, x * 1.0f, 0);
            }
            yield return StartCoroutine(DragonMovement(random, target_offset, next_dir));
            gameObject.GetComponent<DirectionScript>().SetDirection(next_dir);
            yield return new WaitForSeconds(0.65f);
        }

    }

    void UpdatePositionList(Vector3 target_offset)
    {
        newpositions.Clear();
        for (int i = 0; i < dragonlist.Count; i++)
        {
            if (i == 0)
            {
                newpositions.Add(dragonlist[i].transform.position + target_offset);
            }
            else
            {
                newpositions.Add(dragonlist[i - 1].transform.position);
            }
        }
    }
    
    void UpdateCloneList()
    {
        spriteclone.Clear();
        scaleclone.Clear();
        eulerclone.Clear();
        directionclone.Clear();
        for (int i = 0; i < dragonlist.Count; i++)
        {
            spriteclone.Add(dragonlist[i].GetComponent<SpriteRenderer>().sprite);
            scaleclone.Add(dragonlist[i].transform.localScale);
            eulerclone.Add(dragonlist[i].transform.eulerAngles);
            directionclone.Add(dragonlist[i].GetComponent<DirectionScript>().ReturnDirection());
        }
    }

    IEnumerator DragonMovement(int random, Vector3 target_offset, DirectionScript.GameObjectDirection new_dir)
    {
        for (int r = 0; r < random; r++)
        {
            UpdatePositionList(target_offset);
            DirectionScript GOD;

            for (int i = 0; i < dragonlist.Count; i++)
            {

                dragonlist[i].transform.position = Vector3.MoveTowards(dragonlist[i].transform.position, newpositions[i], 1f);

                yield return null;

                if (r == 0)
                {
                    GOD = dragonlist[i].GetComponent<DirectionScript>();
                    //turn the dragonhead and make a curve

                    if (i == 0)
                    {
                        UpdateCloneList();
                        GOD.DragonSingleDirection(Sprite_dragon_head, new_dir);
                    }
                    else if (i == 1)
                    {
                        GOD.DragonCurveDirection(Sprite_dragoncurve, Sprite_dragoncurve_1, new_dir);
                    }
                    else if (i == dragonlist.Count - 1)
                    {
                        GOD.InheritDirectionandRotation(Sprite_dragontail, scaleclone[i - 1], eulerclone[i - 1], directionclone[i - 1]);
                    }
                    else
                    {
                        GOD.InheritDirectionandRotation(spriteclone[i - 1], scaleclone[i - 1], eulerclone[i - 1], directionclone[i - 1]);
                    }
                }
                else
                {
                    GOD = dragonlist[i].GetComponent<DirectionScript>();
                    if (i == 0)
                    {
                        GOD.DragonSingleDirection(Sprite_dragon_head, new_dir);
                    }
                    else if (i == 1)
                    {
                        UpdateCloneList();
                        GOD.DragonSingleDirection(Sprite_dragonbody, new_dir);
                    }
                    else if (i == dragonlist.Count - 1)
                    {
                        GOD.DragonSingleDirection(Sprite_dragontail, directionclone[i - 1]);
                    }
                    else
                    {
                        GOD.InheritDirectionandRotation(spriteclone[i - 1], scaleclone[i - 1], eulerclone[i - 1], directionclone[i - 1]);
                    }
                }
            }

        }
    }

    public void PlayAudioClip(string Clip)
    {
        AudioSource audiosource = GetComponent<AudioSource>();
        AudioClip Change; 
        if (Clip == "Roar")
        {
            Change = Roar;
        }
        else if (Clip == "Bite")
        {
            Change = Bite;
        }
        else
        {
            Debug.Log("Wrong");
            return; 
        }
        audiosource.clip = Change;
        audiosource.Play();

    }
}
