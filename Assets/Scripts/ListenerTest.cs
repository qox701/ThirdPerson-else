using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ListenerTest : MonoBehaviour
{
    private bool canRotate;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddListener<Enemies>("Enemydead",OnEnemyDead);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canRotate)
        {
            this.transform.Rotate(Vector3.up,Time.deltaTime*100);
        }
    }
    
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveListener<Enemies>("Enemydead",OnEnemyDead);
    }
    
    public void OnEnemyDead(Enemies enemy)
    {
        Debug.Log(enemy.gameObject.name + " is dead");
        canRotate = true;
    }
}
