namespace CleanArch.School.WebApp.Shared.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EnrollStudentRequest
    {
        [Required] [MaxLength(255)] public string StudentName { get; set; }

        [Required]
        [MaxLength(14)]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$")]
        public string StudentCpf { get; set; }

        [Required] public DateTime? StudentBirthday { get; set; }

        [Required] public string Level { get; set; }

        [Required] public string Module { get; set; }

        [Required] public string Class { get; set; }

        [Required] [Range(1, 12)] public int? Installments { get; set; }
    }
}