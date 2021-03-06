﻿using System;
using EasyBuildSystem.Runtimes.Events;
using EasyBuildSystem.Runtimes.Internal.Builder;
using UnityEngine;
using UnityEngine.AI;

public class Demo_TopDown_Player : MonoBehaviour
{
    #region Public Fields

    public LayerMask MovementLayers;

    #endregion

    #region Private Fields

    private NavMeshAgent Agent;

    #endregion

    #region Private Methods

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        EventHandlers.OnBuildModeChanged += OnBuildModeChanged;
    }

    private void OnBuildModeChanged(BuildMode mode)
    {
        if (mode == BuildMode.None)
            Agent.isStopped = false;
        else
            Agent.isStopped = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit Hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, Mathf.Infinity, MovementLayers))
                Agent.destination = Hit.point;
        }
    }

    #endregion
}