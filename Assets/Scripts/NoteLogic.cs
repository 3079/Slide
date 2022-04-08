using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteLogic : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Conductor conductor;
    public int spawnPos;
    public int maxScore;
    public int pos;
    public byte row;
    Transform rect;
    // AudioSource source;
    private bool inZone = false;
    private bool wasHit = false;
    void Awake() {
        conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
    }
    void Start()
    {
        speed = Screen.width / (conductor.eighth * (pos - spawnPos ));
        // Debug.Log(Screen.width + " / (" + conductor.eighth + " * (" + pos + " - " + spawnPos + ")) = " + speed);
        float size = conductor.errorWindow * speed / Mathf.Sqrt(2);
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size, size);
        // rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        // rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(size, size);
        rect.transform.Rotate(new Vector3(0, 0, 45), Space.Self);

        // source = GetComponent<AudioSource>();
    }

    // void FixedUpdate()
    // {
       
    // }

    void Update() {
        Vector3 move = transform.localPosition;
        move.x -= speed * conductor.deltaSong;
        transform.localPosition = move;

        // if(conductor.beatNumber == pos) 
        // {
        //     Debug.Log("NOTE HITS NOW " + conductor.beatNumber + " / " + pos);
        //     StartCoroutine(Blink());
        // }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HitZone zone = other.gameObject.GetComponent<HitZone>();
        if(zone != null && !inZone) 
        {
            inZone = true;
            // if(Input.GetKeyDown())
        }
    }

     void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log("You missed the note in the row " + row);
        HitZone zone = other.gameObject.GetComponent<HitZone>();
        if(zone != null && !wasHit) 
        {
            WasntHit();
            // if(Input.GetKeyDown())
        }
    }

    public void GetHit(float coefficient)
    {
        conductor.ChangeScore((int)(maxScore * coefficient));
        GetComponent<Image>().color = Color.green;
        // source.pitch = scoreCoefficient;
        // source.Play();
        wasHit = true;
        Destroy(gameObject, 0.05f);
    }
    public void WasntHit()
    {
        conductor.ChangeScore(-800);
        GetComponent<Image>().color = Color.red;
        // Debug.Log("You missed the note in the row " + row);
        Destroy(gameObject, 0.5f);
    }
}
