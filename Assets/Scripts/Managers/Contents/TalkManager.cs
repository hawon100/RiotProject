using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_StandingCG; 
    [SerializeField] private SpriteRenderer sprite_DialogueBox;
    [SerializeField] private Text txt_Dialogue;

    private bool isDialogue = false; 
    private int count = 0;

    public void ShowDialogue()
    {
        ONOFF(true); //대화가 시작됨
        count = 0;
        NextDialogue(); //호출되자마자 대사가 진행될 수 있도록 
    }

    private void ONOFF(bool _flag)
    {
        sprite_DialogueBox.gameObject.SetActive(_flag);
        sprite_StandingCG.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }

    private void NextDialogue()
    {
        //첫번째 대사와 첫번째 cg부터 계속 다음 cg로 진행되면서 화면에 보이게 된다. 
        txt_Dialogue.text = Managers.Data.LoadData<TalkData>("Talk/TalkData").dialogue[count].sentence;
        sprite_StandingCG.sprite = Managers.Data.LoadData<TalkData>("Talk/TalkData").dialogue[count].cg;
        count++; //다음 대사와 cg가 나오도록 
    }

    public void OnDialogue()
    {
        if (isDialogue) //활성화가 되었을 때만 대사가 진행되도록
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (count < Managers.Data.LoadData<TalkData>("Talk/TalkData").dialogue.Length) NextDialogue(); //다음 대사가 진행됨
                else ONOFF(false); 
            }
        }
    }
}
