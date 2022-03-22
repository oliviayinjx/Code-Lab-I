using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    //access from player script
    //coroutine variable
    Coroutine firingCoroutine;
    //shooter sequence
    [SerializeField] float firingRate = 0.2f;
    public bool isFiring;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    //start of coroutine
    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            //use coroutine variable to store the status
            firingCoroutine = StartCoroutine(FireContinuously());
            
        }
        else if (!isFiring && firingCoroutine != null)
        {
            //because we don't want to stop all coroutine
            //so store the variable to sepecific coroutine for stopping that
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously() {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                            transform.position,
                                            Quaternion.identity);
            //reference to rigidbody
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            //give projectile uping force
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
