using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New list of tasks", menuName = "Mix/ListOfTasks")]
public class ListOfTasks : ScriptableObject
{
    public List<Task_SO> tasks;
}
