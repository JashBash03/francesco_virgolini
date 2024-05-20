using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MovimientoColumna : MonoBehaviour
{
    [Header("Posiciones")]
    [SerializeField] List<Transform> positionsToGo = new List<Transform>();
    //[SerializeField] List<Color> ListaColores = new List<Color>();
    int startPoint;
    int toGoPoint;

    [Header("Duraciones")]
    float seconds;
    [SerializeField] float durationSeconds;
    [SerializeField] AnimationCurve curve;

    void Start()
    {
        startPoint = 0;
        toGoPoint = 1;
        transform.position = positionsToGo[0].position;
        StartCoroutine(SubeBajaColumn());
    }

    void Update()
    {
        
    }

    public IEnumerator SubeBajaColumn()
    {
        while (true)
        {
            while(seconds <= durationSeconds)
            {
                seconds += Time.deltaTime;
                
                transform.position = Vector3.Lerp(
                    positionsToGo[startPoint].position,
                    positionsToGo[toGoPoint].position, 
                    curve.Evaluate(seconds / durationSeconds));

                yield return new WaitForEndOfFrame();
            }
            seconds = 0;

            yield return null;
            indexChanger();
        }
    }

    void indexChanger()
    {
        startPoint = toGoPoint;
        toGoPoint = (startPoint + 1) % positionsToGo.Count;
    }
}
