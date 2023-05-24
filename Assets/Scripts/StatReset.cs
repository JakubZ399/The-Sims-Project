using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatReset : MonoBehaviour
{
    NavMeshAgent playerAI;

    public Animator animator;

    public GameObject hungerResetObject;
    public GameObject sleepResetObject;
    public GameObject funResetObject;

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

        GetResetPointPos();
    }

    private void Update()
    {
        GoToResetPoint();
    }

    private void OnTriggerStay(Collider other)
    {
        ResetStat(other);
    }



    private void GetResetPointPos()
    {
        hungerResetObjectPosition = hungerResetObject.transform.position;
        sleepResetObjectPosition = sleepResetObject.transform.position;
        funResetObjectPosition = funResetObject.transform.position;
    }

    private void GoToResetPoint()
    {
        if (PlayerStat.sleepStatInt <= statMin && isWalking == false)
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
            return;
        }
    }

    private void ResetStat(Collider other)
    {
        //1.3 - sleep 
        //1 - hunger
        //1 - fun

        distanceToHunger = Vector3.Distance(gameObject.transform.position, hungerResetObjectPosition);
        distanceToSleep = Vector3.Distance(gameObject.transform.position, sleepResetObjectPosition);
        distanceToFun = Vector3.Distance(gameObject.transform.position, funResetObjectPosition);

        //add distance and animation
        if (other.gameObject.tag == "HungerReset")
        {
            StartCoroutine(WaitForHunger(waitForHunger));
            PlayerStat.isHungerTick = false;
        }
        if (other.gameObject.tag == "SleepReset")
        {
            StartCoroutine(WaitForSleep(waitForSleep));
            PlayerStat.isSleepTick = false;
        }
        if (other.gameObject.tag == "FunReset")
        {
            StartCoroutine(WaitForFun(waitForFun));
            PlayerStat.isFunTick = false;
        }
    }

    private IEnumerator WaitForHunger(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.hungerStat = 100f;
        PlayerStat.isHungerTick = true;
        isWalking = false;
        yield break;
    }
    private IEnumerator WaitForSleep(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.sleepStat = 100f;
        PlayerStat.isSleepTick = true;
        isWalking = false;
        yield break;
    }

    private IEnumerator WaitForFun(float wait)
    {
        yield return new WaitForSeconds(wait);
        PlayerStat.funStat = 100f;
        PlayerStat.isFunTick = true;
        isWalking = false;
        yield break;
    }
}
