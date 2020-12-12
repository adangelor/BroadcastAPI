using System;

namespace BroadcastApi.Data.Entities
{
    public class Organization : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ConstitutionDate { get; set; }


        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string Country { get; set; }

        public string TaxID { get; set; }

        public string Email { get; set; }

        public string URL { get; set; }


        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankAccountElectronicID { get; set; }
        public bool IsDeleted { get; set; }

    }
}