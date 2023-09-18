using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonsterSkiFree : MonoBehaviour
{
    public Transform playerTransform;
    public float Speed = 0.3f;
    private Vector3 DirectinToPlayer;
    private Vector3 groundPosition;
    private float directionToDodge;

    Vector3 point1, point2, point3, normal;

    void Awake()
    {
        playerTransform = Player.Instance.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        normalAngle();
        DodgeTree();
        directionPlayer();
        GoingToGround();
        transform.position = Vector3.MoveTowards(this.transform.position, playerTransform.position, Speed);
    }
    public void directionPlayer()
    {
        DirectinToPlayer = (playerTransform.transform.position - this.transform.position).normalized;
        //transform.LookAt(-DirectinToPlayer, normal);/
        Quaternion rotation = Quaternion.LookRotation(DirectinToPlayer, Vector3.up);
        transform.rotation = rotation;

        Debug.DrawRay(transform.position, DirectinToPlayer, Color.cyan);
    }

    public void GoingToGround() 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            groundPosition = hit.point;
            if(groundPosition.y - transform.position.y > transform.lossyScale.y + 0.3f)
            transform.position = Vector3.MoveTowards(this.transform.position, groundPosition, Speed);
        }
        
    }
    public void DodgeTree()
    {
        RaycastHit hit;
       
        if(playerTransform.transform.position.x > this.transform.position.x)    //Caso tenha algo entre o jogador e o monstro vai add 10 ou -10 to x do monstro para desviar
            directionToDodge = 10;
        else { directionToDodge = -10; }
        

        if (Physics.Raycast(transform.position, DirectinToPlayer, out hit))
        {
            if(hit.collider.gameObject.name != "Player")
            {
                transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x + directionToDodge, transform.position.y, transform.position.z), Speed);
            }
        }
    }
    void normalAngle()
    {

        RaycastHit hit;

        Vector3 ray = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, ray, out hit))
        {
            normal = hit.normal;
            Debug.DrawRay(transform.position, normal * 4, Color.green);

            //transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
        }

    }


}
