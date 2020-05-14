using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody mRigidbody;

    public float mForwardMoveForce;
    public float mMaxForwardMoveSpeed;

    public float mSideMoveForce;
    public float mMaxSideMoveSpeed;

    public float mJumpForce;
    public Material[] mArrayMaterial;

    void Start()
    {
        this.mRigidbody = GetComponent<Rigidbody>();
        mForwardMoveForce = 1000f;
        mMaxForwardMoveSpeed = 15f;
        mSideMoveForce = 500f;
        mMaxSideMoveSpeed = 6f;
        mJumpForce = 20000f;
    }

    void Update()
    {
        //전방 이동
        if(Input.GetKey(KeyCode.W))
        {
            float speedz = mRigidbody.velocity.z;
            if(speedz < this.mMaxForwardMoveSpeed)
            mRigidbody.AddForce(transform.forward * mForwardMoveForce * Time.deltaTime);                
        }

        // 좌우 이동
        int key = 0;
        if (Input.GetKey(KeyCode.A)) key = -1;
        if (Input.GetKey(KeyCode.D)) key = 1;
        float speedx = Mathf.Abs(mRigidbody.velocity.x);
        if (speedx < this.mMaxSideMoveSpeed)
        {
            mRigidbody.AddForce(transform.right * key * mSideMoveForce * Time.deltaTime);
        }

        // 점프
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float height = mRigidbody.transform.position.y;
            if(height < 0.1f)
            {
                mRigidbody.velocity = new Vector3(mRigidbody.velocity.x, 0, mRigidbody.velocity.z);
                mRigidbody.AddForce(transform.up * mJumpForce * Time.deltaTime);
            }
        }
    }

    // 충돌
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Obstacle")
        {        
            MeshRenderer[] arrayRenderer = GetComponentsInChildren<MeshRenderer>();
            mArrayMaterial = new Material[2];
            for (int i = 0; i < arrayRenderer.Length; ++i)
            {            
                mArrayMaterial[i] = arrayRenderer[i].material;
                mArrayMaterial[i].SetColor("_Color", Color.black);
            }
        }
    }

    // 낙사
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bound")
        {
            MeshRenderer[] arrayRenderer = GetComponentsInChildren<MeshRenderer>();
            mArrayMaterial = new Material[2];
            for (int i = 0; i < arrayRenderer.Length; ++i)
            {
                mArrayMaterial[i] = arrayRenderer[i].material;
                mArrayMaterial[i].SetColor("_Color", Color.red);
            }
        }
    }
}
