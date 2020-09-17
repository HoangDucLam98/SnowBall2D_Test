using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    Vector3 offset;
    void Start()
    {
        // player = FindObjectOfType<Player>().transform;
        // offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.listObject.Count > 1 && player == null)
        {
            if (GameController.Instance.listObject[1].gameObject.name == "Player(Clone)")
                player = GameController.Instance.listObject[1].gameObject.transform;
        }

        if (player != null)
        {
            // transform.position = player.position + offset;
            Camera.main.transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
