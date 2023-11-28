using MongoDB.Bson;
public class Alumno { 

    public ObjectId Id { get; set; }
    public string Paterno { get; set; } = string.Empty;
    public string Materno { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public long NumControl { get; set; }
    public string Especialidad { get; set; } = string.Empty;
    public int Semestre { get; set; }
    public string Grupo { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
}

