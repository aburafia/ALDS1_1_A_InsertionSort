﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALDS_1_2_C_StableSort : MonoBehaviour
{
    public RectTransform obj;
    public Text countText;

    RectTransform[] objs;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        int count = 10;

        objs = new RectTransform[count];
        yield return StartCoroutine(genRandom(count));
        yield return StartCoroutine(selectSort());
    }

    IEnumerator insertionSoftFunc()
    {
        yield return null;
    }

    IEnumerator genRandom(int count)
    {
        for (int i = 0; i < count; i++)
        {
            objs[i] = clone(obj);

            objs[i].transform.localPosition = new Vector3(numToXpos(i), 0, 0);
            objs[i].transform.localScale += new Vector3(0, Random.Range(0.1f, 5), 0);

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    int count = 0;

    IEnumerator selectSort()
    {
        for (int i=0;i<objs.Length; i++) 
        {
            int min = i;

            for (int j = i; j < objs.Length; j++)
            {
                min = objs[min].sizeDelta.y > objs[j].sizeDelta.y ? j : min;
            }

            var tmp = objs[i];
            objs[i] = objs[min];
            objs[min] = tmp;

            objs[min].position = new Vector3(numToXpos(min), 0, 0);
            objs[i].position = new Vector3(numToXpos(i), 0, 0);

            count++;
            countText.text = count.ToString();

            yield return new WaitForSeconds(0.2f);

            objs[i].GetComponent<Image>().color = Color.red;
        }

        yield return null;
    }

    //添字からxの位置を導出
    public float numToXpos(int i)
    {
        //間隔
        float dXPosition = 50f;

        //初期位置
        float firstXpos = -5;

        return firstXpos + dXPosition * i;
    }

    public RectTransform clone(RectTransform go)
    {
        var cloneGo = GameObject.Instantiate(go.gameObject) as GameObject;
        cloneGo.transform.parent = go.transform.parent;

        var clone = cloneGo.GetComponent<RectTransform>();
        clone.position = go.position;
        clone.sizeDelta = go.sizeDelta;
        return clone;
    }
}