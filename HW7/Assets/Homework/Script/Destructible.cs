using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Destructible")]
    private Destructible destructible;
    public bool OnCollision;
    public GameObject DestroyedPrefab;
    bool destroyed;
    

    public void ToDestroy()
    {
        //replace object after few second
        StartCoroutine(waterCoroutine());
    }

    IEnumerator waterCoroutine()
    {
        yield return new WaitForSeconds(0.7f);

        if (DestroyedPrefab && !destroyed)
        {
            //replace original with new destroy prefab
            GameObject i = Instantiate(DestroyedPrefab, transform.position, transform.rotation);
            i.transform.localScale = gameObject.transform.localScale;
            destroyed = true;
        }
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject);

    }
}
