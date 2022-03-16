namespace Main
{
    using UnityEngine;

    public class SpawnerManager : Singleton<SpawnerManager>
    {
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private GameObject[] containers;
        private bool hasSpawned = false;

        private new void Awake() 
        {
            base.Awake();
        }

        public void Spawn(int objIndex)
        {
            if(this.hasSpawned) return;

            GameObject obj = ObjectPooling.Instance.GetObjectFromPool(containers[objIndex].tag);
            obj.transform.position = this.spawnPoint.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
        }

        public void Spawned(bool b)
        {
            this.hasSpawned = b;
        }
    }
}