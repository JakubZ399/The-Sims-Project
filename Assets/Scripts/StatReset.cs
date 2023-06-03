using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatReset : MonoBehaviour
{
    private LightScript lightScript;
    private NavMeshAgent playerAI;
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip sleepAudio;
    public AudioClip hungryAudio;
    public AudioClip funAudio;

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

    private Vector3 currentPos;

    bool isWalking;

    private void Start()
    {
        //bool
        isWalking = false;

        //getcomponent
        playerAI = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lightScript = GetComponent<LightScript>();
        audioSource = GetComponent<AudioSource>();

        GetResetPointPos();
    }

    private void Update()
    {
        GoToResetPoint();
        currentPos = gameObject.transform.position;
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
        if (PlayerStat.sleepStatInt <= statMin)
        {
            playerAI.SetDestination(sleepResetObjectPosition);
        }
        else if (PlayerStat.hungerStatInt <= statMin)
        {
            playerAI.SetDestination(hungerResetObjectPosition);
        }
        else if (PlayerStat.funStatInt <= statMin)
        {
            playerAI.SetDestination(funResetObjectPosition);
        }


        /*//DEBUG
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
        }*/
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

        audioSource.clip = hungryAudio;
        audioSource.Play();
    }
    private IEnumerator WaitForSleep(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.sleepStat = 100f;
        PlayerStat.isSleepTick = true;

        audioSource.clip = sleepAudio;
        audioSource.Play();
    }

    private IEnumerator WaitForFun(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.funStat = 100f;
        PlayerStat.isFunTick = true;
        playerAI.enabled = true;

        audioSource.clip = funAudio;
        audioSource.Play();
    }
}
