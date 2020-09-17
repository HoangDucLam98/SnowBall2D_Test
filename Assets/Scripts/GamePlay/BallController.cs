using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public Transform ballHolder;
    public Ball ballKeeping;
    public GameObject prefab;
    float angle;

    public bool isMoved;

    private float timeDelaySpawnBall = .5f;
    private float totalTime = 0;

    private float timeDelayForPushed;
    private float oldDistance;

    Vector3 lastPos;
    Vector3 targetPos;

    float speed;
    public Rigidbody2D body;
    public Vector3 direction;

    private float maxTimeOutOfGround = .25f;
    public float totalTimeOutGround;
    public LayerMask ground;
    public LayerMask commingGround;
    // bool isOnCommingGround;

    // [SerializeField]
    [HideInInspector]
    public RaycastHit2D hit;
    [HideInInspector]
    public RaycastHit2D hitDown;

    private float sizeScaled;
    private float sizeForScale = 0.25f;

    private float dragForIns = 0.5f;
    private float dragInscreased;

    public Transform faceObject;

    public InfoData infor;

    // collision
    private int numberBall;
    public bool isPushed;
    float totalTimeForPush;
    float timeForPush = 2f;

    public GameObject stunnedEffect;
    // AI enemy
    public StatusState currentState;
    public StatusState previousState;

    // spine animation
    public SkeletonAnimation skeletonAnimation;

    [HideInInspector]
    public bool isStunning;

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

    public void SetCharacterState(StatusState state)
    {
        if (state == StatusState.Chasing)
        {
            skeletonAnimation.state.ClearTracks();
            skeletonAnimation.skeleton.SetSlotsToSetupPose();
            skeletonAnimation.state.SetAnimation(0, "idle", true);
            // SetAnimation(idle, true, 1);
        }
        else if (state == StatusState.Stun)
        {
            isStunning = true;
            skeletonAnimation.state.ClearTracks();
            // skeletonAnimation.skeleton.SetSlotsToSetupPose();
            skeletonAnimation.state.SetAnimation(0, "stun", true);
            // SetAnimation(stun, true, 1);
        }
    }

    public void OnSpawn(InfoData data)
    {
        this.infor = data;
        faceObject.GetComponent<SpriteRenderer>().sprite = infor.figureSprite;
    }

    public void ReleaseBall()
    {
        if (ballKeeping != null && ballKeeping.transform.parent == ballHolder)
        {
            //Ball newBall = ballKeeping;
            ballKeeping.transform.parent = null;
            ballKeeping.objParent = null;
            //ballKeeping.transform.localScale = ballKeeping.scaleBefore;
            ballKeeping.setIsReleased();
            ballKeeping.ReleaseBall(ballKeeping.transform.position - transform.position);
            ballKeeping = null;
            delayTimeForSpawnBall();
        }
    }

    public virtual void InitBall()
    {
        // Debug.Log(1);
        isMoved = false;
        if (Time.time > totalTime && currentState == StatusState.Chasing)
        {
            var t = Instantiate(prefab, ballHolder.position, Quaternion.identity) as GameObject;
            t.transform.rotation = transform.rotation;
            ballKeeping = t.GetComponent<Ball>();
            ballKeeping.objParent = this;
            ballKeeping.transform.parent = ballHolder;
            ballKeeping.idObject = infor.id;
            ballKeeping.transform.localScale = Vector3.one;
        }
    }

    public void moveCharacter(Vector2 direction)
    {
        if (!UIManager.Ins.gamePlay.notMoving && currentState != StatusState.Idle)
        {
            if (isPushed)
            {
                direction = -direction;
            }

            isMoved = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            angle = Mathf.LerpAngle(angle, targetAngle, Time.fixedDeltaTime * 3f);
            transform.eulerAngles = Vector3.back * angle;

            if (ballKeeping != null)
            {
                ballKeeping.inscreaseSize(Time.deltaTime / 1000);
            }

            transform.Translate(Vector2.up * 3f * Time.fixedDeltaTime);
            if (!isPushed && (ballKeeping == null || ballKeeping.transform.parent == null))
            {
                InitBall();
            }
        }
    }

    public void clearBall()
    {
        if (ballKeeping != null && ballKeeping.transform.parent == ballHolder)
            ballKeeping.DestroyBall();
        // ballKeeping = null;
    }

    public void delayTimeForSpawnBall()
    {
        totalTime = Time.time + timeDelaySpawnBall;
    }

    public virtual void pushByBall(Vector2 direction, float ballSize, int id)
    {
        stunnedEffect.Show();
        clearBall();
        changeIdKilled(id);
        previousState = currentState;
        currentState = StatusState.Stun;
        ++numberBall;
        body.AddForce(direction.normalized * ballSize * 100000 * numberBall);
        isPushed = true;
        //if (totalTimeForPush < Time.time)
        //{
            totalTimeForPush = Time.time + timeForPush;
        //}
        //else
        //{
        //    totalTimeForPush += timeForPush;
        //}

        // GameController.Instance.Killer(infor.idKiller).infor.isPLayer
        if (GameController.Instance.characters[infor.idKiller].isPLayer)
        {
            UIManager.Ins.gamePlay.ShowBoxPush(infor.itemName, ballSize);
        }

        if (infor.idKiller == 0 || infor.id == 0)
            UIManager.Ins.PlayVibrate();

        SetCharacterState(currentState);

        // faceObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        stunnedEffect.Hide();
        currentState = StatusState.Chasing;

        SetCharacterState(currentState);
        MapControler.CutMap += CheckWhenCutMap;
    }

    public virtual void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.up, 100f, ground);
        hitDown = Physics2D.Raycast(transform.position, -transform.up, 100f, ground);

        //Debug.DrawLine(transform.position, hit.point, Color.green);
        //Debug.DrawLine(transform.position, hitDown.point, Color.green);

        // isOnCommingGround = checkObjectOnGround();

        //if (!isOutOfGround(hit.point, hitDown.point))
        //{
        //    totalTimeOutGround = Time.time + maxTimeOutOfGround;
        //}
        //else Time.time > totalTimeOutGround
        //if (isOutOfGround(hit.point, hitDown.point) && GameController.Instance.listObject.Contains(this.gameObject))
        //{
        //    //totalTimeOutGround = Time.time + 100;
        //    body.velocity = Vector3.zero;
        //    BallController other = GameController.Instance.Killer(infor.idKiller);
        //    if (infor.idKiller >= 0 && other != null && !GameController.Instance.characters[infor.idKiller].isDeath)
        //    {
        //        GameController.Instance.UpdateKiller(infor.idKiller);
        //        other.ScaleSize(sizeScaled, dragInscreased);
        //    }
        //    OnDeath();
        //}

        if (numberBall >= 2 && isOutOfGround(hit.point, hitDown.point))
        {
            OnDeath();
        }

        if (ballKeeping != null)
            ballHolder.localPosition = Vector3.up * (faceObject.localScale.y / 2 + ballKeeping.transform.localScale.y / 2 - 0.2f * faceObject.localScale.y);
    }

    void CheckWhenCutMap()
    {
        if (isOutOfGround(hit.point, hitDown.point) && GameController.Instance.listObject.Contains(this.gameObject))
        {
            OnDeath();
        }
    }

    public virtual void FixedUpdate()
    {
        if (Time.time > totalTimeForPush && isStunning)
        {
            EndStun();
        }
    }

    public void EndStun()
    {
        isStunning = false;
        isPushed = false;
        numberBall = 0;
        stunnedEffect.Hide();
        previousState = currentState;
        currentState = StatusState.Chasing;
        SetCharacterState(currentState);
        // faceObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public bool isOutOfGround(Vector2 position, Vector2 positionDown)
    {
        //if (infor.id == 0)
        //    Debug.Log(position + " " + positionDown + " " + Vector2.Distance(position, transform.position));
        //return (Vector2.Distance(position, transform.position) <= 0.1f || Vector2.Distance(positionDown, transform.position) <= 0.1f || position == Vector2.zero || positionDown == Vector2.zero);
        if (position == Vector2.zero || positionDown == Vector2.zero)
        {
            RaycastHit2D hitUpLeft = Physics2D.Raycast(transform.position, Vector2.up, 100f, ground);
            RaycastHit2D hitDownLeft = Physics2D.Raycast(transform.position, Vector2.down, 100f, ground);

            if (hitUpLeft.point == Vector2.zero || hitDownLeft.point == Vector2.zero)
                return true;
        }

        return false;
    }

    // public void scaleSize(Vector3 sizeScaleOfTarget, float dragInscreasedOfTarget)
    // {
    //     sizeScaled += sizeScaleOfTarget;
    //     // transform.localScale += sizeScaleOfTarget;
    //     faceObject.localScale += sizeScaleOfTarget;
    //     sizeScaled += sizeForScale;
    //     // transform.localScale += sizeForScale;
    //     faceObject.localScale += sizeForScale;

    //     // Vector2 tempp = ballHolder.position;
    //     // tempp.y += faceObject.localScale.y;

    //     ballHolder.Translate(new Vector3(0, .08f * faceObject.localScale.y, 0));

    //     dragInscreased += dragInscreasedOfTarget;
    //     body.drag += dragInscreasedOfTarget;
    //     // faceObject.GetComponent<Rigidbody2D>().drag += dragInscreasedOfTarget;
    //     dragInscreased += dragForIns;
    //     body.drag += dragForIns;
    //     // faceObject.GetComponent<Rigidbody2D>().drag += dragForIns;

    // }

    public void ScaleSize(float sizeScaleOfTarget, float dragInscreasedOfTarget)
    {
        StartCoroutine(CoScaleSize(sizeScaleOfTarget, dragInscreasedOfTarget));
    }

    IEnumerator CoScaleSize(float sizeScaleOfTarget, float dragInscreasedOfTarget, float time = 1f)
    {
        sizeScaled += sizeScaleOfTarget + sizeForScale;
        dragInscreased += dragInscreasedOfTarget;

        float drag = dragInscreasedOfTarget + dragForIns;
        body.drag += drag;

        float size = sizeScaleOfTarget + sizeForScale;
        float i = 0;
        Vector2 targetScale = (Vector2)faceObject.localScale + Vector2.one * size;
        while (i < 1)
        {
            i += Time.deltaTime / time;

            var f = Vector2.Lerp(faceObject.localScale, targetScale, i);
            faceObject.localScale = f;
            yield return null;
        }
    }

    public void changeIdKilled(int id)
    {
        infor.idKiller = id;
    }

    public virtual void OnDeath()
    {
        GameController.Instance.AddToCharacterDeaths(infor.id);
        if (infor.idKiller == 0)
        {
            UIManager.Ins.gamePlay.ChangeBlowingText();
        }

        body.velocity = Vector3.zero;
        BallController other = GameController.Instance.Killer(infor.idKiller);
        if (infor.idKiller >= 0 && other != null && !GameController.Instance.characters[infor.idKiller].isDeath)
        {
            GameController.Instance.UpdateKiller(infor.idKiller);
            other.ScaleSize(sizeScaled, dragInscreased);
        }

        GameController.Instance.DestroyGameObjectInList(gameObject);
        infor.isDeath = true;

        //StopCoroutine("UIManager.Ins.gamePlay.showBoxPush");
        if (GameController.Instance.listObject.Count == 1)
        {
            // UIManager.Ins.gamePlay.end.showRankItem(GameControl.Instance.listObject[0].GetComponent<BallController>().infor);
            UIManager.Ins.gamePlay.notMoving = true;

            // Player win
            if (!GameController.Instance.characters[0].isDeath)
            {
                UIManager.Ins.ChangeNumberTop();
                UIManager.Ins.gamePlay.LoadNewChallenge();
                UIManager.Ins.gamePlay.OnPlayerDeath(true);
            }
            // Time.timeScale = 0;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        MapControler.CutMap -= CheckWhenCutMap;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
            if (ballKeeping != null)
            {
                clearBall();
            }

            totalTime = Time.time + timeDelaySpawnBall;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        totalTime = Time.time + timeDelaySpawnBall;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Ball"))
    //     {
    //         Ball ball = other.GetComponent<Ball>();
    //         Vector2 direction = transform.position - other.transform.position;
    //         changeIdKilled(ball.idObject);
    //         clearBall();

    //         pushByBall(direction * 300f, size);
    //     }
    // }
}

public enum StatusState { Idle, Chasing, Stun };