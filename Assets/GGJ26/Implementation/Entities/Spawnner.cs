using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    [SerializeField] float SpawnRate;

    float m_timer = 0.0f;
    Camera m_camera;

    void Start()
    {
        m_camera = FindObjectOfType<Camera>();        
    }

    void Update()
    {
        Vector2 screenPos = m_camera.WorldToScreenPoint(transform.position);
        bool onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;

        if(onScreen) { return; }

        m_timer += Time.deltaTime;
        if(m_timer > SpawnRate) 
        {
            Instantiate(Prefab, transform);
            m_timer = 0.0f;
        }
    }
}
