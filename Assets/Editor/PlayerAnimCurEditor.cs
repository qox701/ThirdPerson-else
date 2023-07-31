using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerAnimationCurve))]
[CanEditMultipleObjects]
public class PlayerAnimCurEditor : Editor
{
    private PlayerAnimationCurve playerAnimationCurve;
    private AnimationCurve speedCurve;
    private AnimationCurve speedDownCurve;
    private float maxSpeed;
    private float speedUpTotalTime;
    private float speedDownTotalTime;
    private void OnEnable()
    {
        playerAnimationCurve = (PlayerAnimationCurve)target;
        speedCurve=playerAnimationCurve._speedCurve;
        speedDownCurve=playerAnimationCurve._speedDownCurve;
        maxSpeed=playerAnimationCurve.maxSpeed;
        speedUpTotalTime=playerAnimationCurve.speedUpTotalTime;
        speedDownTotalTime=playerAnimationCurve.speedDownTotalTime;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if(GUILayout.Button("Reset"))
        {
            playerAnimationCurve._speedCurve = speedCurve;
            playerAnimationCurve._speedDownCurve = speedDownCurve;
            playerAnimationCurve.maxSpeed = maxSpeed;
            playerAnimationCurve.speedUpTotalTime = speedUpTotalTime;
            playerAnimationCurve.speedDownTotalTime = speedDownTotalTime;

        }
        //
        serializedObject.ApplyModifiedProperties();//最后记得调一下这个方法才能保存序列化结果
    }
    
    private void OnSceneGUI()
    {
        Handles.Label(playerAnimationCurve.transform.position + Vector3.up, playerAnimationCurve.transform.name+":"+playerAnimationCurve.transform.position.ToString());
        
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(200, 200, 200, 150));
        
        if(GUILayout.Button("Reset"))
        {
            playerAnimationCurve.transform.position = Vector3.zero;
            playerAnimationCurve.transform.rotation = Quaternion.identity;
            playerAnimationCurve.transform.localScale = Vector3.one;
        }
        GUILayout.Label("Object have animation curve");
        GUILayout.EndArea();
        Handles.EndGUI();
    }
    
}
