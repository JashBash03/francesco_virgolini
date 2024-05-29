using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeccionableBehaviour : MonoBehaviour
{
    [SerializeField] List<Transform> spawPointPositions;
    [SerializeField] Vector3 subida;
    int pos;
    void Start()
    {
        pos = Random.Range(0, spawPointPositions.Count);
        transform.position = new Vector3(spawPointPositions[pos].position.x,
                spawPointPositions[pos].position.y + subida.y,
                spawPointPositions[pos].position.z);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pos = Random.Range(0, spawPointPositions.Count);
            transform.position = new Vector3(spawPointPositions[pos].position.x,
                spawPointPositions[pos].position.y + subida.y,
                spawPointPositions[pos].position.z);
            print(pos);
        }
    }
}
