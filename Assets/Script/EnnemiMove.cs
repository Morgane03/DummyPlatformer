using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiMove : MonoBehaviour
{
    public Vector3 start, end;      //Début et fin du déplacement
    private Vector3 startPoint,EndPoint, resetPoint;     //reset le déplacement pour le recommencer
    private float t;
    public float TimeMove = 1;      //Temps de déplacement de l'ennemi de start à end
    public AnimationCurve Acceleration = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1)); //effet d'accélération du mouvement
  
    void Start()
    {
        resetPoint = transform.position;
        startPoint = transform.TransformPoint(start);
        EndPoint = transform.TransformPoint(end);
        t = 0.5f;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(startPoint, EndPoint, Acceleration.Evaluate(t));
        t += Time.deltaTime / TimeMove;
        if (t >= 1)
        {
            var temp = startPoint;
            startPoint = EndPoint;
            EndPoint = temp;
            t = 0;
        }
    }
}
