using System.Collections;
using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

namespace Game.Entites
{
    public class ItemEntity : StaticEntityBase
    {
        [SerializeField] protected string _itemName;
        [SerializeField] protected float _weight;
    }
}