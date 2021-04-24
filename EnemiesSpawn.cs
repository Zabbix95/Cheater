using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform[] _dotsSpawn;
    [SerializeField] private int _enemyCount = 0;        
    
    void Start()
    {
        _dotsSpawn = gameObject.GetComponentsInChildren<Transform>().Where(go => go.gameObject != this.gameObject).ToArray();        
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    private IEnumerator Spawn()
    {
        while (_enemyCount < _dotsSpawn.Length)
        {
            Transform spawnPosition = _dotsSpawn[Random.Range(0, _dotsSpawn.Length)].GetComponent<Transform>();            
            Collider[] intersecting = Physics.OverlapSphere(spawnPosition.transform.position, 1f, 1 << 6);            
            if (intersecting.Length == 0)
            {                
                Instantiate(_enemy, new Vector3(spawnPosition.position.x, 0f, spawnPosition.position.z), Quaternion.identity);
                _enemyCount++;
                yield return new WaitForSeconds(2f);                
            }            
        }
    }
}
