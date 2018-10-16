using System;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class Work
    {
        public string MasterWork { get; set; }

        public Work()
        {
        }

        public Work(string masterWork)
        {
            MasterWork = masterWork;
        }

    }
}
