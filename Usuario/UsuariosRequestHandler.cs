using MongoDB.Driver;
using System.Net.Mail;

public static class UsuariosRequestHandler {

public static IResult Registrarme (CFDRegistro datos) {
if(string.IsNullOrWhiteSpace(datos. NombredelUsuario)){
    return Results. BadRequest ("El nombre es requerido");
}
if(string.IsNullOrWhiteSpace(datos. CorreoElectronico)){
    return Results. BadRequest ("El correo electronico es requerido");
}
if(string. IsNullOrWhiteSpace(datos. Password)){
    return Results. BadRequest ("La contraseña es requerida");
}
BaseDatos bd = new BaseDatos();
var coleccion = bd. ObtenerColeccion<CFDRegistro> ("Usuarios");
if(coleccion == null){
    throw new Exception("No existe la colección Usuarios");
}
FilterDefinitionBuilder<CFDRegistro> filterBuilder = new FilterDefinitionBuilder<CFDRegistro>();
var filter = filterBuilder. Eq(x => x.CorreoElectronico, datos. CorreoElectronico);

CFDRegistro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
if(usuarioExistente != null){
    return Results.BadRequest($"Ya existe un usuario con el correo {datos.CorreoElectronico}");
}
coleccion. InsertOne (datos); 

return Results.Ok();
}

public static IResult Ingresar (CFDInicio datos){
if(string.IsNullOrWhiteSpace(datos. CorreoElectronico)){
    return Results. BadRequest ("El correo electronico es requerido");
}
if(string. IsNullOrWhiteSpace(datos. Password)){
    return Results. BadRequest ("La contraseña es requerida");
}
BaseDatos bd = new BaseDatos();
var coleccion = bd. ObtenerColeccion<CFDInicio> ("Usuarios");
if(coleccion == null){
    throw new Exception("No existe la colección Usuarios");
}
FilterDefinitionBuilder<CFDInicio> filterBuilder = new FilterDefinitionBuilder<CFDInicio>();
var filter = filterBuilder. Eq(x => x.CorreoElectronico, datos. CorreoElectronico);

CFDInicio? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
if(usuarioExistente == null){
    return Results.BadRequest($"No existe un usuario con el correo {datos.CorreoElectronico}");
}
if(usuarioExistente.Password != datos.Password){
    return Results.BadRequest($"El usuario o contraseña son incorrectos");
}
return Results.Ok("El inicio de sesion fue exitoso");
}
public static IResult Recuperar(CFDOlvido datos) {
      if (string.IsNullOrWhiteSpace(datos.CorreoElectronico)){
            return Results.BadRequest("El correo electronico es requerido");
        }
        BaseDatos bada = new BaseDatos();
        var coleccion = bada.ObtenerColeccion<CFDRegistro>("Usuarios");
        if(coleccion == null){
            throw new Exception("No existe la coleccion Usuarios");
        }
        FilterDefinitionBuilder<CFDRegistro> filterBuilder = new FilterDefinitionBuilder<CFDRegistro>();
        var filter = filterBuilder.Eq(x =>x.CorreoElectronico, datos.CorreoElectronico);
         
        CFDRegistro? usuarioExistente = coleccion.Find(filter).FirstOrDefault();
         if(usuarioExistente == null){
            return Results.BadRequest($"No existe un usuario con el correo {datos.CorreoElectronico}");
         } else if(usuarioExistente.CorreoElectronico==datos.CorreoElectronico){
            MensajeCorreo c = new MensajeCorreo();
            c.Destinatario = usuarioExistente.CorreoElectronico;
            c.Asunto = "RECUPERACION DE CONTRASEÑA";
            c.Mensaje = "Tu contraseña es: "+usuarioExistente.Password;
            c.Enviar();
            return Results.BadRequest("Correo enviado");
         }

         return Results.Ok();
    }
}
