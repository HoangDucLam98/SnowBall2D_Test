using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector2[] direct;
    public GameObject player;
    public GameObject enemy;
    float randX;
    float randY;
    float nextSpawn = .25f;
    float totalTimeSpawn;
    public int numberEnemy;
    private int numberEnemySpawn;
    private int index = 0;

    private float totalTimeForSpawnE = 2f;

    public GameObject Point;
    public Transform pointHolder;

    public GameObject namePre;
    public Transform nameHolder;

    public List<GameObject> maps;
    public Transform mapHolder;
    GameObject map;
    public Transform spawnHolder;

    Coroutine spawn;

    private void Start()
    {
        //UIGameplay.playerDeath += StopSpawn;
        Enemy.SpawnNew += SpawnPart2;
    }

    public void SetNumberEnemySpawn()
    {
        numberEnemySpawn = numberEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name == "Enemy(Clone)")
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        var mapNumber = Random.Range(0, maps.Count);
        // var mapNumber = 0;
        map = Instantiate(maps[mapNumber], mapHolder.position, Quaternion.identity);
        map.transform.parent = mapHolder;

        while (numberEnemySpawn > numberEnemy / 2)
        {
            Spawn(enemy, numberEnemySpawn);
            numberEnemySpawn--;

            yield return new WaitForSeconds(totalTimeSpawn / totalTimeForSpawnE);
        }

        yield return new WaitForSeconds(2.0f);
        //yield return spawn = StartCoroutine("SpawnEnemyPart2");
    }

    // public bool isCorrectWithMap(Vector2 pos, Vector2[] direct)
    // {

    //     for (int i = 0; i < direct.Length; i++)
    //     {
    //         RaycastHit2D hits;
    //         hits = Physics2D.Raycast(pos, direct[i], 100f, 9);
    //         if (hits == null)
    //         {
    //             return false;
    //         }

    //     }

    //     // RaycastHit2D circleCast = Physics2D.CircleCast(pos, 1f, Vector2.zero, 10, 0);
    //     // if (circleCast.point != Vector2.zero)
    //     // {
    //     //     Debug.Log(circleCast.collider.gameObject.name);
    //     // }

    //     return true;
    // }
    void Spawn(GameObject gameObject, int index)
    {
        // spawn Enemy prefab
        var randomPos = (Vector3)Random.insideUnitCircle * 6;

        // while (!isCorrectWithMap(randomPos, direct))
        // {
        //     randomPos = (Vector3)Random.insideUnitCircle * 10;
        // }
        // RaycastHit2D[] hits1 = Physics2D.RaycastAll(randomPos, -Vector2.up);

        if (index > 5)
            randomPos = (Vector3)map.GetComponent<MapControler>().listPointSpawn[index - 6];
        else if (index > 0 && map != null)
            randomPos = SpawnPos();
        else if (index == 0)
            randomPos += (Vector3)map.GetComponent<MapControler>().listPointSpawn[Random.Range(0, 6)];

        GameObject ene = Instantiate(gameObject, randomPos, Quaternion.identity);

        ene.transform.parent = spawnHolder;

        BallController eneAssigned = ene.GetComponent<BallController>();
        eneAssigned.OnSpawn(GameController.Instance.characters[index]);

        // spawn name for enemy
        GameObject nameControl = Instantiate(namePre, Vector3.zero, Quaternion.identity);
        nameControl.transform.parent = nameHolder;
        nameControl.transform.localScale = Vector3.one;

        NameController nameControlCpn = nameControl.GetComponent<NameController>();
        nameControlCpn.target = ene;
        nameControlCpn.OnSpawn(eneAssigned.infor);

        if (!eneAssigned.infor.isPLayer)
        {
            // spawn point direction to enemy
            GameObject point = Instantiate(Point, Vector3.zero, Quaternion.identity);
            point.transform.parent = pointHolder;
            point.transform.localScale = Vector3.one;
            point.GetComponent<WindowQuestPoint>().goTarget = ene.transform;
            point.GetComponent<WindowQuestPoint>().textMesh.text = eneAssigned.infor.itemName;
            // }
            // else
            // {
            //     var scr = ene.GetComponent<Player>();

        }

        eneAssigned.skeletonAnimation.skeletonDataAsset = eneAssigned.infor.dataAsset;
        eneAssigned.skeletonAnimation.Initialize(true);

        GameController.Instance.listObject.Add(ene);
        GameController.Instance.listBallController.Add(eneAssigned);
    }

    GameObject checkPositionForSpawn(GameObject gameObject)
    {
        var randomPos = (Vector3)Random.insideUnitCircle * 10;
        randomPos += transform.position;
        GameObject ene = Instantiate(gameObject, randomPos, Quaternion.identity);
        BallController eneControl = ene.GetComponent<BallController>();

        if (eneControl.isOutOfGround(eneControl.hit.point, eneControl.hitDown.point))
        {
            Destroy(ene.gameObject);
            return checkPositionForSpawn(gameObject);
        }

        return ene;
    }

    public void OnSpawn()
    {
        StartCoroutine(SpawnEnemy());
        Spawn(player, 0);
    }

    public void StopSpawn()
    {
        //StopCoroutine(spawn);
    }

    IEnumerator SpawnEnemyPart2()
    {
        while (numberEnemySpawn > 0)
        {
            if (spawnHolder.childCount < 7)
            {
                yield return new WaitForSeconds(0.5f);
                Spawn(enemy, numberEnemySpawn);
                numberEnemySpawn--;
            }
            yield return null;
        }
    }

    public void SpawnPart2()
    {
        if( numberEnemySpawn > 0 )
        {
            Spawn(enemy, numberEnemySpawn);
            numberEnemySpawn--;
        }
    }

    Vector3 SpawnPos()
    {
        Vector2[] listSpawn = map.GetComponent<MapControler>().listPointSpawn;
        for (int i = 6; i < listSpawn.Length; i++)
        {
            if (CheckOutViewport(listSpawn[i]))
            {
                return listSpawn[i];
            }
        }

        return Vector2.zero;
    }

    public bool CheckOutViewport(Vector2 pos)
    {
        Vector3 check = Camera.main.WorldToViewportPoint(pos);
        if (check.x > 0 && check.x < 1 && check.y > 0 && check.y < 1 && check.z > 0)
            return false;
        else
            return true;
    }

}
