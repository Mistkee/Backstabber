using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] GameObject player;
    Vector3 eForward, pForward, playerPosition;
    float playerDistance;
    bool seeSomeone;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        eForward = transform.forward;
        pForward = player.transform.forward;
        playerPosition = player.transform.position - transform.position;
        playerDistance = playerPosition.magnitude;

        if (Vector3.Dot(eForward, playerPosition) > 0)
        {
            seeSomeone = true;
        }
        else
        {
            seeSomeone = false;
        }

        if (Vector3.Dot(eForward, playerPosition) < 0)
        {
            if (Vector3.Dot(eForward, pForward) > 0)
            {
                
                if (playerDistance < maxDistance)
                {
                    CharacterMovement.instance.Stab(gameObject);
                }
            }
        }
        
    }

    public bool SeeSomeone()
    {
        return seeSomeone;
    }
}
