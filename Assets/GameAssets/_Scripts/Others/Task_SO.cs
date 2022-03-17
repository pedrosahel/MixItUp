using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Mix/Task", order = 0)]
public class Task_SO : ScriptableObject 
{
    public enum Frasco{Frasco_01, Frasco_02, Frasco_03}
    public Frasco myFrasco;

    public enum Color{blue, red, yellow, purple, green, orange, empty}
    public Color myColor;

}

