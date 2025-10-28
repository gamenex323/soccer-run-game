using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 360f;

    [Header("Drag Settings")]
    public float dragSensitivity = 0.02f;
    public float minX = -3f;
    public float maxX = 3f;

    public float coolDownTime = 3f;

    private float targetX;
    private Vector3 lastMousePosition;

    void Start()
    {
        targetX = transform.position.x;
    }

    void Update()
    {
        HandleInput();
        MoveBall();
        RotateBall();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            targetX += delta.x * dragSensitivity;
            targetX = Mathf.Clamp(targetX, minX, maxX);
            lastMousePosition = Input.mousePosition;
        }
    }

    void MoveBall()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * 10f);
        transform.position = pos;
    }

    void RotateBall()
    {
        // Constant rotation (in place)
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    public void CoolTheBall()
    {
        BallCollider(false);
        GetComponent<Animator>().enabled = true;
        DOVirtual.DelayedCall(coolDownTime, () =>
        {
            BallCollider(true);
            GetComponent<Animator>().enabled = false;
            StartCoroutine(ChangeColorNextFrame());
        });
    }
    IEnumerator ChangeColorNextFrame()
    {
        yield return null; // Wait one frame
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hurdle")
        {
            BallCollider(false);
            GameManager.Instance.HitWithHurdle();
        }
    }

    public void BallCollider(bool flag)
    {
        GetComponent<Collider>().enabled = flag;
        
    }
}