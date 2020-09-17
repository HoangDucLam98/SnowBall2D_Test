using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : BallController
{
    public static bool Done = false;
    public int id;
    private Camera camera;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Vector2 pointC;
    private Vector2 offset;
    // Vector2 direction;

    float angleDirection;

    // touch
    public Vector2 startPos;
    Vector2 dir;
    public bool directionChosen;

    // bool waitForRevival;

    // public AnimationReferenceAsset idle, stun;
    // public string playerState;
    public int numberGoBack = 0;

    public override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Application.platform == RuntimePlatform.Android)
        {
            //code cho android
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on touch phase.
                switch (touch.phase)
                {
                    // Record initial touch position.
                    case TouchPhase.Began:
                        startPos = touch.position;
                        touchStart = true;
                        break;

                    // Determine direction by comparing the current touch position with the initial one.
                    case TouchPhase.Moved:
                        UIManager.Ins.ChangePlayerMoveDistance((float)(Time.fixedDeltaTime * 0.004 * 1500f));
                        dir = touch.position - startPos;
                        break;

                    // Report that a direction has been chosen when the finger is lifted.
                    case TouchPhase.Ended:
                        touchStart = false;
                        ReleaseBall();
                        break;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //code cho window
            if (Input.GetMouseButtonDown(0))
            {
                pointA = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.z));
            }

            if (Input.GetMouseButton(0))
            {
                UIManager.Ins.ChangePlayerMoveDistance((float)(Time.fixedDeltaTime * 0.004 * 1500f));
                touchStart = true;
                pointB = camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.z));
            }
            else
            {
                touchStart = false;
                ReleaseBall();
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //cái chỗ này có thế viết như sau:đỡ phải xóa khi build android hay chạy trên window
        if (Application.platform == RuntimePlatform.Android)
        {
            //code cho android
            if (touchStart && !infor.isDeath)
            {
                dir.Normalize();
                moveCharacter(dir);
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //code cho window
            if (touchStart && !infor.isDeath)
            {
                Vector2 tempp = pointB - pointA;
                if (tempp.normalized != offset.normalized && pointA != pointB)
                {
                    offset = tempp;
                    pointA = pointB;
                }
                direction = Vector2.ClampMagnitude(offset, 1.0f);
                if (direction.magnitude < 0.9f)
                {
                    direction.Normalize();
                    moveCharacter(direction);
                }
            }
        }

        UIManager.Ins.gamePlay.SetNumberKilled(infor.numberKilled);
    }

    // void RotateDirection(Vector2 direction)
    // {
    //     float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    //     angleDirection = Mathf.LerpAngle(angleDirection, targetAngle, Time.fixedDeltaTime * 5f);
    //     faceObject.GetChild(0).eulerAngles = Vector3.back * angleDirection;
    // }


    // IEnumerator Revived()
    // {
    //     faceObject.GetComponent<BoxCollider2D>().isTrigger = true;
    //     float i = 0;
    //     while (i <= 3)
    //     {
    //         i += Time.deltaTime;
    //         if (UIManager.Ins.gamePlay.revive)
    //         {
    //             Debug.Log(1);
    //             gameObject.Show();
    //             faceObject.GetComponent<BoxCollider2D>().isTrigger = false;
    //             infor.isDeath = false;
    //             // gameObject.Show();
    //         }
    //         yield return null;
    //     }
    //     if (!UIManager.Ins.gamePlay.revive)
    //         base.OnDeath();
    // }

    public void Revival()
    {
        touchStart = false;
        bool checkActive = gameObject.activeSelf;
        gameObject.SetActive(!checkActive);
        infor.isDeath = !infor.isDeath;
        transform.position = MapControler.Instance.positionRevive;
        // if (ballKeeping != null)
        clearBall();
        SetCharacterState(StatusState.Chasing);
        EndStun();
    }

    public override void OnDeath()
    {
        if (!Done)
        {
            Done = true;
            // AudioManager.PlaySound(AudioManager.defeatName);
            body.velocity = Vector3.zero;
            if (UIManager.Ins.IsFreeLifeSaver && GameController.Instance.listObject.Count > 1)
            {
                UIManager.Ins.gamePlay.PLaySaver();
            }
            else if (GameController.Instance.listObject.Count > 5)
            {
                AudioManager1.Ins.PlaySound(SoundType.Lose);

            if (numberGoBack > 2 && UIManager.Ins.gamePlay.currentState == AvailableState.Challenge)
            {
                if (!UIManager.Ins.gamePlay.notMoving)
                    UIManager.Ins.gamePlay.OnPlayerDeath();

                base.OnDeath();
            }
            else
            {
                UIManager.Ins.gamePlay.RevivalPlayer();
            }
            }
            else if (!infor.isDeath)
            {
                if (!UIManager.Ins.gamePlay.notMoving)
                {
                    AudioManager1.Ins.PlaySound(SoundType.Lose);
                    UIManager.Ins.gamePlay.OnPlayerDeath();
                }

                base.OnDeath();
            }
        }
    }
}
