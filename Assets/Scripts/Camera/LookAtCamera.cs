﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform Target;

    private void LateUpdate()
    {
        transform.LookAt(Target);
    }
}
