using UnityEngine;
using System.Collections;

public class Fireball: MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D coll)
    {
        var hit = coll.gameObject;

        if (hit.name == "Player_Knight" || hit.name == "Player_Knight(Clone)")
        {
            // set Knight on fire
            KnightActions knightActions = hit.GetComponent<KnightActions>();
            knightActions.catchFire();
            Debug.Log("Knight is hit");
        }

        Destroy(gameObject);
    }
}