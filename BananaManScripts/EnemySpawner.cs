using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{

    public BoxCollider spawnArea;
    public GameObject enemyPrefab;
    public GameObject player;

    private Bounds _bounds;
    private bool spawn;

    void Awake(){
        _bounds = spawnArea.bounds;
        spawn = true;
    }

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        if(spawn) spawnEnemy();
    }

    private Vector3 randomPointInSpawnArea(Bounds bounds){
        return new Vector3(
            Random.Range(bounds.min.x,bounds.max.x),
            Random.Range(bounds.min.y,bounds.max.y),
            Random.Range(bounds.min.z,bounds.max.z)
        );
    }

    private void spawnEnemy(){
        spawn = false;
        float distanceToPlayer = Vector3.Distance(player.transform.position,this.transform.position);
        if(distanceToPlayer>350){
            GameObject enemy = Instantiate(enemyPrefab,randomPointInSpawnArea(_bounds),Quaternion.identity);
        }
        StartCoroutine(spawnRate());
    }

    private IEnumerator spawnRate(){
        float rate = Random.Range(0,2.5f);
        yield return new WaitForSeconds(rate);
        spawn = true;
    }

}
