﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

