using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatReset : MonoBehaviour
{
    NavMeshAgent playerAI;

    public GameObject hungerResetObject;
    public float playerSpeed;
    

    private Vector3 hungerResetObjectPosition;
    private Vector3 startingPosition;

    private void Start()
    {
        playerAI = GetComponent<NavMeshAgent>();

        startingPosition = transform.position;
        hungerResetObjectPosition = hungerResetObject.transform.position;
    }
    private void Update()
    {
        if(PlayerStat.hungerStatInt <= 99)
        {
            playerAI.SetDestination(hungerResetObjectPosition);
        }
    }
}
