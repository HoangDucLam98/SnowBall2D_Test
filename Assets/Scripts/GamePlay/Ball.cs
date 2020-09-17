using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// [RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private float size;
    private float maxSize = 0.005f;
    public bool isReleased { get; private set; }
    private Rigidbody2D body;

    public GameObject frostEffect;

    BallController ballController;
    public LayerMask ground;
    public BallController objParent;

    public int idObject;

    //public Vector3 scaleBefore;
    public Transform faceBall;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // ballEffect = transform.GetChild(0);
        //idObject = GameControl.Instance.GetIndexOfObject(transform.parent.parent.transform.gameObject);
    }

    private void Update()
    {
        // ballEffect.transform.Rotate(0, 0, 15f);
        //if (size < maxSize && !isReleased)
        //{
        //    ChangeSize();
        //    // InsDistance();
        //}

        var hit = Physics2D.Raycast(transform.position, transform.up, 100f, ground);
        if (isOutOfGround(hit.point) && isReleased)
        {
            Destroy(gameObject, .25f);
        }

        Rolling();
    }

    private void FixedUpdate()
    {
        // if (size < maxSize && !isReleased)
        // {
        //     ChangeSize();
        //     InsDistance();
        // }
    }

    void Rolling()
    {
        faceBall.Rotate(15, 0, 0);
    }

    public void ChangeSize()
    {
        Vector3 tempp = transform.localScale;
        tempp.x += size * 2;
        tempp.y += size * 2;
        tempp.z += size * 2;
        transform.localScale = tempp;
        //scaleBefore = transform.localScale;
    }

    public void ReleaseBall(Vector2 direction)
    {
        if (isReleased)
        {
            body.AddForce(direction * 250f);
        }
    }

    public void inscreaseSize(float ins)
    {
        if (size < maxSize && !isReleased)
        {
            size += ins;
            ChangeSize();
            // InsDistance();
        }
    }

    public bool compareSize(float sizeBall)
    {
        return size > sizeBall;
    }

    public void setIsReleased()
    {
        isReleased = true;
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BallController otherBallController = other.transform.parent.gameObject.GetComponent<BallController>();
            if (idObject != otherBallController.infor.id)
            {
                PlayEffect();
                Destroy(this.gameObject);
                Vector2 direction = other.transform.position - transform.position;

                otherBallController.pushByBall(direction, size, idObject);

                // AudioManager.PlaySound(AudioManager.ballCollisionName);
                AudioManager1.Ins.PlaySound(SoundType.BallCollision);
            }
        }

        if (other.CompareTag("Ball"))
        {
            if (other.transform.localScale.x >= transform.localScale.x)
            {
                PlayEffect();
                Destroy(this.gameObject);
            }
            // AudioManager.PlaySound(AudioManager.ballCollisionName);
            AudioManager1.Ins.PlaySound(SoundType.BallCollision);
        }

        if (objParent != null)
        {
            objParent.delayTimeForSpawnBall();
        }
    }

    public void PlayEffect()
    {
        GameObject effectPlayer = (GameObject)Instantiate(frostEffect, transform.position, transform.rotation);
        effectPlayer.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Destroy(effectPlayer.gameObject, 1f);
    }

    public bool isOutOfGround(Vector2 position)
    {
        return position == Vector2.zero;
    }
}
