// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const Registrar = document.getElementById("registrarForm");
Registrar.addEventListener("submit", (event) => 
{
    event.preventDefault();
    console.log("Se intentó enviar el formulario");
    const NombreUsuario = document.getElementById("NombreUsuario").value;
    const Contraseña = document.getElementById("Contraseña").value;
    const Nombre = document.getElementById("Nombre").value;
    const Apellido = document.getElementById("Apellido").value;
    const TipoUsuario = document.getElementById("TipoUsuario").value;
    if (NombreUsuario && Contraseña && Nombre && Apellido && TipoUsuario)
    {
        console.log("Todos los campos están completos. Se puede enviar el formulario.");
        Registrar.submit();
    }
    else
    {
        document.getElementById("errorMessage").textContent = "Por favor, complete todos los campos.";
    }
}
);