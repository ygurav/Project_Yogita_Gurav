﻿namespace Project_Yogita_Gurav.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string salutation { get; set; }

        public string initials { get; set; }
        public string firstname { get; set; }
        public string firstname_ascii { get; set; }
        public string gender { get; set; }

        public string firstname_country_rank { get; set; }

        public string firstname_country_frequency { get; set; }

        public string lastname { get; set; }

        public string lastname_ascii { get; set; }

        public string lastname_country_rank { get; set; }

        public string lastname_country_frequency { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string country_code { get; set; }

        public string country_code_alpha { get; set; }

        public string country_name { get; set; }

        public string primary_language_code { get; set; }
        public string primary_language { get; set; }

        public string balance { get; set; }

        public string phone_Number { get; set; }

        public string currency { get; set; }
        public string partitionKey { get; set; }

        public int rowKey { get; set; }

        public string timestamp { get; set; }

        //public string eTag { get; set; } = "true";
    }
}
