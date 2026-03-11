using System.ComponentModel.DataAnnotations;

namespace FinalTaller2_Test_2021.ViewModels;

public class VacunaAsignadaViewModel
{
    [Required]
    public int IdProvincia { get; set; }

    [Required]
    public int IdVacuna { get; set; }

    [Required]
    public int CantidadAplicada { get; set; }
}