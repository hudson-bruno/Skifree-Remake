using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    public float speed;
    public Animator animator;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);   
    }


    private void OnTriggerEnter(Collider other)
    {
        Bark();
    }

    private void Bark()
    {
        animator.SetBool("Barking", true);
        speed = 0;
    }
}
