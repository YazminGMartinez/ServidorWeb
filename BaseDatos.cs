using MongoDB.Driver;
public class BaseDatos {
    private string conexion = "mongodb+srv://MarcoGalvan:vale3105@cluster0.tdesqgc.mongodb.net/?retryWrites=true&w=majority"; 
    private string baseDatos = "Proyecto";
   
    public IMongoCollection<T>? ObtenerColeccion<T>(string coleccion){
        MongoClient client = new MongoClient(this.conexion);
        IMongoCollection<T>? collection = client.GetDatabase(this.baseDatos).GetCollection<T>(coleccion);

        return collection;
    }
}