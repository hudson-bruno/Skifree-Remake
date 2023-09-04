using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonsterSkiFree : MonoBehaviour
{
    public Transform Player;
    public float Speed = 0.3f;
    private Vector3 DirectinToPlayer;
    private Vector3 groundPosition;
    private float directionToDodge;

    Vector3 point1, point2, point3, normal;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        normalAngle();
        DodgeTree();
        directionPlayer();
        GoingToGround();
        transform.position = Vector3.MoveTowards(this.transform.position, Player.position, Speed);
    }
    public void directionPlayer()
    {
        DirectinToPlayer = (Player.transform.position - this.transform.position).normalized;

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
       
        if(Player.transform.position.x > this.transform.position.x)    //Caso tenha algo entre o jogador e o monstro vai add 10 ou -10 to x do monstro para desviar
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

        Vector3 ray1 = (transform.TransformDirection(new Vector3(-0.3f, -0.5f, 0.5f)).normalized);
        Vector3 ray2 = (transform.TransformDirection(new Vector3(0.3f, -0.5f, 0.5f)).normalized);
        Vector3 ray3 = transform.TransformDirection(new Vector3(0, -1, 0));

        if (Physics.Raycast(transform.position, ray1, out hit))
        {
            point1 = hit.point;
        }

        if (Physics.Raycast(transform.position, ray2, out hit))
        {
            point2 = hit.point;

        }

        if (Physics.Raycast(transform.position, ray3, out hit))
        {
            point3 = hit.point;
        }

        normal = Vector3.Cross(point1 - point3, point2 - point3);
        Debug.DrawRay(transform.position, normal, Color.green);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
    }


}
