using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Ghost _ghostTemplate;

    private List<Ghost> _ghosts = new List<Ghost>();

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        if (_ghosts.Count < value)
        {
            int createHealthValue = value - _ghosts.Count;

            for (int i = 0; i < createHealthValue; i++)
            {
                CreateGhost();
            }
        }
        else if (_ghosts.Count > value)
        {
            int deleteHealthValue = _ghosts.Count - value;

            for (int i = 0; i < deleteHealthValue; i++)
            {
                DestroyGhost(_ghosts[_ghosts.Count-1]);
            }
        }

    }

    private void DestroyGhost(Ghost ghost)
    {
        _ghosts.Remove(ghost);
        ghost.ToEmpty();
    }

    private void CreateGhost()
    {
        Ghost newGhost = Instantiate(_ghostTemplate, transform);
        _ghosts.Add(newGhost.GetComponent<Ghost>());
        newGhost.ToFill();
    }
}
