using MongoDB.Bson;
public class CFDInicio {

    public ObjectId Id{get; set;}
    public string? CorreoElectronico{ get; set; } = "";
    public string? Password{ get; set; } = string.Empty;
    public string? NombredelUsuario{ get; set; } = string.Empty;

}