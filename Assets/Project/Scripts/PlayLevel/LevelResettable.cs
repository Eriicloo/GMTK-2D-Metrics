using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LevelResettable 
{
   
    protected void Subscribe()
    {
        PlayLevelManager.OnPlayStart += OnReset;
    }
    protected void Unsubscribe()
    {
        PlayLevelManager.OnPlayStart -= OnReset;
    }


    protected void OnReset();


}
