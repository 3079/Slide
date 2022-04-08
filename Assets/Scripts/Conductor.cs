using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Conductor : MonoBehaviour
{
    [SerializeField] private Bar slider1;
    [SerializeField] private Bar slider2;
    [SerializeField] public float BPM;
    [SerializeField] public float offset;
    [SerializeField] public float delay = 2f;
    [SerializeField] public int notesAhead;
    [SerializeField] public List<Note> notes = new List<Note>();
    // public Note[] notes = 
    
    [SerializeField] public float noteSpeed;
    [SerializeField] public Transform[] rows;
    [SerializeField] public Transform rowsObj;
    [SerializeField] public GameObject hitzone;
    [SerializeField] public GameObject single;
    [SerializeField] public GameObject hold;
    [SerializeField] public AudioSource source;
    public Canvas canvas;
    public float canvasWidth;
    public float crotchet;
    public float errorWindow;
    public float eighth;
    public float bar;
    public float songPosition = 0;
    public float deltaSong;
    private float songdsp;
    public float lastBeat = 0;
    public int beatNumber = -1;
    public int barNumber = -1;
    private float controlX = 200;
    public bool started = false;
    public int lvl = 1;
    public int score = 0;
    public Text scoreText;
    public Text scoreNumberText;
    public LevelManager levelManager;
    public int levelDuration;
    public int growEighth;

    private void Awake() 
    {
        source = GetComponent<AudioSource>();
        crotchet = 60f / BPM; //1/4th
        eighth = crotchet / 2;
        errorWindow = crotchet / 4;
        bar = crotchet * 4;
        scoreNumberText.text = BPM.ToString();
        scoreText.text = "BPM";
        //zone.localPosition.x + Screen.width, 0f, 0f);
        StartCoroutine(startLevel());
    }

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        switch(lvl) 
        {
            case 1:
                initializeLevel1();
                break;
            case 2:
                initializeLevel2();
                break;
            case 3:
                initializeLevel3();
                break;
        }
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float speed = Screen.width / (eighth * (notesAhead));
        float size = errorWindow * speed / Mathf.Sqrt(2);
        hitzone.GetComponent<RectTransform>().sizeDelta = new Vector2(size,  hitzone.GetComponent<RectTransform>().rect.height);
        hitzone.GetComponent<BoxCollider2D>().size = new Vector2(size,  hitzone.GetComponent<RectTransform>().rect.height);
        rowsObj.localPosition = new Vector3(hitzone.transform.localPosition.x + Screen.width, 0, 0);
    }

    public IEnumerator startLevel()
    {
        yield return new WaitForSeconds(delay/2);
        scoreNumberText.text = "READY";
        scoreText.text = "GET";

        yield return new WaitForSeconds(delay/2);
        source.Play();
        songdsp = (float)AudioSettings.dspTime;
        started = true;   

        scoreNumberText.text = score.ToString();
        scoreText.text = "SCORE";
    }

    void Update()
    {
        if(started) 
        {

        float tmp = songPosition;
        songPosition = (float)AudioSettings.dspTime - songdsp - offset;

        deltaSong = Mathf.Max(0, songPosition - tmp); 
        
        if(songPosition > (beatNumber + 1) * eighth)
        {
            beatNumber++;

            SpawnNotesAhead(notesAhead, beatNumber);

            if( beatNumber >= levelDuration) 
            {
                switch(lvl)
                {
                    case 1:
                        if(score > HighScoreManager.LVL1_HIGHSCORE)
                            HighScoreManager.LVL1_HIGHSCORE = score;
                        break;
                    case 2:
                        if(score > HighScoreManager.LVL2_HIGHSCORE)
                            HighScoreManager.LVL2_HIGHSCORE = score;
                        break;
                    case 3:
                        if(score > HighScoreManager.LVL3_HIGHSCORE)
                            HighScoreManager.LVL3_HIGHSCORE = score;
                        break;
                }

                levelManager.Menu();
            }

            if (growEighth > 0 && beatNumber == growEighth)
            {
                slider1.Grow();
                slider2.Grow();
            }
        }

        if (songPosition > (barNumber + 1)  * bar)
            barNumber++;
        
        }
    }

    private void SpawnNotesAhead(int n, int spawnPos)
    {
        for (int j = 0; j < notes.Count; j++)
        {

            if (notes[j].pos <= spawnPos + n)
            {
                GameObject noteObj = Instantiate(single, rows[notes[j].row - 1].position, Quaternion.identity, rows[notes[j].row - 1]);
                NoteLogic note = noteObj.GetComponent<NoteLogic>();
                note.row = notes[j].row;
                note.pos = notes[j].pos;
                note.spawnPos = spawnPos;
                notes.RemoveAt(j);
            }
            else
                break;
        }
    }

    // private void newNote(int pos, byte row)
    // {
    //     notes.Add(new Note(pos, row));
    // } 

    // -разница для опережения, +разница для отставания
    public float getDistanceToBeat()
    {
        int currentBeat = beatNumber;
        if((currentBeat + 1) * eighth - songPosition < errorWindow)
        {
            return songPosition - (currentBeat + 1) * eighth;
        }
        else
            return songPosition - currentBeat * eighth;
    }

    public void ChangeScore(int value)
    {
        score = Mathf.Clamp(score + value, int.MinValue, int.MaxValue);
        scoreNumberText.text = score.ToString();
    }

    private void initializeLevel1()
    {
        notes.Clear();
        //F2
        //D2
        notes.Add(new Note(32, 2));
        notes.Add(new Note(36, 2));
 
        notes.Add(new Note(40, 2));
        notes.Add(new Note(44, 2));
 
        notes.Add(new Note(50, 1));
        notes.Add(new Note(54, 1));
 
        notes.Add(new Note(58, 1));
        notes.Add(new Note(62, 1));
 
        notes.Add(new Note(64, 2));
        notes.Add(new Note(68, 2));
 
        notes.Add(new Note(72, 2));
        notes.Add(new Note(76, 2));
        notes.Add(new Note(78, 1));
 
        notes.Add(new Note(80, 2));
        notes.Add(new Note(84, 2));
 
        notes.Add(new Note(88, 2));
        notes.Add(new Note(92, 2));
        notes.Add(new Note(94, 1));
 
        notes.Add(new Note(96, 2));
    }

     private void initializeLevel2()
    {
        notes.Clear();
        //первый слайдер с фазой 100(в самом низу)
        //D2  D2
        //C2  F2
        // второй - 0
        notes.Add(new Note(32, 1));
        notes.Add(new Note(34, 1));
        notes.Add(new Note(36, 1));
        notes.Add(new Note(38, 1));

        notes.Add(new Note(40, 1));
        notes.Add(new Note(42, 1));
        notes.Add(new Note(44, 1));
        notes.Add(new Note(46, 2));

        notes.Add(new Note(48, 1));
        notes.Add(new Note(50, 2));
        notes.Add(new Note(52, 1));
        notes.Add(new Note(54, 2));

        notes.Add(new Note(56, 1));
        notes.Add(new Note(58, 1));
        notes.Add(new Note(60, 1));
        notes.Add(new Note(62, 2));

        notes.Add(new Note(64, 2));
        notes.Add(new Note(66, 1));
        notes.Add(new Note(68, 2));
        notes.Add(new Note(70, 1));

        notes.Add(new Note(72, 2));
        notes.Add(new Note(74, 1));
        notes.Add(new Note(76, 2));
        notes.Add(new Note(78, 1));

        notes.Add(new Note(80, 2));
        notes.Add(new Note(82, 1));
        notes.Add(new Note(84, 1));
        notes.Add(new Note(86, 2));

        notes.Add(new Note(88, 1));
        notes.Add(new Note(90, 1));
        notes.Add(new Note(92, 1));
        notes.Add(new Note(94, 1));

        notes.Add(new Note(96, 1));

    }
    private void initializeLevel3()
    {
        notes.Clear();
        //первый слайдер с фазой 100(в самом низу)
        // второй - 0
        //F2 D2
        //D2 C2
        //C2 A1
        notes.Add(new Note(32, 2));
        notes.Add(new Note(34, 2));
        notes.Add(new Note(36, 1));
        notes.Add(new Note(38, 1));

        notes.Add(new Note(40, 2));
        notes.Add(new Note(42, 2));
        notes.Add(new Note(44, 1));
        notes.Add(new Note(46, 1));

        //удлиннение
        notes.Add(new Note(48, 1));
        notes.Add(new Note(50, 2));
        notes.Add(new Note(52, 3));
        notes.Add(new Note(54, 2));

        notes.Add(new Note(56, 1));
        notes.Add(new Note(58, 2));
        notes.Add(new Note(60, 3));
        notes.Add(new Note(62, 2));

        notes.Add(new Note(64, 2));
        notes.Add(new Note(66, 3));
        notes.Add(new Note(68, 2));
        notes.Add(new Note(70, 1));

        notes.Add(new Note(72, 2));
        notes.Add(new Note(74, 3));
        notes.Add(new Note(76, 2));
        notes.Add(new Note(78, 1));

        notes.Add(new Note(80, 1));
        notes.Add(new Note(82, 2));
        notes.Add(new Note(84, 2));
        notes.Add(new Note(86, 2));

        notes.Add(new Note(88, 1));
        notes.Add(new Note(90, 2));
        notes.Add(new Note(92, 2));
        notes.Add(new Note(94, 2));

        notes.Add(new Note(96, 2));

    }
}
