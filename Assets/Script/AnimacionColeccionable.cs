using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionColeccionable : MonoBehaviour
{
    float seconds;
    [SerializeField] float durationSeconds;
    [SerializeField] Vector3 tamañoMax;
    Vector3 tamañoEscalado;
    Vector3 tamañoInicial = new Vector3(1, 1, 1);
    [SerializeField] AnimationCurve curve;
    [SerializeField] Vector3 velocidadRotacion;
    void Start()
    {
        transform.localScale = tamañoInicial;
        tamañoEscalado = tamañoMax;
        StartCoroutine(ScaleChanger());
    }

    void Update()
    {
        transform.Rotate(velocidadRotacion * Time.deltaTime);
    }

    public IEnumerator ScaleChanger()
    {
        while (true)
        {
            while(seconds < durationSeconds)
            {
                seconds += Time.deltaTime;
                transform.localScale = Vector3.Lerp(
                    tamañoInicial,
                    tamañoEscalado,
                    curve.Evaluate(seconds / durationSeconds));

                yield return new WaitForEndOfFrame();

            }
            seconds = 0;
            transform.localScale = tamañoInicial;

            yield return null;
        }
    }
}
