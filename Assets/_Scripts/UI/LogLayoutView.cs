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
            //ActionsBase._actionIsCompleteEvent += OnAnActionIsComplete;
            ActionHelper.ActionLogEvent += OnMeleeAttackOccured;
            EntityStatsView._entityDiesEvent += OnEntityDies;
        }
        
        private void AddLogEntry(string logEntry)
        {
            LogEntryView newLog;
            
            if (_currentLogs.Count == _maxLogCount)
            {
                _logContentContainer.GetChild(0).transform.SetAsLastSibling();
                newLog = _logContentContainer.GetChild(_logContentContainer.childCount - 1).GetComponent<LogEntryView>();
            }
            else
            {
                newLog = Instantiate(_logEntryPrefab, _logContentContainer.transform);
                _currentLogs.Add(newLog);
            }

            newLog.gameObject.SetActive(true);
            newLog.SetText(logEntry);
        }
        
        private void OnMeleeAttackOccured(string combatLog)
        {
            AddLogEntry(combatLog);
        }
        
        private void OnEntityDies(EntityDynamic entity)
        {
            AddLogEntry(entity.GetDefinitionData()._entityName + " dies!");
        }
    }
}
