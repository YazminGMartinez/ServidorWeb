using MongoDB.Bson;
public class CFDRegistro {

    public ObjectId Id{get; set;}
    public string? NombredelUsuario{ get; set; } = "";
    public string? CorreoElectronico{ get; set; } = "";
    public string? Password{ get; set; } = string.Empty;

}