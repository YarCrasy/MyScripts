using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static GameObject[] pool = new GameObject[10];
    public static int bulletIndex = 0;
    public static bool allCreated = false;
    public GameObject colParticle;

    static ParticleController particleInstance;

    float speed = 50f;

    float maxTimer = 5;
    float timer = 0;

    private void Start()
    {
        particleInstance = ParticleController.instance;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector3(0, 0, 1));
        if (timer >= maxTimer)
        {
            gameObject.SetActive(false);
            timer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObject = collision.gameObject;
        if (colObject.TryGetComponent(out EnemyStateMachine stateMachine))
        {
            stateMachine.SetState(EnemyStates.damage);
        }
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        particleInstance.PlayParticleAt(ParticleType.bullet, transform);
    }
}
