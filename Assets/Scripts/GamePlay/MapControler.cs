using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    public static event System.Action CutMap;
    public static MapControler Instance;
    public GameObject edgePrefab;
    public List<int> listPointsToRemove = new List<int>();
    private float timeForNewCut = 5f;
    private float timeDelayForCut = 5f;

    int index = 0;

    public Ferr2DT_PathTerrain terrain;

    public GameObject TerrainPre;
    private Ferr2DT_PathTerrain terrainCut;
    public Vector2 lineCut;

    public Vector2[] listPointSpawn;
    public Vector2 positionRevive;

    private GameObject terrainPrefab, edgePre;

    private Ferr2DT_PathTerrain currentTerrain;
    private EdgeCol currentEdge;
    private Vector2 currentLine;

    // public EdgeCollider2D edgeCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Ferr2DT_PathTerrain.Instance.RemovePoint(1);
        if (Instance == null)
        {
            Instance = this;
        }

        StartCoroutine(CoShowCutArea());
        //StartCoroutine(CoSpawnCutArea());
        // var l = terrain.PathData.GetControls();

    }

    IEnumerator CoSpawnCutArea()
    {
        while (index < listPointsToRemove.Count)
        {
            SpawnCutArea(listPointsToRemove[index]);
            yield return new WaitForSeconds(0.5f);
            index++;
        }
        index = 0;
        yield return StartCoroutine(CoShowCutArea());
    }

    IEnumerator CoShowCutArea()
    {
        while (index < listPointsToRemove.Count)
        {
            float t = timeDelayForCut;
            yield return new WaitForSeconds(2f);
            SpawnCutArea(listPointsToRemove[index]);
            yield return new WaitForSeconds(timeForNewCut - 2);
            ShowNewCutArea(index);
            UIManager.Ins.gamePlay.dangerTxt.Show();
            while (t >= 0)
            {
                t -= Time.deltaTime;
                UIManager.Ins.gamePlay.SetDangerText(Mathf.CeilToInt(t));
                yield return null;
            }
            UIManager.Ins.gamePlay.dangerTxt.Hide();
            terrain.RemovePoint(listPointsToRemove[index]);
            terrain.Build();
            yield return new WaitForSeconds(0.2f);
            if (CutMap != null)
                CutMap();
            index++;
        }
    }

    IEnumerator ChangeLayer()
    {
        while (index < listPointsToRemove.Count)
        {
            float t = timeDelayForCut;
            yield return new WaitForSeconds(timeForNewCut);
            DrawIsCommingGround(listPointsToRemove[index]);
            UIManager.Ins.gamePlay.dangerTxt.Show();
            while (t >= 0)
            {
                t -= Time.deltaTime;
                UIManager.Ins.gamePlay.SetDangerText(Mathf.CeilToInt(t));
                yield return null;
            }
            UIManager.Ins.gamePlay.dangerTxt.Hide();
            terrain.RemovePoint(listPointsToRemove[index]);
            terrain.Build();
            index++;
        }
    }

    private void Update()
    {
        if (terrainCut != null)
        {
            terrainCut.vertexColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * 4, 1));
            terrainCut.PathData.SetDirty();
            terrainCut.Build(true);
        }
    }

    void DrawIsCommingGround(int numberOfPoint)
    {
        terrainPrefab = Instantiate(TerrainPre, transform.position, Quaternion.identity);
        edgePre = Instantiate(edgePrefab, transform.position, Quaternion.identity);

        var edge = edgePre.GetComponent<EdgeCol>();

        List<Vector2> listNewPoints = new List<Vector2>();

        if (numberOfPoint != 0)
            listNewPoints.Add(terrain.PathData.Get(numberOfPoint - 1));
        else
            listNewPoints.Add(terrain.PathData.Get(terrain.PathData.GetPoints(1).Count - 1));

        listNewPoints.Add(terrain.PathData.Get(numberOfPoint));

        if (numberOfPoint < terrain.PathData.GetControls().Count - 1)
            listNewPoints.Add(terrain.PathData.Get(numberOfPoint + 1));
        else
            listNewPoints.Add(terrain.PathData.Get(0));

        var t = terrainPrefab.GetComponent<Ferr2DT_PathTerrain>();
        var m = t.PathData.GetPoints(1);

        for (var i = m.Count - 1; i >= 0; i--)
        {
            t.PathData.RemoveAt(i);
        }

        for (int i = 0; i < listNewPoints.Count; i++)
        {
            t.PathData.Add(listNewPoints[i]);
            if (i == 1)
            {
                t.PathData.GetControls(i).type = terrain.PathData.GetControls(numberOfPoint).type;
                t.PathData.GetControls(i).controlPrev = terrain.PathData.GetControls(numberOfPoint).controlPrev;
                t.PathData.GetControls(i).controlNext = terrain.PathData.GetControls(numberOfPoint).controlNext;
            }
            else
            {
                t.PathData.GetControls(i).type = Ferr.PointType.Sharp;
            }
        }

        lineCut = listNewPoints[0] - listNewPoints[2];

        listNewPoints.Add(listNewPoints[0]);
        edge.gameObject.Hide();
        edge.edge.points = listNewPoints.ToArray();
        edge.gameObject.Show();

        Destroy(edgePre, timeDelayForCut);
        Destroy(terrainPrefab, timeDelayForCut);

        terrainCut = t;
    }

    public void Clear()
    {
        if (terrainPrefab != null)
            Destroy(terrainPrefab.gameObject);

        if (edgePre != null)
            Destroy(edgePre.gameObject);

        Destroy(this.gameObject);
    }

    void SpawnCutArea(int numberOfPoint)
    {
        terrainPrefab = Instantiate(TerrainPre, transform.position, Quaternion.identity);
        edgePre = Instantiate(edgePrefab, transform.position, Quaternion.identity);

        currentEdge = edgePre.GetComponent<EdgeCol>();

        List<Vector2> listNewPoints = new List<Vector2>();

        if (numberOfPoint != 0)
            listNewPoints.Add(terrain.PathData.Get(numberOfPoint - 1));
        else
            listNewPoints.Add(terrain.PathData.Get(terrain.PathData.GetPoints(1).Count - 1));

        listNewPoints.Add(terrain.PathData.Get(numberOfPoint));

        if (numberOfPoint < terrain.PathData.GetControls().Count - 1)
            listNewPoints.Add(terrain.PathData.Get(numberOfPoint + 1));
        else
            listNewPoints.Add(terrain.PathData.Get(0));

        var t = terrainPrefab.GetComponent<Ferr2DT_PathTerrain>();
        var m = t.PathData.GetPoints(1);

        for (var i = m.Count - 1; i >= 0; i--)
        {
            t.PathData.RemoveAt(i);
        }

        for (int i = 0; i < listNewPoints.Count; i++)
        {
            t.PathData.Add(listNewPoints[i]);
            if (i == 1)
            {
                t.PathData.GetControls(i).type = terrain.PathData.GetControls(numberOfPoint).type;
                t.PathData.GetControls(i).controlPrev = terrain.PathData.GetControls(numberOfPoint).controlPrev;
                t.PathData.GetControls(i).controlNext = terrain.PathData.GetControls(numberOfPoint).controlNext;
            }
            else
            {
                t.PathData.GetControls(i).type = Ferr.PointType.Sharp;
            }
        }

        currentLine = listNewPoints[0] - listNewPoints[2];

        listNewPoints.Add(listNewPoints[0]);
        currentEdge.gameObject.Hide();
        currentEdge.edge.points = listNewPoints.ToArray();

        currentTerrain = t;
    }

    void ShowNewCutArea(int index)
    {
        currentEdge.gameObject.Show();

        Destroy(currentEdge.gameObject, timeDelayForCut);
        Destroy(currentTerrain.gameObject, timeDelayForCut);

        terrainCut = currentTerrain;
        lineCut = currentLine;
    }
}
