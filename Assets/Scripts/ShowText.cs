using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShowTextOneByOne
{
    public class ShowText : MonoBehaviour
    {
        public Text text;
    
        public string textToShow;
        public float speed;
        // Start is called before the first frame update
        void Start()
        {
            text.text = "";
        
        }
        
        private bool _isFinished=true;
        void Update()
        { 
            PlayTypewriterEffect();
        }

        private void PlayTypewriterEffect()
        {
            if (Input.GetKeyDown(KeyCode.Space)&&_isFinished==true)
            {
                _isFinished = false;
                StartCoroutine(Showtext(textToShow));
            }
            else if(Input.GetKeyDown(KeyCode.Space)&&_isFinished==false)
            {
                BreakShow();
            }
        }
    
        private IEnumerator Showtext(string textShow)
        {
            text.text = "";
            int index = 0;

            while (index < textShow.Length)
            {
                text.text += textShow[index];
                index++;
                yield return new WaitForSeconds(speed);
            }

            _isFinished = true;
        }

        private void BreakShow()
        {
            StopAllCoroutines();
            text.text = textToShow;
            _isFinished = true;
        }
        
    }
}

