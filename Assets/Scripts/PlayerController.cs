using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = .25f;
    [SerializeField] private bool lockPlayerMovement = false;
    [SerializeField] private IntVariable magicCount;
    [SerializeField] private Vector3 transportOffset;
    [SerializeField] private Collider collider;
    [SerializeField] private LineRenderer arrow;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseStrength;
    [SerializeField] private float verticalImpulse;
    [SerializeField] private float maxArrowLength;
    [SerializeField] private GameObject launchParticles;
    [SerializeField] private ParticleSystem smokeParticles;
    private Vector3 spawnPoint;
    private float tileSize = .5f;
    private IObservable<Vector2> movement;
    private Camera cam;
    private void Awake()
    {
        movement = this.FixedUpdateAsObservable()
            .Select(_ => {
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");
                return new Vector2(x, y).normalized;
            });
    }

    void Start()
    {
        //smokeParticles.gameObject.SetActive(false);
        cam = Camera.main;
        
        movement
            .Where(v => v != Vector2.zero)
            .Subscribe(inputMovement => {
                
                var inputVelocity = inputMovement * walkSpeed;
                var playerVelocity =
                    inputVelocity.x * transform.right +
                    inputVelocity.y * transform.forward;
                var distance = playerVelocity * Time.fixedDeltaTime;
                if (!lockPlayerMovement) transform.Translate(distance);

            }).AddTo(this);

#if !UNITY_ANDROID
        this.OnMouseDownAsObservable()
            .Subscribe(_ => arrow.enabled = true)
            .AddTo(this);
        
        this.OnMouseDownAsObservable()
            .SelectMany(_ => this.UpdateAsObservable())
            .TakeUntil(this.OnMouseUpAsObservable())
            .Select(_ => Input.mousePosition)
            .RepeatUntilDestroy(this) // safety way
            .Subscribe(pos => UpdateArrow(pos)).AddTo(this);

        this.OnMouseUpAsObservable()
            .Subscribe(_ =>
            {
                arrow.enabled = false;
                ThrowSelf();
            })
            .AddTo(this);
#endif
        spawnPoint = transform.position;
    }

    private void UpdateArrow(Vector3 mousePos)
    {
        arrow.SetPosition(0, this.transform.position);
        Ray ray = cam.ScreenPointToRay(mousePos);
        Plane hPlane = new Plane(Vector3.up , Vector3.zero + new Vector3(0f, .6f, 0f));
        float distance = 0; 
        if (hPlane.Raycast(ray, out distance)){
            // get the hit point:
            arrow.SetPosition(1,ray.GetPoint(distance));
            if (Vector3.Distance(arrow.GetPosition(0), arrow.GetPosition(1)) > maxArrowLength)
            {
                Vector3 norm = this.transform.position + maxArrowLength * (arrow.GetPosition(1) - arrow.GetPosition(0)).normalized;
                arrow.SetPosition(1, norm);
            }
        }
    }

    private void ThrowSelf()
    {
        if (lockPlayerMovement) return;
        GameObject particles = Instantiate(launchParticles, transform.position, Quaternion.identity);
        Destroy(particles,1f);
        //StartCoroutine(PlaySmoke());
        float strength = Vector3.Distance(arrow.GetPosition(1), arrow.GetPosition(0));
        Vector3 arrowForce = (arrow.GetPosition(1) - arrow.GetPosition(0)).normalized;
        arrowForce *= impulseStrength * strength;
        float vertical = Mathf.Lerp(verticalImpulse*.2f, verticalImpulse, strength / maxArrowLength);
        rb.AddForce(arrowForce[0], vertical, arrowForce[2], ForceMode.Impulse);
    }
    
    IEnumerator PlaySmoke() 
    {
        smokeParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        smokeParticles.gameObject.SetActive(false);
    }
    
    public void SetMovementLock(bool value)
    {
        rb.velocity = Vector3.zero;
        lockPlayerMovement = value;
    }

    public void TransportPlayer(Transform t)
    {
        StartCoroutine(LockControls());
        rb.velocity = Vector3.zero;
        transform.position = t.TransformPoint(transportOffset);
        spawnPoint = transform.position;

    }

    public void DeathActivated()
    {
        rb.velocity = Vector3.zero;
        transform.position = spawnPoint;
    }

    IEnumerator LockControls()
    {
        lockPlayerMovement = true;
        yield return new WaitForSeconds(.35f);
        lockPlayerMovement = false;

    }

    

    


    
}

