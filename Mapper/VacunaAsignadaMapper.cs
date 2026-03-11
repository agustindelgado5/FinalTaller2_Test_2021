using FinalTaller2_Test_2021.Models;
using FinalTaller2_Test_2021.ViewModels;

public static class VacunaAsignadaMapper
{
    public static VacunaAsignada ToEntity(VacunaAsignadaViewModel vm)
    {
        return new VacunaAsignada
        {
            IdProvincia = vm.IdProvincia,
            IdVacuna = vm.IdVacuna,
            CantidadAplicada = vm.CantidadAplicada
        };
    }

    public static VacunaAsignadaViewModel ToViewModel(VacunaAsignada v)
    {
        return new VacunaAsignadaViewModel
        {
            IdProvincia = v.IdProvincia,
            IdVacuna = v.IdVacuna,
            CantidadAplicada = v.CantidadAplicada
        };
    }
}