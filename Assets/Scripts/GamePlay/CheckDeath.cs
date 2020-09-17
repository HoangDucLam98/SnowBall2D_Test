using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeath : MonoBehaviour
{
    public BallController parent;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
        {
            var position = collision.ClosestPoint(transform.position);
            var distance = Vector2.Distance(position, transform.position);

            if (distance <= 0.1f)
                parent.OnDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map"))
        {
            if (parent.body.velocity != Vector2.zero)
                parent.OnDeath();
        }
    }
}
