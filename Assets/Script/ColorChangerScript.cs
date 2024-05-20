using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChangerScript : MonoBehaviour
{
    [SerializeField] List<Color> listaColores;
    int startPoint;
    int toGoPoint;
    [SerializeField] AnimationCurve curve;
    float seconds;
    [SerializeField] float durationColorChange;
    [SerializeField] Vector2 velocidad;
    void Start()
    {
        startPoint = 0;
        toGoPoint = 1;
        StartCoroutine(colorChanger());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator colorChanger()
    {
        while (true)
        {
            while(seconds <= durationColorChange)
            {
                seconds += Time.deltaTime;

                transform.GetComponent<MeshRenderer>().material.color = Color.Lerp(listaColores[startPoint],
                    listaColores[toGoPoint],
                    curve.Evaluate(seconds / durationColorChange));
                transform.GetComponent<MeshRenderer>().material.mainTextureOffset += velocidad * Time.deltaTime;
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
        toGoPoint = (startPoint + 1) % listaColores.Count;
    }
}
