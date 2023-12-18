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
        ONOFF(true); //��ȭ�� ���۵�
        count = 0;
        NextDialogue(); //ȣ����ڸ��� ��簡 ����� �� �ֵ��� 
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
        //ù��° ���� ù��° cg���� ��� ���� cg�� ����Ǹ鼭 ȭ�鿡 ���̰� �ȴ�. 
        txt_Dialogue.text = Managers.Data.LoadData<TalkData>("Talk/TalkData").dialogue[count].sentence;
        sprite_StandingCG.sprite = Managers.Data.LoadData<TalkData>("Talk/TalkData").dialogue[count].cg;
        count++; //���� ���� cg�� �������� 
    }

    public void OnDialogue()
    {
        if (isDialogue) //Ȱ��ȭ�� �Ǿ��� ���� ��簡 ����ǵ���
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (count < Managers.Data.LoadData<TalkData>("Talk/TalkData").dialogue.Length) NextDialogue(); //���� ��簡 �����
                else ONOFF(false); 
            }
        }
    }
}
