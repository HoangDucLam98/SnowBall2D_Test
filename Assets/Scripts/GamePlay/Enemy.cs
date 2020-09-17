using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BallController
{
    public static event System.Action SpawnNew;
    bool isBallSpawned;

    // private float totalTime;

    private float sizeBall;
    private GameObject target;
    float timeFollow;
    float timeRandomFollow;

    private RaycastHit2D hitCommingGroundUp;
    private RaycastHit2D hitCommingGroundDown;

    bool chasePlayer;

    // public override void Start()
    // {
    //     base.Start();
    //     // player = FindObjectOfType<Player>().transform;
    //     spawner = GetComponent<Spawner>();
    //     // direction = UpdatePath();
    //     // currentState = State.Chasing;
    // }

    public override void Update()
    {
        base.Update();
        //if (GameController.Instance.listObject.Count > 1)
        //{
        switch (UIManager.Ins.gamePlay.typeAI)
        {
            case TypeAI.SizeBall:
                if (ballKeeping != null && isMoved)
                {
                    if (ballKeeping.compareSize(sizeBall))
                    {
                        ReleaseBall();
                        target = null;
                        SetRandomState();
                    }
                    else if (target == null)
                    {
                        direction = UpdatePath();
                    }
                }
                break;

            case TypeAI.DirectionToTarget:
                if (direction == Vector3.zero)
                    direction = UpdatePath();
                else if (transform.up.normalized == direction.normalized)
                {
                    ReleaseBall();
                    direction = UpdatePath();
                    SetRandomState();
                }
                break;

            case TypeAI.FollowDirectionInTime:
                if (Time.time >= timeFollow)
                {
                    ReleaseBall();
                    timeRandomFollow = Random.Range(2f, 7.5f);
                    timeFollow = Time.time + timeRandomFollow;
                    direction = new Vector3(Random.Range(-359, 359), Random.Range(-359, 359), 0);
                    SetRandomState();
                }
                break;

            case TypeAI.FollowTargetInTime:
                if (Time.time >= timeFollow)
                {
                    ReleaseBall();
                    timeRandomFollow = Random.Range(2f, 7.5f);
                    timeFollow = Time.time + timeRandomFollow;
                    direction = UpdatePath();
                    SetRandomState();
                }
                break;

            case TypeAI.SmartAI:
                if (Time.time >= timeFollow)
                {
                    ReleaseBall();
                    timeRandomFollow = Random.Range(2f, 7.5f);
                    timeFollow = Time.time + timeRandomFollow;
                    direction = UpdatePath();
                    SetRandomState();
                }
                break;
        }
        //}

            hitCommingGroundUp = Physics2D.Raycast(transform.position, transform.up, 100f, commingGround);
            hitCommingGroundDown = Physics2D.Raycast(transform.position, -transform.up, 100f, commingGround);

            if (Vector2.Distance(transform.position, hit.point) < 1.5f && currentState == StatusState.Chasing)
            {
                // SetRandomState();
                direction = UpdatePath();
            }
            else if (Vector2.Distance(transform.position, hit.point) < 0.5f && currentState == StatusState.Stun)
            {
                currentState = StatusState.Idle;
            }

            if (checkObjectOnGround())
            {
                direction = UpdatePathForCommingGround();
            }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        // if (!isBallSpawned && !UIManager.Ins.gamePlay.notMoving)
        // {
        //     InitBall();
        //     isBallSpawned = true;
        // }

        //if (currentState == StatusState.Stun)
        //    direction = -direction;
        direction.Normalize();
        moveCharacter(direction);
    }

    private void LateUpdate()
    {
        if (GameController.Instance.listObject.Count == 2 && !chasePlayer)
        {
            chasePlayer = true;
        }
    }

    void SetRandomState()
    {
        int index = Random.Range(0, 10);
        if (index < 9 && !UIManager.Ins.gamePlay.notMoving)
            InitBall();
        else
            StartCoroutine(SetIdle());
    }

    IEnumerator SetIdle()
    {
        currentState = StatusState.Idle;
        if (ballKeeping != null)
            ReleaseBall();
        yield return new WaitForSeconds(1f);
        currentState = StatusState.Chasing;
    }

    public override void InitBall()
    {
        base.InitBall();
        sizeBall = Random.Range(0, 0.005f);
        // direction = UpdatePath();
    }

    Vector3 UpdatePath()
    {
        // int index = Random.Range(0, GameControl.Instance.listObject.Count);
        int index = Random.Range(0, GameController.Instance.listObject.Count);
        target = GameController.Instance.listObject[index];

        if (GameController.Instance.listObject.Count == 2)
            target = GameController.Instance.listObject.Find(s => s.name == "Player(Clone)");
        if (this.gameObject != target && target != null)
        {
            return target.transform.position - transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }

    Vector3 UpdatePathForCommingGround()
    {
        target = null;
        Vector2 dir = -Vector2.Perpendicular(MapControler.Instance.lineCut);
        if (currentState == StatusState.Stun)
            return -dir;
        else
            return dir;
    }

    public bool checkObjectOnGround()
    {
        if (hitCommingGroundUp.point != Vector2.zero && hitCommingGroundDown.point != Vector2.zero)
        {
            return true;
        }
        //else if (hitCommingGroundUp.point != Vector2.zero || hitCommingGroundDown.point != Vector2.zero)
        //{
        //    return false;
        //}
        return false;
    }

    public override void OnDeath()
    {
        if (SpawnNew != null)
            SpawnNew();

        base.OnDeath();
    }
}
