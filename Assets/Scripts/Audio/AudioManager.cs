using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioClip sound, soundTrack, ballCollision, button, buyButton, defeat, playerCollision, realeaseBall, revive, selectFigure, spinStart, victory;
    public static AudioSource audioSource;

    public static string soundName = "Sound";
    public static string soundTrackName = "SoundTrack";
    public static string ballCollisionName = "BallCollision";
    public static string buttonName = "Button";
    public static string buyButtonName = "BuyButton";
    public static string defeatName = "Defeat";
    public static string playerCollisionName = "PlayerCollision";
    public static string realeaseBallName = "RealeaseBall";
    public static string reviveName = "Revive";
    public static string selectFigureName = "SelectFigure";
    public static string spinStartName = "SpinStart";
    public static string victoryName = "Victory";

    private void Awake()
    {
        sound = Resources.Load<AudioClip>(soundName);
        soundTrack = Resources.Load<AudioClip>(soundTrackName);
        ballCollision = Resources.Load<AudioClip>(ballCollisionName);
        button = Resources.Load<AudioClip>(buttonName);
        buyButton = Resources.Load<AudioClip>(buyButtonName);
        defeat = Resources.Load<AudioClip>(defeatName);
        playerCollision = Resources.Load<AudioClip>(playerCollisionName);
        realeaseBall = Resources.Load<AudioClip>(realeaseBallName);
        revive = Resources.Load<AudioClip>(reviveName);
        selectFigure = Resources.Load<AudioClip>(selectFigureName);
        spinStart = Resources.Load<AudioClip>(spinStartName);
        victory = Resources.Load<AudioClip>(victoryName);

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string name)
    {
        switch (name)
        {
            case "Sound":
                audioSource.Stop();
                if (UIManager.Ins.Music == 1)
                    audioSource.PlayOneShot(sound);
                break;
            case "SoundTrack":
                audioSource.Stop();
                if (UIManager.Ins.Music == 1)
                    audioSource.PlayOneShot(soundTrack);
                break;
            case "BallCollision":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(ballCollision);
                break;
            case "Button":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(button);
                break;
            case "BuyButton":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(buyButton);
                break;
            case "Defeat":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(defeat);
                break;
            case "PlayerCollision":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(playerCollision);
                break;
            case "RealeaseBall":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(realeaseBall);
                break;
            case "Revive":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(revive);
                break;
            case "SelectFigure":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(selectFigure);
                break;
            case "SpinStart":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(spinStart);
                break;
            case "Victory":
                if (UIManager.Ins.Sfx == 1)
                    audioSource.PlayOneShot(victory);
                break;
        }
    }
}
