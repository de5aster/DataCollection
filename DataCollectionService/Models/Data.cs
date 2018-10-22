using System;

namespace DataCollectionService.Entities
{
    [Serializable]
    public class Data
    {
        public Data()
        {
        }

        public Data(string clientName, string clientAddress, string phoneNumber, string email, string equipment, string breakage, string masterName, string masterPersonnelNumber, DateTime putDate, DateTime performData, string[] workList, RepairEquipment[] repairEquipments)
        {
            this.ClientName = clientName;
            this.ClientAddress = clientAddress;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Equipment = equipment;
            this.Breakage = breakage;
            this.MasterName = masterName;
            this.MasterPersonnelNumber = masterPersonnelNumber;
            this.PutDate = putDate;
            this.PerformData = performData;
            this.WorkList = workList;
            this.RepairEquipments = repairEquipments;
        }

        public string ClientName { get; set; }

        public string ClientAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Equipment { get; set; } // Возможно Enum

        public string Breakage { get; set; } // поломка

        public string MasterName { get; set; }

        public string MasterPersonnelNumber { get; set; } // Табельный

        public DateTime PutDate { get; set; } // Дата сдачи

        public DateTime PerformData { get; set; } // Дата выполнения

        public string[] WorkList { get; set; } // Выполненные работы

        public RepairEquipment[] RepairEquipments { get; set; } // Затраченные материалы KeyValuePair = {Запчасть - Количество}
    }
}