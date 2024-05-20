using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeccionableBehaviour : MonoBehaviour
{
    //[SerializeField] GameObject coleccionablePF;
    //[SerializeField] Transform spawnPoint;
    [SerializeField] List<Transform> spawPointPositions;
    [SerializeField] Vector3 subida;
    int pos;
    void Start()
    {
        pos = Random.Range(0, spawPointPositions.Count);
        transform.position = new Vector3(spawPointPositions[pos].position.x,
                spawPointPositions[pos].position.y + subida.y,
                spawPointPositions[pos].position.z);
        //spawnPoint = spawnPoint.Find("Spawn");
        //GameObject coleccionableInstance = Instantiate(
        //    coleccionablePF,
        //    spawPointPositions[pos].position + subida,
        //    Quaternion.identity);
        //spawPointPositions = new List<Transform>();
        //for (int i = 0; i < spawnPoint.childCount; i++)
        //    spawPointPositions.Add(spawnPoint.GetChild(i));
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pla"))
        {
            pos = Random.Range(0, spawPointPositions.Count);
            
            //GameObject ColecionaInstanciado = Instantiate(coleccionablePF,
            //    spawPointPositions[pos].position,
            //    Quaternion.identity);
            transform.position = new Vector3(spawPointPositions[pos].position.x,
                spawPointPositions[pos].position.y + subida.y,
                spawPointPositions[pos].position.z);
            print(pos);
        }
    }
    //private void ontriggerenter(collider other)
    //{
    //    if (other.gameobject.comparetag("pla"))
    //    {

    //        gameObject coleccionableinstance = instantiate(
    //        coleccionablepf,
    //        spawpointpositions[pos].position + subida,
    //        quaternion.identity);
    //        destroy(coleccionableinstance);

    //        pos++;
    //        //coleccionablepf.transform.position = spawpointpositions[pos].position + subida;
    //        print("mecoges");
    //    }
            //if (pos >= spawPointPositions.Count)
            //{
            //    pos = 0;
            //    //gameobject coleccionableinstance = instantiate(
            //    //coleccionablepf,
            //    //spawpointpositions[pos].position,
            //    //quaternion.identity);
            //}

    //}



    //Player
}
