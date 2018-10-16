using System;

namespace DataCollectionService.Entities
{   
    [Serializable]
    public class Data
    {
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Equipment { get; set; } //Возможно Enum
        public string Breakage { get; set; } //поломка
        public string MasterName { get; set; }
        public string MasterPersonnelNumber { get; set; } //Табельный
        public DateTime PutDate { get; set; } //Дата сдачи
        public DateTime PerformData { get; set; } //Дата выполнения
        public Work[] WorkList { get; set; } //Выполненные работы
        public RepairEquipment[] RepairEquipments { get; set; } //Затраченные материалы KeyValuePair = {Запчасть - Количество}

        public Data()
        {
        }
        public Data(string clientName, string clientAddress, string phoneNumber, string email, string equipment, string breakage, string masterName, string masterPersonnelNumber, DateTime putDate, DateTime performData, Work[] workList, RepairEquipment[] repairEquipments)
        {
            ClientName = clientName;
            ClientAddress = clientAddress;
            PhoneNumber = phoneNumber;
            Email = email;
            Equipment = equipment;
            Breakage = breakage;
            MasterName = masterName;
            MasterPersonnelNumber = masterPersonnelNumber;
            PutDate = putDate;
            PerformData = performData;
            WorkList = workList;
            RepairEquipments = repairEquipments;
        }
    }

    
}