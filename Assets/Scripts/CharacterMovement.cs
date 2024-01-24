using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed, rotationSpeed;
    [SerializeField] private GameObject spotted, backstab;
    Vector3 input, eForward, pForward, playerPosition;
    Rigidbody rb;
    GameObject target;
    Animator animator;
    bool stabEnabled;
   
    
    public static CharacterMovement instance;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
     
    }

    
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<EnemyScript>().SeeSomeone())
            {
                spotted.GetComponent<Animator>().SetBool("Spotted", true);
                spotted.GetComponent<Animator>().SetBool("NonSpotted", false);
                backstab.SetActive(false);
                stabEnabled = false;
                return;
                
            }
            else
            {
                spotted.GetComponent<Animator>().SetBool("Spotted", false);
                spotted.GetComponent<Animator>().SetBool("NonSpotted", true);
                backstab.SetActive(true);
                stabEnabled = true;
            }

        }

        if (target)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > 1.5)
            {
                backstab.GetComponent<Animator>().SetBool("Backstab", false);
                backstab.GetComponent<Animator>().SetBool("NonBackstab", true);
                stabEnabled = false;
            }

            if (Vector3.Dot(transform.forward, target.transform.forward) < 0)
            {
                backstab.GetComponent<Animator>().SetBool("Backstab", false);
                backstab.GetComponent<Animator>().SetBool("NonBackstab", true);
                stabEnabled = false;
            }
        }

        if(stabEnabled)
        {
            if (!target)
            {
                backstab.GetComponent<Animator>().SetBool("Backstab", false);
                backstab.GetComponent<Animator>().SetBool("NonBackstab", true);
            }
            else
            {
                Debug.Log(target.name);
                backstab.GetComponent<Animator>().SetBool("Backstab", true);
                backstab.GetComponent<Animator>().SetBool("NonBackstab", false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Attack");
                Destroy(target);
            }
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 desiredRotation = new Vector3(0, input.x * rotationSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(desiredRotation));

        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
        rb.MovePosition(transform.position + movement);
    }

    public void Stab(GameObject targetEnemy)
    {
        target = targetEnemy;
    }
}
