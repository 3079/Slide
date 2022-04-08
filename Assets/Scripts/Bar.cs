using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] public float minRange;
    [SerializeField] public float maxRange;
    [SerializeField] public int rangeLevels;
    [SerializeField] private Conductor conductor;
    [SerializeField] private Knob knob;
    private AudioSource song;
    public float range;
    private float lastBeat;

    // Start is called before the first frame update

    //применение изменения высоты
    void ChangeHeight()
    {
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, range);
    }

    //увеличение высоты на 1 тон
    public void Grow()
    {
        // range = Mathf.Clamp(maxRange / ((maxRange / range) - 1), minRange, maxRange);
        range = Mathf.Clamp(range + (maxRange - minRange) / rangeLevels, minRange, maxRange);
        // Debug.Log(range);
        ChangeHeight();
    }

    void Start()
    {
        lastBeat = conductor.offset;
        range = minRange;
        ChangeHeight();
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.C)) 
        // {
        //     range = Mathf.Clamp(range + (maxRange - minRange) / 4, minRange, maxRange);
        //     ChangeHeight();
        //     knob.UpdateParameters();
        // }

        // if(conductor.songPosition > lastBeat + conductor.bar * 4)
        // {
        //     Grow();
        //     lastBeat += conductor.bar * 4;
            // Debug.Log("BAR GROWTH " + conductor.songPosition);
        // }
    }
}
