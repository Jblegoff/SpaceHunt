using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] int max_HP;
    int _current_HP;

    public int Current_HP { get => _current_HP; set => _current_HP = value; }
    protected void setHP(int new_HP)
    {
        _current_HP = new_HP;
    }
    protected void loseHP(int lost_HP)
    {
        _current_HP -= lost_HP;
    }

    // Start is called before the first frame update
    public virtual void Awake()
    {
        _current_HP = max_HP;
    }
    public int GetMaxHP()
    {
        return max_HP;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
