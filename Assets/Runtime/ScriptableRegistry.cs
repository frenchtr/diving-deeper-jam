using System;
using System.Collections.Generic;
using TravisRFrench.Common.Runtime.Registration;
using UnityEngine;

namespace OTStudios.DDJ.Runtime.Runtime
{
    public class ScriptableRegistry<TEntity> : ScriptableObject, IRegistrar<TEntity>
    {
        private Registrar<TEntity> registrar;

        public bool IsSetup { get; private set; }
        
        public event Action<TEntity> Registered
        {
            add => this.registrar.Registered += value;
            remove => this.registrar.Registered -= value;
        }
        public event Action<TEntity> Deregistered
        {
            add => this.registrar.Deregistered += value;
            remove => this.registrar.Deregistered -= value;
        }

        public void Setup()
        {
            this.registrar = new Registrar<TEntity>();
            this.IsSetup = true;
        }

        public void Teardown()
        {
            this.IsSetup = false;
        }
        
        public void Register(TEntity entity)
        {
            this.registrar.Register(entity);
        }

        public void Deregister(TEntity entity)
        {
            this.registrar.Deregister(entity);
        }

        IEnumerable<TEntity> IRegistrar<TEntity>.Entities
        {
            get => ((IRegistrar<TEntity>)this.registrar).Entities;
        }
    }
}
