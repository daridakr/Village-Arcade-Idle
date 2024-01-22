using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface ISpecializationsController
    {
        public void SetupSpecializationsFor(object condition = null);
        public MonoBehaviour SelectSpecialization(ISpecialization specialization);
        public ISpecialization[] GetAllSpecializations();
        public event Action Initialized;
    }
}