using Microsoft.Data.SqlClient;
using Dapper;

namespace try_catch_poc.Models;

public class BD
{
    private string _connectionString = @"Server=localhost;
    Database=TP05_Login;Integrated Security=True;TrustServerCertificate=True;";
    

    public Usuarios GetUsuario(string NombreUsuario, string Contraseña)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @pNombreUsuario AND Contraseña = @pContraseña";
            return connection.QueryFirstOrDefault<Usuarios>(query, new { pNombreUsuario = NombreUsuario, pContraseña = Contraseña});
        }
    }
    public void RegistrarUsuario(Usuarios usuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombre, Apellido, TipoUsuario) VALUES (@NombreUsuario, @Contraseña, @Nombre, @Apellido, @TipoUsuario)";
            connection.Execute(query, usuario);
        }
    }
    public int VerificarUsuarioExistente(string NombreUsuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
            int count = connection.ExecuteScalar<int>(query, new { NombreUsuario });
            return count;
        }
    }
}