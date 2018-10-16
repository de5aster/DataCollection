using System;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class Work
    {
        public Work()
        {
        }

        public Work(string masterWork)
        {
            this.MasterWork = masterWork;
        }

        public string MasterWork { get; set; }
    }
}
