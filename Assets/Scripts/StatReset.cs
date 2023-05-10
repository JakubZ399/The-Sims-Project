using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatReset : MonoBehaviour
{
    NavMeshAgent playerAI;

    public GameObject hungerResetObject;
    public GameObject sleepResetObject;
    public GameObject funResetObject;
    public float playerSpeed;

    private Vector3 hungerResetObjectPosition;
    private Vector3 sleepResetObjectPosition;
    private Vector3 funResetObjectPosition;

    bool isWalking;

    private void Start()
    {
        //bool
        isWalking = false;

        playerAI = GetComponent<NavMeshAgent>();
        hungerResetObjectPosition = hungerResetObject.transform.position;
        sleepResetObjectPosition = sleepResetObject.transform.position;
        funResetObjectPosition = funResetObject.transform.position;
    }
    private void Update()
    {
        Debug.Log(isWalking);
        if (PlayerStat.sleepStatInt <= 80 && isWalking == false)
        {
            playerAI.SetDestination(sleepResetObjectPosition);
            isWalking = true;
        }
        else if (PlayerStat.hungerStatInt <= 80 && isWalking == false)
        {
            playerAI.SetDestination(hungerResetObjectPosition);
            isWalking = true;
        }
        else if (PlayerStat.funStatInt <= 80 && isWalking == false)
        {
            playerAI.SetDestination(funResetObjectPosition);
            isWalking = true;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SleepReset")
        {
            PlayerStat.sleepStat = 100f;
            isWalking = false;
        }
        if (other.gameObject.tag == "HungerReset")
        {
            PlayerStat.hungerStat = 100f;
            isWalking = false;
        }
        if (other.gameObject.tag == "FunReset")
        {
            PlayerStat.funStat = 100f;
            isWalking = false;
        }
    }
}
