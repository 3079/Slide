using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knob : MonoBehaviour
{
    [SerializeField] private Conductor conductor;
    [SerializeField] private Bar bar;

    [SerializeField] private float speed;
    // [SerializeField] private float delay = 2f;
    [SerializeField] private float phase; // за неимением синуса - начальное расстояние от нуля
    private int turned = 0; //переменная для дебага
    public byte currentRow = 1;

    public AudioClip[] notes;
    public AudioClip miss;
    public AudioSource source;

    private void Start() 
    {
        source = GetComponent<AudioSource>();
        speed = bar.minRange / conductor.crotchet;
        //начальная позиция с учетом фазы
        Vector3 pos = transform.localPosition;
        if(phase > bar.minRange)
        {
            speed *= -1;   
            pos.y = bar.minRange * 2 - phase;
        }
        else
        {
            pos.y = phase;
        }
        transform.localPosition = pos;
    }

    void Update()
    {
        if(conductor.started)
        {
            Vector3 pos = transform.localPosition;
            pos.y -= conductor.deltaSong * speed;
            // pos.y -= Time.deltaTime * speed;

            // разворот в скорости и смена позиции с учетом отскока
            if (pos.y < -bar.range)
            {
                speed *= -1;
                // ошибка была тут
                // было pos.y = -bar.range
                // похоже, от этого терялось довольно значительное расстояние
                pos.y = (-bar.range * 2) - pos.y;

                //для дебага
                turned++;
                // Debug.Log("turned " + turned + " times");
            }

            if (pos.y > 0)
            {
                speed *= -1;
                // аналогично предыдущему случаю
                pos.y *= -1;

                //для дебага
                turned++;
                // Debug.Log("turned " + turned + " times");
            }
            transform.localPosition = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Debug.Log("Collided with something");
        switch(other.tag)
        {
            case "Row 1":
                currentRow = 1;
                break;
            case "Row 2":
                currentRow = 2;
                break;
            case "Row 3":
                currentRow = 3;
                break;
            case "Row 4":
                currentRow = 4;
                break;
            case "Row 5":
                currentRow = 5;
                break;
        };
    }
}
