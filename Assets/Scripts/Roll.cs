using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Roll : MonoBehaviour,  IPointerDownHandler
{
    public List<DiceDone> dice = new List<DiceDone>();
    public List<DiceDone> rareDice = new List<DiceDone>();
    public bool isRolling = false;


    public int rollcount = 0;
    public Text count;
    public Text collect;

    public Text top;
    public Text buttom;

    public Sprite mark;

    public AudioClip ro;

    void Update() 
    {
        collect.text = (20 - checkDone()) + "/20 COLLECTED";

        if(checkDone() == 0)
        {
            top.text = "GOOD GAME";
            buttom.text = "YOU HAVE COLLECTED ALL THE DICE";
            print("done");
        }
    }

    public  void OnPointerDown(PointerEventData eventData)
    {
        if(isRolling == false)
        {
            StartCoroutine(RollIt());
        }
    }

    public void RollDice()
    {
        GetComponent<AudioSource>().PlayOneShot(ro);
        Image im = GetComponent<Image>();
        float num = Random.Range(0f,1f);
        if(num > 0.1f)
        {
            DiceDone dd = dice[Random.Range(0,dice.Count)];
            dd.collected = true;
            im.sprite = dd.sprite;
        }
        else
        {
            DiceDone dd = rareDice[Random.Range(0,rareDice.Count)];
            dd.collected = true;
            im.sprite = dd.sprite;
        }

        rollcount ++;
        count.text = "ROLL COUNT : "+ rollcount;
    }

    public int checkDone()
    {
        int count = 0;
        foreach (var dd in dice)
        {
            if(!dd.collected)
            {
                count ++;
            }
        }

        foreach (var dd in rareDice)
        {
            if(!dd.collected)
            {
                count ++;
            }
        }

        return count;
    }

    IEnumerator RollIt()
    {
        GetComponent<Image>().sprite = mark;
        isRolling = true;
        GetComponent<Animation>().Play();
        yield return new WaitForSeconds(2f);
        RollDice();
        yield return new WaitForSeconds(0.1f);
        isRolling = false;
    }

    [System.Serializable]
    public class DiceDone
    {
        public Sprite sprite;
        public bool collected = false;
    }
}
