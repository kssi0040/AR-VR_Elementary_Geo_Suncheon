using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TypingManager : MonoBehaviour 
{
    [SerializeField]
    Text dialogue;

    private string text = "Q 2. 대한민국의 주권은 국민에게 있습니다. 주권의 의미를 알아볼까요?&#xA; &#xA;주인된 권리로 우리나라의 국민이라면 누구나 가진다.";

    StringBuilder sb1 = new StringBuilder();
    StringBuilder sb2 = new StringBuilder();

    WaitForSeconds SpellingDelay = new WaitForSeconds(0.03f);
    
	// Use this for initialization
	void Start () 
    {
        //TypingText(text, dialogue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TypingText(string contents, Text uiText)
    {
        sb1.Append(contents);

        TypeSentence(uiText);
    }

    private void TypeSentence(Text uiText)
    {
        StartCoroutine(ITypeSentence(uiText));
    }

    private IEnumerator ITypeSentence(Text uiText)
    {
        foreach (char letter in sb1.ToString().ToCharArray())
        {
            //_dialogueText.text += letter;
            sb2.Append(letter);
            uiText.text = sb2.ToString();

            yield return SpellingDelay;

            if(AppManage.Instance.isClicked)
            {
                uiText.text = string.Empty;
                uiText.text = sb1.ToString();
                break;
            }
        }
        AppManage.Instance.isComplite = true;
        AppManage.Instance.isClicked = false;
        sb1.Length = 0;
        sb2.Length = 0;
        Debug.Log("Completed!");
    }
}
