using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MarbleEventActivation : MonoBehaviour
{
    /*
     * Define multiple states: var t = Mecha.DoubleJump | Mecha.Dash | Mecha.Gravity
     * Remove all states: var t = Mecha.None
     * Remove one state: t &= ~Mecha.DoubleJump
     * Add a state : t |= Mecha.DoubleJump
     * Check a state : if(t & Mecha.DoubleJump > 0){ }
     * Check multiple states : if(b & (Mecha.DoubleJump | Mecha.Dash) > 0){ }
     */
    [Flags]
    public enum ActivationType { Start = 1 << 0, TriggerEnter = 1<<1, TriggerExit = 1 << 2, TriggerStay = 1 << 3, CollisionEnter = 1 << 4, CollisionExit = 1 << 5, CollisionStay = 1 << 6 }

    public Activation[] activations;

    // Start is called before the first frame update
    void Start()
    {
        CheckCollision(ActivationType.Start, null);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(ActivationType.TriggerEnter, other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        CheckCollision(ActivationType.TriggerExit, other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        CheckCollision(ActivationType.TriggerStay, other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(ActivationType.CollisionEnter, collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        CheckCollision(ActivationType.CollisionExit, collision.gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        CheckCollision(ActivationType.CollisionStay, collision.gameObject);
    }

    public void CheckCollision(ActivationType typeOfCollision, GameObject collision)
    {
        foreach (var active in activations)
        {
            #region Event
            if (active.useEvent)
            {
                if ((active.eventActivationType & typeOfCollision) > 0)
                {
                    if (!((typeOfCollision & ActivationType.Start) > 0))
                    {
                        if (active.eventGameObjectToCollide.Length > 0)
                        {
                            foreach (var col in active.eventGameObjectToCollide)
                            {
                                if (col == collision.gameObject)
                                {
                                    Debug.Log("Activate Event by " + typeOfCollision + " collide with " + collision);
                                    active.eventActivate.Invoke();
                                }
                            }
                        }
                        if (active.eventTagNameToCollide.Length > 0)
                        {
                            foreach (var tagName in active.eventTagNameToCollide)
                            {
                                if (collision.gameObject.CompareTag(tagName) || collision.name == tagName)
                                {
                                    Debug.Log("Activate Event by " + typeOfCollision + " collide with " + collision);
                                    active.eventActivate.Invoke();
                                }
                            }
                        }
                    }
                    else
                    {
                        active.eventActivate.Invoke();
                        Debug.Log("Activate Event by " + typeOfCollision);
                    }
                }
            }
            #endregion
            #region Force
            if (active.useForce)
            {
                if ((active.forceActivationType & typeOfCollision) > 0)
                {
                    if (!((typeOfCollision & ActivationType.Start) > 0))
                    {
                        if (active.forceGameObjectToCollide.Length > 0)
                        {
                            foreach (var col in active.forceGameObjectToCollide)
                            {
                                if (col == collision.gameObject)
                                {
                                    Debug.Log("Activate Force by " + typeOfCollision + " collide with " + collision);
                                    foreach (var force in active.forces)
                                    {
                                        if (force.useCollisionObject && collision.TryGetComponent(out Rigidbody rb))
                                            force.objectApplyForce = rb;

                                        force.objectApplyForce.AddForce(force.directionOfForce.normalized * force.forceToApply, ForceMode.Impulse);
                                    }
                                }
                            }
                        }
                        if (active.forceTagNameToCollide.Length > 0)
                        {
                            foreach (var tagName in active.forceTagNameToCollide)
                            {
                                if (collision.gameObject.CompareTag(tagName) || collision.name == tagName)
                                {
                                    Debug.Log("Activate Force by " + typeOfCollision + " collide with " + collision);
                                    foreach (var force in active.forces)
                                    {
                                        if (force.useCollisionObject && collision.TryGetComponent(out Rigidbody rb))
                                            force.objectApplyForce = rb;
                                        force.objectApplyForce.AddForce(force.directionOfForce.normalized * force.forceToApply, ForceMode.Impulse);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var force in active.forces)
                        {
                            force.objectApplyForce.AddForce(force.directionOfForce.normalized * force.forceToApply, force.forceMode);
                        }
                        Debug.Log("Activate force by " + typeOfCollision);
                    }
                }
            }
            #endregion
            #region TP
            if (active.useTP)
            {
                if ((active.tpActivationType & typeOfCollision) > 0)
                {
                    if (!((typeOfCollision & ActivationType.Start) > 0))
                    {
                        if (active.tpGameObjectToCollide.Length > 0)
                        {
                            foreach (var col in active.tpGameObjectToCollide)
                            {
                                if (col == collision.gameObject)
                                {
                                    Debug.Log("Activate tp by " + typeOfCollision + " collide with " + collision);
                                    foreach (var objectToTp in active.objectsToTP)
                                    {
                                        if (objectToTp.stopVelocity && objectToTp.objectTP.TryGetComponent(out Rigidbody rb))
                                        {
                                            rb.velocity = Vector3.zero;
                                        }

                                        if (objectToTp.useCollisionObject)
                                            objectToTp.objectTP = collision.transform;

                                        if (objectToTp.useRandom)
                                        {
                                            Debug.Log("Random Position");
                                            objectToTp.objectTP.position = objectToTp.randomPositions[UnityEngine.Random.Range(0, objectToTp.randomPositions.Length)];
                                            objectToTp.objectTP.rotation = Quaternion.Euler(objectToTp.randomRotations[UnityEngine.Random.Range(0, objectToTp.randomRotations.Length)]);
                                        }
                                        else
                                        {
                                            objectToTp.objectTP.position = objectToTp.position;
                                            objectToTp.objectTP.rotation = Quaternion.Euler(objectToTp.rotation);
                                        }
                                    }
                                }
                            }
                        }
                        if(active.tpTagNameToCollide.Length > 0)
                        {
                            foreach (var tagName in active.tpTagNameToCollide)
                            {
                                if (collision.gameObject.CompareTag(tagName) || collision.name == tagName)
                                {
                                    Debug.Log("Activate tp by " + typeOfCollision + " collide with " + collision);
                                    foreach (var objectToTp in active.objectsToTP)
                                    {
                                        if (objectToTp.stopVelocity && objectToTp.objectTP.TryGetComponent(out Rigidbody rb))
                                        {
                                            rb.velocity = Vector3.zero;
                                        }

                                        if (objectToTp.useCollisionObject)
                                            objectToTp.objectTP = collision.transform;

                                        if (objectToTp.useRandom)
                                        {
                                            objectToTp.objectTP.position = objectToTp.randomPositions.Length != 0 ? objectToTp.randomPositions[Random.Range(0, objectToTp.randomPositions.Length)] : Vector3.zero;
                                            objectToTp.objectTP.rotation = objectToTp.randomRotations.Length != 0 ? Quaternion.Euler(objectToTp.randomRotations[Random.Range(0, objectToTp.randomRotations.Length)]) : Quaternion.identity;
                                        }
                                        else
                                        {
                                            objectToTp.objectTP.position = objectToTp.position;
                                            objectToTp.objectTP.rotation = Quaternion.Euler(objectToTp.rotation);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Activate TP by " + typeOfCollision);
                        foreach (var objectToTp in active.objectsToTP)
                        {
                            if(objectToTp.stopVelocity && objectToTp.objectTP.TryGetComponent(out Rigidbody rb))
                            {
                                rb.velocity = Vector3.zero;
                            }
                            if (objectToTp.useRandom)
                            {
                                objectToTp.objectTP.position = objectToTp.randomPositions.Length != 0 ? objectToTp.randomPositions[Random.Range(0, objectToTp.randomPositions.Length)] : Vector3.zero;
                                objectToTp.objectTP.rotation = objectToTp.randomRotations.Length != 0 ? Quaternion.Euler(objectToTp.randomRotations[Random.Range(0, objectToTp.randomRotations.Length)]) : Quaternion.identity;
                            }
                            else
                            {
                                objectToTp.objectTP.position = objectToTp.position;
                                objectToTp.objectTP.rotation = Quaternion.Euler(objectToTp.rotation);
                            }
                        }
                    }
                }
            }
            #endregion
            #region Spawn
            if (active.useSpawning)
            {
                if ((active.spawnActivationType & typeOfCollision) > 0)
                {
                    if (!((typeOfCollision & ActivationType.Start) > 0))
                    {
                        if (active.spawnGameObjectToCollide.Length > 0)
                        {
                            foreach (var col in active.spawnGameObjectToCollide)
                            {
                                if (col == collision.gameObject)
                                {
                                    Debug.Log("Activate Spawn by " + typeOfCollision + " collide with " + collision);
                                    foreach (var objectToSpawn in active.spawnObject)
                                    {
                                        if (objectToSpawn.useCollisionObject)
                                            objectToSpawn.objectToSpawn = collision;
                                        SpawnObjectSetup(objectToSpawn);
                                    }
                                }
                            }
                        }
                        if (active.spawnTagNameToCollide.Length > 0)
                        {
                            foreach (var tagName in active.spawnTagNameToCollide)
                            {
                                if (collision.gameObject.CompareTag(tagName) || collision.name == tagName)
                                {
                                    Debug.Log("Activate Spawn by " + typeOfCollision + " collide with " + collision);
                                    foreach (var objectToSpawn in active.spawnObject)
                                    {
                                        if (objectToSpawn.useCollisionObject)
                                            objectToSpawn.objectToSpawn = collision;
                                        SpawnObjectSetup(objectToSpawn);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Activate Spawn by " + typeOfCollision);
                        foreach (var objectToSpawn in active.spawnObject)
                        {
                            SpawnObjectSetup(objectToSpawn);
                        }
                    }
                }
            }
            #endregion
            #region Activation
            if (active.useActivation)
            {
                if ((active.activateActivationType & typeOfCollision) > 0)
                {
                    if (!((typeOfCollision & ActivationType.Start) > 0))
                    {
                        if (active.activateGameObjectToCollide.Length > 0)
                        {
                            foreach (var col in active.activateGameObjectToCollide)
                            {
                                if (col == collision.gameObject)
                                {
                                    Debug.Log("Activate Activate by " + typeOfCollision + " collide with " + collision);
                                    foreach (var objectToActivate in active.activateObject)
                                    {
                                        if (objectToActivate.useCollisionObject)
                                            objectToActivate.objectToTarget = collision;

                                        if(objectToActivate.destroy)
                                        {
                                            Destroy(objectToActivate.objectToTarget, objectToActivate.timeDestroy);
                                        }
                                        else
                                        {
                                            objectToActivate.objectToTarget.SetActive(objectToActivate.activation);
                                        }
                                    }
                                }
                            }
                        }
                        if (active.activateTagNameToCollide.Length > 0)
                        {
                            foreach (var tagName in active.activateTagNameToCollide)
                            {
                                if (collision.gameObject.CompareTag(tagName) || collision.name == tagName)
                                {
                                    Debug.Log("Activate Activate by " + typeOfCollision + " collide with " + collision);
                                    foreach (var objectToActivate in active.activateObject)
                                    {
                                        if (objectToActivate.useCollisionObject)
                                            objectToActivate.objectToTarget = collision;

                                        if (objectToActivate.destroy)
                                        {
                                            Destroy(objectToActivate.objectToTarget, objectToActivate.timeDestroy);
                                        }
                                        else
                                        {
                                            objectToActivate.objectToTarget.SetActive(objectToActivate.activation);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Activate Activate by " + typeOfCollision);
                        foreach (var objectToActivate in active.activateObject)
                        {
                            if (objectToActivate.destroy)
                            {
                                Destroy(objectToActivate.objectToTarget, objectToActivate.timeDestroy);
                            }
                            else
                            {
                                objectToActivate.objectToTarget.SetActive(objectToActivate.activation);
                            }
                        }
                    }
                }
            }
            #endregion
            #region Cinemachine
            if (active.useActivation)
            {
                if ((active.cinemachineActivationType & typeOfCollision) > 0)
                {
                    if (!((typeOfCollision & ActivationType.Start) > 0))
                    {
                        if (active.cinemachineGameObjectToCollide.Length > 0)
                        {
                            foreach (var col in active.cinemachineGameObjectToCollide)
                            {
                                if (col == collision.gameObject)
                                {
                                    Debug.Log("Activate cinemachine by " + typeOfCollision + " collide with " + collision);
                                    if (active.useCollisionObject)
                                        active.newCinemachineTarget = collision.transform;

                                    active.cameraTarget.Follow = active.newCinemachineTarget;
                                    active.cameraTarget.LookAt = active.newCinemachineTarget;
                                }
                            }
                        }
                        if (active.cinemachineTagNameToCollide.Length > 0)
                        {
                            foreach (var tagName in active.cinemachineTagNameToCollide)
                            {
                                if (collision.gameObject.CompareTag(tagName) || collision.name == tagName)
                                {
                                    Debug.Log("Activate cinemachine by " + typeOfCollision + " collide with " + collision);
                                    if (active.useCollisionObject)
                                        active.newCinemachineTarget = collision.transform;
                                    active.cameraTarget.Follow = active.newCinemachineTarget;
                                    active.cameraTarget.LookAt = active.newCinemachineTarget;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Activate cinemachine by " + typeOfCollision);
                        active.cameraTarget.Follow = active.newCinemachineTarget;
                        active.cameraTarget.LookAt = active.newCinemachineTarget;
                    }
                }
            }
            #endregion
        }
    }

    private void SpawnObjectSetup(Activation.SpawnObject spawnObject)
    {
        var spawn = GameObject.Instantiate(spawnObject.objectToSpawn, (spawnObject.randomPosition.Length == 0 ? spawnObject.position : spawnObject.randomPosition[Random.Range(0, spawnObject.randomPosition.Length)]), Quaternion.Euler(spawnObject.rotation), spawnObject.parent);
        if(!String.IsNullOrEmpty(spawnObject.layer))
        {
            spawn.layer = LayerMask.NameToLayer(spawnObject.layer);
        }
        if (!String.IsNullOrEmpty(spawnObject.tag))
        {
            spawn.tag = spawnObject.tag;
        }
        if (!String.IsNullOrEmpty(spawnObject.name))
        {
            spawn.name = spawnObject.name;
        }
        if(spawnObject.camera != null && spawnObject.focusCinemachine)
        {
            spawnObject.camera.Follow = spawn.transform;
            spawnObject.camera.LookAt = spawn.transform;
        }
    }

#if UNITY_EDITOR
    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        if (activations == null)
            return;
        if (activations.Length != 0)
        {
            foreach (var active in activations)
            {
                if (active == null)
                    continue;

                if (active.useEvent)
                {
                    foreach (var go in active.eventGameObjectToCollide)
                    {
                        if (go != null)
                        {
                            Gizmos.color = new Color(1, 0, 0, 1);
                            Gizmos.DrawSphere(go.transform.position, 0.3f);
                        }
                    }
                }
                if (active.useForce)
                {
                    foreach (var go in active.forceGameObjectToCollide)
                    {
                        if (go != null)
                        {
                            Gizmos.color = new Color(0, 1, 0, 1);
                            Gizmos.DrawSphere(go.transform.position, 0.3f);
                        }
                    }
                    foreach (var force in active.forces)
                    {
                        DrawArrow(transform.position, force.directionOfForce.normalized, Color.green);
                    }
                }
                if (active.useTP)
                {
                    foreach (var go in active.tpGameObjectToCollide)
                    {
                        if (go != null)
                        {
                            Gizmos.color = new Color(0, 0, 1, 1);
                            Gizmos.DrawSphere(go.transform.position, 0.3f);
                        }
                    }
                    foreach (var tp in active.objectsToTP)
                    {
                        Gizmos.color = new Color(0, 0, 1, 1);
                        if (!tp.useRandom)
                        {
                            Gizmos.DrawSphere(tp.position, 0.2f);
                            DrawArrow(tp.position, Quaternion.Euler(tp.rotation) * Vector3.forward, Color.green);
                        }
                        else
                        {
                            foreach (var randomPosition in tp.randomPositions)
                            {
                                Gizmos.color = new Color(1, 1, 0, 1);
                                Gizmos.DrawSphere(randomPosition, 0.2f);
                                foreach (var randomRotation in tp.randomRotations)
                                {
                                    DrawArrow(randomPosition, Quaternion.Euler(randomRotation) * Vector3.forward, Color.green);
                                }
                            }
                        }
                    }
                }
                if (active.useSpawning)
                {
                    foreach (var go in active.spawnGameObjectToCollide)
                    {
                        if (go != null)
                        {
                            Gizmos.color = new Color(0, 0, 1, 1);
                            Gizmos.DrawSphere(go.transform.position, 0.3f);
                        }
                    }
                    foreach (var spawn in active.spawnObject)
                    {
                        Gizmos.color = new Color(0, 0, 1, 1);
                        if (spawn.randomPosition.Length == 0)
                        {
                            Gizmos.DrawSphere(spawn.position, 0.2f);
                            DrawArrow(spawn.position, Quaternion.Euler(spawn.rotation) * Vector3.forward, Color.green);
                        }
                        else
                        {
                            foreach (var randomPosition in spawn.randomPosition)
                            {
                                Gizmos.color = new Color(1, 1, 0, 1);
                                Gizmos.DrawSphere(randomPosition, 0.2f);
                                DrawArrow(randomPosition, Quaternion.Euler(spawn.rotation) * Vector3.forward, Color.green);
                            }
                        }
                    }
                }
            }
        }
    }

    public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }
    #endregion
#endif

    [Serializable]
    public class Activation
    {
        [Header("Event")]
        public bool useEvent;
        [Tooltip("Le type d'activation de l'event")]
        public ActivationType eventActivationType;
        [Tooltip("Si de type Trigger ou Collision, mettre les objets pour detecter les collisions")]
        public GameObject[] eventGameObjectToCollide;
        [Tooltip("Si de type Trigger ou Collision, mettre les tags ou noms pour detecter les collisions")]
        public string[] eventTagNameToCollide;
        [Tooltip("L'event à trigger en cas de déclenchement")]
        public UnityEvent eventActivate;

        [Header("Force")]
        public bool useForce;
        [Tooltip("Le type d'activation pour la force")]
        public ActivationType forceActivationType;
        [Tooltip("Si de type Trigger ou Collision, mettre les objets pour detecter les collisions")]
        public GameObject[] forceGameObjectToCollide;
        [Tooltip("Si de type Trigger ou Collision, mettre les tags ou noms pour detecter les collisions")]
        public string[] forceTagNameToCollide;
        public Force[] forces;
        
        [Serializable]
        public class Force
        {
            [Tooltip("Le rigibody sur lequel appliquer la force")]
            public Rigidbody objectApplyForce;
            public bool useCollisionObject;
            [Tooltip("La puissance de la force à appliquer")]
            public float forceToApply;
            [Tooltip("La direction vers laquelle la force est appliquée")]
            public Vector3 directionOfForce = new Vector3(1,0,0);
            public ForceMode forceMode;
        }

        [Header("Position & Rotation")]
        public bool useTP;
        [Tooltip("Le type d'activation pour la TP")]
        public ActivationType tpActivationType;
        [Tooltip("Si de type Trigger ou Collision, mettre les objets pour detecter les collisions")]
        public GameObject[] tpGameObjectToCollide;
        [Tooltip("Si de type Trigger ou Collision, mettre les tags ou noms pour detecter les collisions")]
        public string[] tpTagNameToCollide;
        public Tp[] objectsToTP;

        [Serializable]
        public class Tp
        {
            [Tooltip("L'Object à teleporter")]
            public Transform objectTP;
            public bool useCollisionObject;
            [Tooltip("Sa nouvelle position")]
            public Vector3 position;
            [Tooltip("Sa nouvelle rotation")]
            public Vector3 rotation;

            public bool stopVelocity;

            public bool useRandom;
            public Vector3[] randomPositions;
            public Vector3[] randomRotations;
        }

        [Header("Spawn Object")]
        public bool useSpawning;
        [Tooltip("Le type d'activation pour la TP")]
        public ActivationType spawnActivationType;
        [Tooltip("Si de type Trigger ou Collision, mettre les objets pour detecter les collisions")]
        public GameObject[] spawnGameObjectToCollide;
        [Tooltip("Si de type Trigger ou Collision, mettre les tags ou noms pour detecter les collisions")]
        public string[] spawnTagNameToCollide;
        public SpawnObject[] spawnObject;

        [Serializable]
        public class SpawnObject
        {
            public GameObject objectToSpawn;
            public bool useCollisionObject;
            public Vector3 position;
            public Vector3[] randomPosition;
            public Vector3 rotation;
            public Transform parent;
            public string tag, name, layer;
            public Cinemachine.CinemachineVirtualCamera camera;
            public bool focusCinemachine;
        }

        [Header("Activate Object")]
        public bool useActivation;
        [Tooltip("Le type d'activation pour la TP")]
        public ActivationType activateActivationType;
        [Tooltip("Si de type Trigger ou Collision, mettre les objets pour detecter les collisions")]
        public GameObject[] activateGameObjectToCollide;
        [Tooltip("Si de type Trigger ou Collision, mettre les tags ou noms pour detecter les collisions")]
        public string[] activateTagNameToCollide;
        public ActivateObject[] activateObject;

        [Serializable]
        public class ActivateObject
        {
            public GameObject objectToTarget;
            public bool useCollisionObject;
            public bool activation;
            public bool destroy;
            public float timeDestroy;
        }

        [Header("Cinemachine")]
        public bool useCinemachine;
        public ActivationType cinemachineActivationType;
        [Tooltip("Si de type Trigger ou Collision, mettre les objets pour detecter les collisions")]
        public GameObject[] cinemachineGameObjectToCollide;
        [Tooltip("Si de type Trigger ou Collision, mettre les tags ou noms pour detecter les collisions")]
        public string[] cinemachineTagNameToCollide;
        public Cinemachine.CinemachineVirtualCamera cameraTarget;
        public Transform newCinemachineTarget;
        public bool useCollisionObject;
    }
}
