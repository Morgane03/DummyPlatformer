using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [Tooltip("Le point de départ et d'arrivée. Pour inverser la direction, il faut inverser le start et end.")]
    public Vector3 start, end;
    private Vector3 startPoint, goalPoint, resetPoint;
    private float t;
    [Tooltip("Le temps requis à la plateforme pour aller de start à end.")]
    public float timeToReach = 1;
    [Tooltip("La courbe d'accéleration.")]
    public AnimationCurve acceleration = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    // Start is called before the first frame update
    void Start()
    {
        resetPoint = transform.position;
        startPoint = transform.TransformPoint(start);
        goalPoint = transform.TransformPoint(end);
        //transform.position = transform.TransformPoint(start);
        t = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPoint, goalPoint, acceleration.Evaluate(t));
        t += Time.deltaTime/timeToReach;
        if(t >= 1)
        {
            var temp = startPoint;
            startPoint = goalPoint;
            goalPoint = temp;
            t = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 offset = Vector3.zero;
        if (Application.isPlaying)
        {
            offset = resetPoint - transform.position;
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(resetPoint, 0.1f);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.TransformPoint(start + offset), transform.TransformPoint(end + offset));
        Gizmos.DrawSphere(transform.TransformPoint(start + offset), 0.2f);
        Gizmos.DrawSphere(transform.TransformPoint(end + offset), 0.2f);
    }
}
