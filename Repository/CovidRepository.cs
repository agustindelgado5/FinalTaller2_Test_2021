using Microsoft.Data.Sqlite;
using FinalTaller2_Test_2021.Models;

public class CovidRepository
{
    private readonly string _connectionString = "Data Source=CovidVacuna.db";

    public List<dynamic> GetVacunasAsignadas()
    {
        var list = new List<dynamic>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"SELECT p.Provincia, v.Vacuna, p.CantidadPersonal, va.CantidadAplicada
        FROM VacunaAsignada va
        JOIN Provincias p ON va.IdProvincia = p.IdProvincia
        JOIN TiposVacunas v ON va.IdVacuna = v.IdVacuna";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new
            {
                Provincia = reader.GetString(0),
                Vacuna = reader.GetString(1),
                Personal = reader.GetInt32(2),
                CantidadAplicada = reader.GetInt32(3)
            });
        }

        return list;
    }

    public void CreateVacunaAsignada(VacunaAsignada vacuna)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"INSERT INTO VacunaAsignada
        (IdProvincia,IdVacuna,CantidadAplicada)
        VALUES(@provincia,@vacuna,@cantidad)";

        command.Parameters.AddWithValue("@provincia", vacuna.IdProvincia);
        command.Parameters.AddWithValue("@vacuna", vacuna.IdVacuna);
        command.Parameters.AddWithValue("@cantidad", vacuna.CantidadAplicada);

        command.ExecuteNonQuery();
    }

    public void UpdateVacunaAsignada(VacunaAsignada vacuna)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"UPDATE VacunaAsignada
        SET CantidadAplicada=@cantidad
        WHERE IdProvincia=@provincia AND IdVacuna=@vacuna";

        command.Parameters.AddWithValue("@provincia", vacuna.IdProvincia);
        command.Parameters.AddWithValue("@vacuna", vacuna.IdVacuna);
        command.Parameters.AddWithValue("@cantidad", vacuna.CantidadAplicada);

        command.ExecuteNonQuery();
    }

    public void DeleteVacunaAsignada(int provinciaId, int vacunaId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"DELETE FROM VacunaAsignada
        WHERE IdProvincia=@provincia AND IdVacuna=@vacuna";

        command.Parameters.AddWithValue("@provincia", provinciaId);
        command.Parameters.AddWithValue("@vacuna", vacunaId);

        command.ExecuteNonQuery();
    }
    public List<dynamic> CantidadPorTipoVacuna()
    {
        var list = new List<dynamic>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"SELECT v.Vacuna, SUM(va.CantidadAplicada)
        FROM VacunaAsignada va
        JOIN TiposVacunas v ON va.IdVacuna = v.IdVacuna
        GROUP BY v.Vacuna";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new
            {
                Vacuna = reader.GetString(0),
                Total = reader.GetInt32(1)
            });
        }

        return list;
    }
    public dynamic ProvinciaMenosAstraZeneca()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"SELECT p.Provincia, SUM(va.CantidadAplicada) total
        FROM VacunaAsignada va
        JOIN Provincias p ON va.IdProvincia=p.IdProvincia
        JOIN TiposVacunas v ON va.IdVacuna=v.IdVacuna
        WHERE v.Vacuna='AstraZeneca'
        GROUP BY p.Provincia
        ORDER BY total ASC
        LIMIT 1";

        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new
            {
                Provincia = reader.GetString(0),
                Total = reader.GetInt32(1)
            };
        }

        return null;
    }

    public dynamic ProvinciaMasSputnik()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText =
        @"SELECT p.Provincia, SUM(va.CantidadAplicada) total
        FROM VacunaAsignada va
        JOIN Provincias p ON va.IdProvincia=p.IdProvincia
        JOIN TiposVacunas v ON va.IdVacuna=v.IdVacuna
        WHERE v.Vacuna='Sputnik V'
        GROUP BY p.Provincia
        ORDER BY total DESC
        LIMIT 1";

        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new
            {
                Provincia = reader.GetString(0),
                Total = reader.GetInt32(1)
            };
        }

        return null;
    }

}