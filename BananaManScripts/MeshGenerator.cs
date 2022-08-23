using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    public MeshCollider meshCollider;

    public GameObject powerUpPrefab;
    public GameObject enemySpawnerPrefab;
    public GameValues gameValues;
    public GameObject goalObject;

    public Vector3[] vertices;

    public int[] triangles;

    private int xSize;
    private int zSize;
    private int levelIncrease = 0;
    private int stageLevel;
    private int yGoal;
    private int offsetGoalY;
    private int offsetGoalZ;
    private int enemySpawnRate;
    private int powerUpMin;
    private int powerUpMax;
    private int powerUpYmax;
    private int powerUpYmin;

    private float noiseScalar;
    private float yScalar;

    private bool refreshMesh = true;

    void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        AssignValues();
        CreateShape();
        AssignMesh();
        SpawnGoal(xSize,yGoal,zSize);
        RefreshMeshCollider();
    }

    void Update(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void LateUpdate(){
        // Solves a issue where the mesh collider isn't being assinged to the mesh after CreateShape() is done
        if(refreshMesh){
            meshCollider.enabled = false;
            meshCollider.enabled = true;
            refreshMesh = false;
        }
    }

    void AssignValues(){
        levelIncrease   = gameValues.levelIncrease;
        stageLevel      = gameValues.stageLevel;
        offsetGoalY     = gameValues.offsetGoalY;
        offsetGoalZ     = gameValues.offsetGoalZ;
        yGoal           = gameValues.yGoal;
        xSize           = gameValues.xSize;
        zSize           = gameValues.zSize + (levelIncrease*stageLevel);
        noiseScalar     = gameValues.noiseScalar;
        yScalar         = gameValues.yScalar;
        enemySpawnRate  = gameValues.enemySpawnRate;
        powerUpMin      = gameValues.powerUpMin;
        powerUpMax      = gameValues.powerUpMax;
        powerUpYmin     = gameValues.powerUpYmin;
        powerUpYmax     = gameValues.powerUpYmax;
    }

    void SpawnGoal(int x,int y,int z){
        // x/2 to give the middle of the stage
        int _x = (x/2);
        int _y = y + offsetGoalY;
        int _z = z + offsetGoalZ;
        goalObject.GetComponent<Goal>().SetPosition(_x,_y,_z);
    }

    void CreateShape(){

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
       
        for (int i = 0, z = 0; z<= zSize; z++){
            for(int x = 0; x<= xSize; x++){
                float y = Mathf.PerlinNoise(x*noiseScalar , z*noiseScalar) * yScalar * i;
                vertices[i] = new Vector3(x,y,z);
                SpawnPowerUp(x,y,z,i);
                SpawnEnemySpawner(x,y,z,i);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int _verticies = 0;
        int _triangles = 0;

        for(int z = 0; z<zSize;z++){
            for(int x = 0; x<xSize; x++){
                triangles[_triangles + 0] = _verticies + 0;
                triangles[_triangles + 1] = _verticies + xSize + 1;
                triangles[_triangles + 2] = _verticies + 1;
                triangles[_triangles + 3] = _verticies + 1;
                triangles[_triangles + 4] = _verticies + xSize + 1;
                triangles[_triangles + 5] = _verticies + xSize + 2;

                _verticies++;
                _triangles +=6;
            }
            _verticies++;
        }
    }

    private void AssignMesh(){
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    private void SpawnEnemySpawner(float x,float y,float z,int i){
        int range = enemySpawnRate;
        if ( i % range == 0){
            GameObject enemySpawner = Instantiate(enemySpawnerPrefab,new Vector3(x,y,z),Quaternion.identity);
        }
    }

    private void SpawnPowerUp(float x, float y, float z, int i){
        int range = Random.Range(powerUpMin,powerUpMax);
        if( i % range == 0){
            float offset = Random.Range(powerUpYmin,powerUpYmax);
            y += offset;
            GameObject powerUp = Instantiate(powerUpPrefab,new Vector3(x,y,z),Quaternion.identity);
        }
    }

    private void ClearEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies){
            Destroy(enemy);
        }
    }

    private void IncreaseStageLevel(){
        gameValues.stageLevel += 1;
    }

    private void RefreshMeshCollider(){
            refreshMesh = true;
    }

    public void GenerateNewStage(){
        mesh.Clear();
        ClearEnemies();
        RefreshMeshCollider();
        IncreaseStageLevel();
        AssignValues();
        CreateShape();
        AssignMesh();
        SpawnGoal(xSize,yGoal,zSize);
    }

}
