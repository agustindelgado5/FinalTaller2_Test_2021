using Microsoft.AspNetCore.Mvc;
using FinalTaller2_Test_2021.ViewModels;

[ApiController]
[Route("api/vacunas")]
public class VacunasController : ControllerBase
{
    private readonly CovidRepository _repository = new();

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_repository.GetVacunasAsignadas());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] VacunaAsignadaViewModel vm)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = VacunaAsignadaMapper.ToEntity(vm);

            _repository.CreateVacunaAsignada(entity);

            return Ok("Vacuna asignada creada");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] VacunaAsignadaViewModel vm)
    {
        try
        {
            var entity = VacunaAsignadaMapper.ToEntity(vm);

            _repository.UpdateVacunaAsignada(entity);

            return Ok("Actualizado");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    public IActionResult Delete(int provinciaId, int vacunaId)
    {
        try
        {
            _repository.DeleteVacunaAsignada(provinciaId, vacunaId);

            return Ok("Eliminado");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("cantidad-por-vacuna")]
    public IActionResult CantidadPorVacuna()
    {
        return Ok(_repository.CantidadPorTipoVacuna());
    }

    [HttpGet("menos-astrazeneca")]
    public IActionResult MenosAstraZeneca()
    {
        return Ok(_repository.ProvinciaMenosAstraZeneca());
    }

    [HttpGet("mas-sputnik")]
    public IActionResult MasSputnik()
    {
        return Ok(_repository.ProvinciaMasSputnik());
    }
}