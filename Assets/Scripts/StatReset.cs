using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatReset : MonoBehaviour
{
    private LightScript lightScript;
    
    NavMeshAgent playerAI;

    public Animator animator;

    public GameObject hungerResetObject;
    public GameObject sleepResetObject;
    public GameObject funResetObject;

    public Transform funLookAt;

    public float playerSpeed;
    public float statMin = 90f;

    public float waitForSleep = 2f;
    public float waitForHunger = 2f;
    public float waitForFun = 2f;

    private float distanceToHunger;
    private float distanceToSleep;
    private float distanceToFun;

    private Vector3 hungerResetObjectPosition;
    private Vector3 sleepResetObjectPosition;
    private Vector3 funResetObjectPosition;

    bool isWalking;

    private void Start()
    {
        //bool
        isWalking = false;

        //getcomponent
        playerAI = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lightScript = GetComponent<LightScript>();

        GetResetPointPos();
    }

    private void Update()
    {
        GoToResetPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetStat(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HungerReset")
        {
            animator.SetBool("isPraying", false);
            lightScript.LightOff();
        }
        if(other.gameObject.tag == "SleepReset")
        {
            animator.SetBool("isSleeping", false);
        }
        if(other.gameObject.tag == "FunReset")
        {
            animator.SetBool("isSitting", false);
        }
    }


    private void GetResetPointPos()
    {
        hungerResetObjectPosition = hungerResetObject.transform.position;
        sleepResetObjectPosition = sleepResetObject.transform.position;
        funResetObjectPosition = funResetObject.transform.position;
    }

    private void GoToResetPoint()
    {
        /*if (PlayerStat.sleepStatInt <= statMin && isWalking == false)
        {
            playerAI.SetDestination(sleepResetObjectPosition);
            isWalking = true;
        }
        else if (PlayerStat.hungerStatInt <= statMin && isWalking == false)
        {
            playerAI.SetDestination(hungerResetObjectPosition);
            isWalking = true;
        }
        else if (PlayerStat.funStatInt <= statMin && isWalking == false)
        {
            playerAI.SetDestination(funResetObjectPosition);
            isWalking = true;
        }
        else
        {
            playerAI.SetDestination(funResetObjectPosition);
            isWalking = true;
        }
        */
        
        
        //DEBUG
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerAI.SetDestination(sleepResetObjectPosition);
            isWalking = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerAI.SetDestination(hungerResetObjectPosition);
            isWalking = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            playerAI.SetDestination(funResetObjectPosition);
            isWalking = true;
        }
    }

    private void ResetStat(Collider other)
    {
        if (other.gameObject.tag == "HungerReset")
        {
            PlayerStat.isHungerTick = false;
            animator.SetBool("isPraying", true);
            
            lightScript.LightOn();
            
            StartCoroutine(WaitForHunger(waitForHunger));
        }
        if(other.gameObject.tag == "SleepReset")
        {
            PlayerStat.isSleepTick = false;
            animator.SetBool("isSleeping", true);
            StartCoroutine(WaitForSleep(waitForSleep));
        }
        if(other.gameObject.tag == "FunReset")
        {
            PlayerStat.isFunTick = false;
            playerAI.enabled = false;
            animator.SetBool("isSitting", true);
            gameObject.transform.LookAt(funLookAt);
            StartCoroutine(WaitForFun(waitForFun));
        }
    }

    private IEnumerator WaitForHunger(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.hungerStat = 100f;
        PlayerStat.isHungerTick = true;
        isWalking = false;
    }
    private IEnumerator WaitForSleep(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.sleepStat = 100f;
        PlayerStat.isSleepTick = true;
        isWalking = false;
    }

    private IEnumerator WaitForFun(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.funStat = 100f;
        PlayerStat.isFunTick = true;
        playerAI.enabled = true;
        isWalking = false;
    }
}
