using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class BasePanel : MonoBehaviour
    {
        
        
        //callback function when click the button
        protected virtual void OnClick(string btnName)  
        {  
        }  
        
        //callback function when click the toggle
        protected virtual void OnToggle(bool value,string toggleName){}

        public virtual void ShowMe()
        {
        }
        
        public virtual void HideMe()
        {
        }
        
        //string is the name of a gameObject
        //List<UIBehaviour> is the list of the controls in the gameObject
        //for one gameObject, it may have multiple controls
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();
        
        //find the controls in the children of the panel
        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            //get all the controls
            T[] controls= this.GetComponentsInChildren<T>();
            //put the controls into the dictionary by the names of the gameObjects
            for (int i = 0; i < controls.Length; i++)
            {
                string objName=controls[i].gameObject.name;
                //if one object have multiple controls, add them into the list
                if(controlDic.ContainsKey(objName))
                    controlDic[objName].Add(controls[i]);
                //if one object have only one control, add it into the dictionary,and create a list to store it
                else
                    controlDic.Add(objName,new List<UIBehaviour>(){controls[i]});
                if (controls[i] is Button)
                {
                    (controls[i] as Button).onClick.AddListener(() =>
                    {
                        OnClick(objName);
                    });
                }
                else if(controls[i] is Toggle)
                {
                    (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                    {
                        OnToggle(value,objName);
                    });
                }
            }
        }

        //find the T in the object by the name of the it
        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(controlName))
            {
                for (int i = 0; i < controlDic[controlName].Count; i++)
                {
                    if (controlDic[controlName][i] is T)
                        return controlDic[controlName][i] as T;
                }
            }
            return null;
        }

        protected virtual void Awake()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
            FindChildrenControl<ScrollRect>();
            FindChildrenControl<InputField>();
        }
    }
}
