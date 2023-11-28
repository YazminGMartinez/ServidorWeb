using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;

public static class LenguajeRequestHandler{
    public static IResult ListarRegistros (string idCategoria){
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x => x.IdCategoria, idCategoria);

        BaseDatos bd = new BaseDatos ();
        var coleccion = bd. ObtenerColeccion<LenguajeDbMap> ("Lenguaje");
        var lista = coleccion. Find (filter). ToList();

        return Results. Ok(lista.Select(x => new {
        Id = x.Id. ToString(), 
        IdCategoria = x. IdCategoria, 
        Titulo = x. Titulo,
        Descripcion = x.Descripcion,
        EsVideo = x. EsVideo,
        Url = x.Url
        }).ToList());
    }

    public static IResult CrearRegistro (LenguajeDTO datos){
        if(string.IsNullOrWhiteSpace(datos.IdCategoria)){
            return Results. BadRequest("El ID de la categoria es requerido");
        }
        if(string.IsNullOrWhiteSpace(datos. Titulo)){
            return Results.BadRequest("El titulo de la categoria es requerido");
        }
        if(string.IsNullOrWhiteSpace(datos.Descripcion)){
            return Results.BadRequest("La descripcion de la categoria es requerida");
        }
        
        if(string.IsNullOrWhiteSpace(datos.Url)){ 
            return Results.BadRequest("El Url de la categoria es requerido");
        }

        if(!ObjectId.TryParse(datos.IdCategoria, out ObjectId idCategoria)){ 
            return Results.BadRequest($"El Id de la categoria ({datos.IdCategoria}) no es válido");
        }

        BaseDatos bd = new BaseDatos();
        var FilterBuilderCategorias = new FilterDefinitionBuilder<CategoriaDbMap>(); 
        var filterCategoria = FilterBuilderCategorias. Eq(x => x.Id , idCategoria);
        var coleccionCategoria = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
        var categoria = coleccionCategoria. Find(filterCategoria).FirstOrDefault();

        if(categoria == null){
            return Results. NotFound ($"No existe una categoría con ID = {(datos.IdCategoria)}");
        }

        LenguajeDbMap registro = new LenguajeDbMap(); 
        registro.Titulo = datos.Titulo;
        registro.EsVideo = datos.EsVideo; 
        registro.Descripcion = datos.Descripcion;
        registro.Url = datos.Url;
        registro.IdCategoria = datos.IdCategoria;

        var coleccionLenguaje = bd.ObtenerColeccion<LenguajeDbMap> ("Lenguaje"); 
        coleccionLenguaje!.InsertOne (registro);
        return Results.Ok(registro.Id.ToString());
    }

    public static IResult Eliminar(string id){
        if(!ObjectId.TryParse(id, out ObjectId idLenguaje)){
            return Results. BadRequest($"El Id proporcionado ({id}) no es válido");
        }

        BaseDatos bd = new BaseDatos();
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x => x.Id, idLenguaje);
        var coleccion = bd. ObtenerColeccion<LenguajeDbMap>("Lenguaje");
        coleccion!.DeleteOne (filter);
        return Results.NoContent();
    }

    public static IResult Buscar(string texto){
        var queryExpr = new BsonRegularExpression (new Regex(texto, RegexOptions. IgnoreCase));
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder. Regex ("Titulo", queryExpr) |
            filterBuilder. Regex ("Descripcion", queryExpr);
            
        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("Lenguaje");
        var lista = coleccion.Find(filter).ToList();

        return Results.Ok(lista.Select(x => new {
        Id = x.Id.ToString(),
        IdCategoria = x.IdCategoria,
        Titulo = x.Titulo,
        Descripcion = x.Descripcion,
        EsVideo = x.EsVideo,
        Url = x.Url
        }).ToList());
    }
}