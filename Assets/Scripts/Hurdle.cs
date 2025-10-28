using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Hurdle : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        speed = GameManager.Instance.currentMode.speedOfLevel;
    }

    void Update()
    {
        if(GameManager.Instance.stopGame)
            return;
        // Move the hurdle forward (along the Z-axis)
        transform.Translate(Vector3.back * GameManager.Instance.currentMode.speedOfLevel * Time.deltaTime);
    }
}