using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //coroutines allow for greater control over when functions are executed
        //for games this is very important as many action can be much more
        //reliant on timing than on events
        //typically activate once per frame
        StartCoroutine(SelfDestruct());
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SelfDestruct()
    {
        //yield allows this enumerating method to iterate like a foreach loop
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
