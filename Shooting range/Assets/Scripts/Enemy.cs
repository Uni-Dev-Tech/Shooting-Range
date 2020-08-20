using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 3f, maxSpeed = 7f;
    private float speed;
    [SerializeField]
    private float minScale = 0.3f, maxScale = 0.5f;
    private float scale;
    public Animator animator;
    public ParticleSystem explosion;
    private bool activeZone;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        explosion = GetComponentInChildren<ParticleSystem>();
        speed = Random.Range(minSpeed, maxSpeed);
        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
        activeZone = false;
    }
    private void OnEnable()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    private void OnDisable()
    {
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        transform.rotation = quaternion;
        transform.localScale = new Vector3(1, 1, 1);
        activeZone = false;
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }
    public void DisableMyself()
    {
        gameObject.SetActive(false);
        WorldController.instance.AddEnemy();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DisableZone") && activeZone)
        {
            UIManager.instance.birdsPoints--;
            DisableMyself();
        }
        if (other.CompareTag("DisableZoneActive"))
            activeZone = true;
    }
}
