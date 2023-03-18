using System;
using Game.Entites;
using UnityEngine;
using Game.Entites.Actions;

namespace Game.UI
{
    public class LogLayoutView : MonoBehaviour
    {
        [SerializeField] private LogEntryView _logEntryPrefab;
        [SerializeField] private Transform _logContentContainer;
        private void Start()
        {
            ActionsBase._actionIsCompleteEvent += OnAnActionIsComplete;
        }

        private void OnAnActionIsComplete(ActionsBase action, EntityDynamic actor, object target, DateTime time, string verb)
        { 
            var log = Instantiate(_logEntryPrefab, _logContentContainer.transform);
           log.gameObject.SetActive(true);
           log.SetText(actor.GetDefinitionData()._entityName + " " + verb + " " + target + " !");
        }
    }
}
