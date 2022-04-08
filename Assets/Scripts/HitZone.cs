using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] private int penalty;
    [SerializeField] private Knob knob1;
    [SerializeField] private Knob knob2;
    [SerializeField] private Conductor conductor;
    [SerializeField] private ContactFilter2D filter2D;
    List<NoteLogic> notes1 = new List<NoteLogic>();
    List<NoteLogic> notes2 = new List<NoteLogic>();
    List<Collider2D> result1 = new List<Collider2D>();
    List<Collider2D> result2 = new List<Collider2D>();
    private Collider2D zone;
    // Start is called before the first frame update
    void Start()
    {
        zone = GetComponent<Collider2D>();
        // Debug.Log(notes);
        // Debug.Log(result);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && knob1.isActiveAndEnabled)
        {
            notes1.Clear();
            result1.Clear();
            
            zone.OverlapCollider(filter2D, result1);
            // Debug.Log("HITZONE COLLIDING WITH "+ result.Count + " OBJECTS");

            if(result1.Count == 0)
            {
                knob1.source.PlayOneShot(knob1.miss);
                conductor.ChangeScore(-penalty);
            }
            else
            {
                foreach(Collider2D col in result1)
                    notes1.Add(col.GetComponent<NoteLogic>());

                byte count = 0;
                for (int i = 0; i < notes1.Count; i++)
                {
                    NoteLogic note = notes1[i];
                    if (knob1.currentRow == note.row)
                    {
                        Debug.Log("Hit the note on knob 1");
                        count++;
                        float scoreCoefficient = (1f - Mathf.Pow(Mathf.Abs(conductor.getDistanceToBeat() / conductor.errorWindow), 3.0f));
                        note.GetHit(scoreCoefficient);
                        knob1.source.pitch = scoreCoefficient;
                        knob1.source.PlayOneShot(knob1.notes[knob1.currentRow - 1], 0.4f);
                        notes1.RemoveAt(i);
                    }
                }

                if(count == 0)
                {
                    knob1.source.PlayOneShot(knob1.miss);
                    conductor.ChangeScore(-penalty);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.K) && knob2.isActiveAndEnabled)
        {
            notes2.Clear();
            result2.Clear();
            
            zone.OverlapCollider(filter2D, result2);
            // Debug.Log("HITZONE COLLIDING WITH "+ result.Count + " OBJECTS");

            if(result2.Count == 0)
            {
                knob2.source.PlayOneShot(knob2.miss);
                conductor.ChangeScore(-penalty);
            }
            else
            {
                foreach(Collider2D col in result2)
                    notes2.Add(col.GetComponent<NoteLogic>());

                byte count = 0;
                for (int i = 0; i < notes2.Count; i++)
                {
                    NoteLogic note = notes2[i];
                    if (knob2.currentRow == note.row)
                    {
                        Debug.Log("Hit the note on knob 2");
                        count++;
                        float scoreCoefficient = (1f - Mathf.Pow(Mathf.Abs(conductor.getDistanceToBeat() / conductor.errorWindow), 3.0f));
                        note.GetHit(scoreCoefficient);
                        knob2.source.pitch = scoreCoefficient;
                        knob2.source.PlayOneShot(knob2.notes[knob2.currentRow - 1], 0.4f);
                        notes2.RemoveAt(i);
                    }
                }

                if(count == 0)
                {
                    knob2.source.PlayOneShot(knob2.miss);
                    conductor.ChangeScore(-penalty);
                }
            }
        }
    }

}
