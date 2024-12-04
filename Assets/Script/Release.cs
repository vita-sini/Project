using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Release
{
    public void FreeBlock(Rigidbody selectedBlock)
    {
        selectedBlock = null;
        Debug.Log("Release: FreeBlock");
    }
}
