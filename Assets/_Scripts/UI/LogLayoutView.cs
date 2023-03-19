using System;
using System.Collections.Generic;
using Game.Entites;
using UnityEngine;
using Game.Entites.Actions;

namespace Game.UI
{
    public class LogLayoutView : MonoBehaviour
    {
        [SerializeField] private LogEntryView _logEntryPrefab;
        [SerializeField] private Transform _logContentContainer;
        [SerializeField] private int _maxLogCount = 15;

        private List<LogEntryView> _currentLogs = new List<LogEntryView>();
        private void Start()
        {
            ActionsBase._actionIsCompleteEvent += OnAnActionIsComplete;
        }

        private void OnAnActionIsComplete(ActionsBase action, EntityDynamic actor, object target, DateTime time, string verb)
        {
            LogEntryView newLog;
            
            
            if (_currentLogs.Count == _maxLogCount)
            {
                _currentLogs[0].transform.SetAsLastSibling();
                newLog = _currentLogs[_maxLogCount - 1];
               // newLog.transform.SetSiblingIndex(_maxLogCount - 1);
            }
            else
            {
                newLog = Instantiate(_logEntryPrefab, _logContentContainer.transform);
                _currentLogs.Add(newLog);
            }
            
            newLog.gameObject.SetActive(true);
            newLog.SetText(actor.GetDefinitionData()._entityName + " " + verb + " " + target + " !");
        }
    }
}
