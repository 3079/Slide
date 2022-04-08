using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note
{
    public int pos;
    public byte row;
    // public byte duration;

    // public enum Type { Single, Chord, Hold }
    // public Type type;

    public Note(int pos, byte row)
    {
        this.pos = pos;
        this.row = row;
        // this.duration = 0;
    }

    /*
    public Note(int pos, byte row, byte duration) : this(pos, row)
    {
        this.duration = duration;
    }
    */
}
