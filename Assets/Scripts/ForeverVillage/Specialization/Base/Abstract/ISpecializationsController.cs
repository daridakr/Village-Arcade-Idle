using System;
using UnityEngine;

namespace Village
{
    public interface ISpecializationsController
    {
        public void SetupSpecializationsFor(object condition = null);
        public MonoBehaviour SelectSpecialization(ISpecialization specialization);
        public ISpecialization[] GetAllSpecializations();
        public ISpecialization GetSelectedSpecialization();
        public event Action Initialized;
    }
}