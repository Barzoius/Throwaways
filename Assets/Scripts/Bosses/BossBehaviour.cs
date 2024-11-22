using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

    public bool playerPresent = false;

    GameObject player;
    private Animator animator;
    private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();       
        animator.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPresent && !animator.enabled)
        {
            animator.enabled = true; 
        }
        else if (!playerPresent && animator.enabled)
        {
            animator.enabled = false; 
        }
    }
}
