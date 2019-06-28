using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionScript : MonoBehaviour
{
    private Animator animator;
    private static SceneTransitionScript instance;


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

        animator = GetComponentInChildren<Animator>();

    }

    public void TransitionIn()
    {
        animator.SetBool("LoadOut", true);
        animator.SetBool("LoadIn", false);
    }

    public void TransitionOut()
    {
        animator.SetBool("LoadOut", false);
        animator.SetBool("LoadIn", true);
    }
}
