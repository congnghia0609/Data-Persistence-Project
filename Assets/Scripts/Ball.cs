using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private MainManager mainManager;
    private String tagBrick = "Brick";
    private String tagPaddle = "Paddle";
    private AudioSource playerAudio;
    public AudioClip explosionSound;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        playerAudio = mainManager.GetComponent<AudioSource>();
    }

    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }
        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }
        m_Rigidbody.velocity = velocity;

        // Khi quả bóng chạm gạch.
        if (other.gameObject.CompareTag(tagBrick))
        {
            playerAudio.PlayOneShot(explosionSound, 1.0f);
        }

        // Khi quả bóng chạm mặt vợt, nếu hết gạch thì tạo lại toàn bộ gạch mới.
        if (other.gameObject.CompareTag(tagPaddle))
        {
            int count = GameObject.FindGameObjectsWithTag(tagBrick).Length;
            if (count == 0)
            {
                mainManager.GenBrickPrefab();
            }
        }
    }
}
