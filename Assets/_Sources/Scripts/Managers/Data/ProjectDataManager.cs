using System;
using System.Threading;
using CerberusFramework.Managers.Data;
using CerberusFramework.Managers.Data.Syncers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CFGameClient.Managers.Data
{
    public class ProjectDataManager : DataManager
    {
        private ILocalSyncer<ProjectSaveStorage> _localSyncer;

        public ProjectSaveStorage ProjectSaveStorage { get; private set; }

        protected override async UniTask Initialize(CancellationToken disposeToken)
        {
            SaveKey = "ProjectSave";

            _localSyncer = new LocalStorageSyncer<ProjectSaveStorage>(SaveKey, PlayerPrefs.GetInt("PlayerID").ToString());

            ProjectSaveStorage = await _localSyncer.Load(disposeToken);

            StartAutoSavingJob(disposeToken).Forget();
            SetReady();
        }

        protected override void SaveLocal()
        {
            _localSyncer.Save(ProjectSaveStorage);
        }

        public override T Load<T>()
        {
            return !IsReady() ? throw new InvalidOperationException("Trying to load data before ProjectDataManager is ready") : ProjectSaveStorage.Get<T>();
        }

        public override void Save<T>(T data)
        {
            if (!IsReady())
            {
                throw new InvalidOperationException("Trying to save data before ProjectDataManager is ready");
            }

            ProjectSaveStorage.Set(data);
            IsSaveDirty = true;
        }
    }
}
