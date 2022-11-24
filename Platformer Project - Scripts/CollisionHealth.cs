using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHealth : MonoBehaviour
{
    public int collisionHealth = 2;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisionTag)
        {
            Moving health = coll.gameObject.GetComponent<Moving>();
            health.SetHealth(collisionHealth);
            Destroy(gameObject);
        }
    }
    

}