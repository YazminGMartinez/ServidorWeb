using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;

public static class CategoriasRequestHandler{

    public static IResult Crear(CategoriaDTO datos){
        if(string.IsNullOrWhiteSpace(datos.Nombre)){
            return Results.BadRequest("El nombre de la categoria es requerido");
        }
        if(string.IsNullOrWhiteSpace(datos.UrlIcono)){
            return Results.BadRequest("El URL Icono es requerido");
        }
        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
        var builder = new FilterDefinitionBuilder<CategoriaDbMap>();
        var filter = builder.Eq(x => x.Nombre, datos.Nombre);
        CategoriaDbMap? registro = coleccion.Find(filter).FirstOrDefault();
        if(registro != null){
            return Results.BadRequest("La categoria ya existe");
        }

        registro = new CategoriaDbMap();
        registro.Nombre = datos.Nombre;
        registro.UrlIcono = datos.UrlIcono;

        coleccion!.InsertOne(registro);
        string nuevoId = registro.Id.ToString();

        return Results.Ok(nuevoId);
    }

    public static IResult Listar(){
        var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
        var filter = filterBuilder.Empty;

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
        List<CategoriaDbMap> mongoDbList = coleccion.Find(filter).ToList();

        var lista = mongoDbList.Select(x => new {
            Id = x.Id.ToString(), 
            Nombre = x. Nombre,
            UrlIcono = x. UrlIcono
        }). ToList();

        return Results.Ok(lista);
    }
}