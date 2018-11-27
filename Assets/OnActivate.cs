using UnityEngine;
using System.Collections;

public abstract class OnActivate : MonoBehaviour
{
    abstract public IEnumerator onActivate();
    abstract public void onDeactivate();
}
