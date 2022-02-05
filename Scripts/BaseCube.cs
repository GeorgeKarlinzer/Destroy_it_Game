using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    [SerializeField] protected new Transform transform;
    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] protected new SpriteRenderer renderer;


    public virtual void OnCreate(Vector3 position, Vector2 force)
    {
        transform.position = position;

        rigidbody.AddForce(force);
    }
}
