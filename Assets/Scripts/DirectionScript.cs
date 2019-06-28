using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectionScript : MonoBehaviour
{
    public enum GameObjectDirection { Up, Down, Left, Right };



    public GameObjectDirection gameobjectdir;

    private SpriteRenderer sr;

    public void SetDirection(GameObjectDirection dir)
    {
        gameobjectdir = dir;

        sr = GetComponent<SpriteRenderer>();
    }

    public GameObjectDirection ReturnDirection()
    {
        return gameobjectdir;
    }

    public void DirectionLeft(GameObject obj)
    {
        /*
        if (gameobjectdir != GameObjectDirection.Left)
        {
            obj.transform.Rotate(new Vector3(0f, 0f, 90f), Space.World);
            gameobjectdir = GameObjectDirection.Left;
        }*/
        if (gameobjectdir == GameObjectDirection.Right)
        {
            obj.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            gameobjectdir = GameObjectDirection.Left;
        }
    }

    public void DirectionRight(GameObject obj)
    {

        if (gameobjectdir == GameObjectDirection.Left)
        {
            obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            gameobjectdir = GameObjectDirection.Right;
        }
    }

    public void DragonSingleDirection(Sprite SingleDirection, GameObjectDirection new_dir)
    {
        sr.sprite = SingleDirection;
        if (new_dir == GameObjectDirection.Left)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localScale = new Vector3(-1.0f, -1.0f, 1.0f);
        }
        else if (new_dir == GameObjectDirection.Right)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (new_dir == GameObjectDirection.Up)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (new_dir == GameObjectDirection.Down)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            transform.localScale = new Vector3(-1.0f, -1.0f, 1.0f);
        }
        
    }

    public void InheritDirectionandRotation(Sprite past_sprite, Vector3 scale, Vector3 angle, GameObjectDirection dir)
    {
        sr.sprite = past_sprite;
        transform.eulerAngles = angle;
        transform.localScale = scale;
        gameobjectdir = dir;
    }

    public void DragonCurveDirection(Sprite Curve, Sprite Curve_1, GameObjectDirection god)
    {
        sr.sprite = Curve;

        if (gameobjectdir == GameObjectDirection.Right)
        {
            if (god == GameObjectDirection.Up)
            {
                sr.sprite = Curve_1;

                transform.eulerAngles = new Vector3(0, 0, 180);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
                //Debug.Log("Right + Up");

            }
            else if (god == GameObjectDirection.Down)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

               // Debug.Log("Right + Down");
            }
        }
        else if (gameobjectdir == GameObjectDirection.Left)
        {
            if (god == GameObjectDirection.Up)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

                //Debug.Log("Left + Up");
            }
            else if (god == GameObjectDirection.Down)
            {

                sr.sprite = Curve_1;
                transform.eulerAngles= new Vector3(0, 0, 0);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

                //Debug.Log("Left + Down");
            }
        }
        else if (gameobjectdir == GameObjectDirection.Up)
        {
            if (god == GameObjectDirection.Right)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                //Debug.Log("Up + Right");
            }
            else if (god == GameObjectDirection.Left)
            { 
                sr.sprite = Curve_1;
                transform.eulerAngles = new Vector3(0, 0, -90);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

               // Debug.Log("Up + Left");
            }

        }
        else if (gameobjectdir == GameObjectDirection.Down)
        {
            if (god == GameObjectDirection.Right)
            {

                sr.sprite = Curve_1;

                transform.eulerAngles = new Vector3(0, 0, 90);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

                //Debug.Log("Down + Right");
            }
            else if (god == GameObjectDirection.Left)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

                //Debug.Log("Down + Left");
            }
        }

        gameobjectdir = god;


    }

}
